using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models;
using static THLWToolBox.Models.GeneralTypeMaster;

namespace THLWToolBox.Controllers
{
    public class PictureDatasFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        public PictureDatasFilter(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: PictureDatasFilter
        public async Task<IActionResult> Index(PictureDataViewModel request)
        {
            if (_context.PictureData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PictureData' is null.");
            }

            var pictureDatas = from pd in _context.PictureData
                               select pd;
            var pictureDatasList = await pictureDatas.Distinct().ToListAsync();

            var raceDatas = from rd in _context.RaceData
                            select rd;
            var raceDataList = await raceDatas.Distinct().ToListAsync();
            IDictionary<int, string> raceDict = new Dictionary<int, string>();
            foreach (var raceData in raceDataList)
                raceDict[raceData.id] = raceData.name;

            var displayPictureDatas = GetSelectedPictureDatas(pictureDatasList, request);

            CreateSelectLists(ref request, pictureDatasList);
            request.PictureDatas = displayPictureDatas;
            request.RaceDict = raceDict;

            return View(request);
        }

        /* It's too complex for LINQ, so just use naive list operation */
        List<PictureData> GetSelectedPictureDatas(List<PictureData> pds, PictureDataViewModel request)
        {
            List <PictureData> queryResult = new List<PictureData>();
            foreach (var pd in pds)
            {
                bool isSelected = true;
                /* --- check rare --- */
                if (   (pd.rare == 3 && request.RareType3 != null && request.RareType3.GetValueOrDefault() == false)
                    || (pd.rare == 4 && request.RareType4 != null && request.RareType4.GetValueOrDefault() == false)
                    || (pd.rare == 5 && request.RareType5 != null && request.RareType5.GetValueOrDefault() == false))
                {
                    isSelected = false;
                    continue;
                }

                /* --- check correction_type --- */
                List<int> currentCorrTypes = new List<int> { pd.correction1_type, pd.correction2_type };
                List<int> selectedCorrTypes = new List<int>();
                if (request.CorrTypeMain != null)
                    selectedCorrTypes.Add(request.CorrTypeMain.GetValueOrDefault());
                if (request.CorrTypeSub != null)
                    selectedCorrTypes.Add(request.CorrTypeSub.GetValueOrDefault());
                if (selectedCorrTypes.Except(currentCorrTypes).Any())
                {
                    isSelected = false;
                    continue;
                }

                /* --- check effect --- */
                isSelected &= FilterByEffect(pd, request.EffectId, request.SubeffectId, request.Range, request.UnitRoleTypeId, request.TurnTypeId);
                isSelected &= FilterByEffect(pd, request.Effect2Id, request.Subeffect2Id, request.Range2, request.UnitRoleType2Id, request.TurnType2Id);

                if (isSelected)
                {
                    queryResult.Add(pd);
                }
            }
            queryResult.Sort(delegate (PictureData pd1, PictureData pd2)
            {
                int corrTypeMain = request.CorrTypeMain.GetValueOrDefault();
                int corrTypeSub = request.CorrTypeMain.GetValueOrDefault();
                int displayPictureLevel = request.DisplayPictureLevel.GetValueOrDefault(0);
                if (request.CorrTypeMain != null && GetCorrTypeValue(corrTypeMain, pd1, displayPictureLevel) != GetCorrTypeValue(corrTypeMain, pd2, displayPictureLevel))
                    return -GetCorrTypeValue(corrTypeMain, pd1, displayPictureLevel).CompareTo(GetCorrTypeValue(corrTypeMain, pd2, displayPictureLevel));
                else if (request.CorrTypeSub != null && GetCorrTypeValue(corrTypeSub, pd1, displayPictureLevel) != GetCorrTypeValue(corrTypeSub, pd2, displayPictureLevel))
                    return -GetCorrTypeValue(corrTypeSub, pd1, displayPictureLevel).CompareTo(GetCorrTypeValue(corrTypeSub, pd2, displayPictureLevel));
                else if (pd1.rare != pd2.rare)
                    return -pd1.rare.CompareTo(pd2.rare);
                else
                    return -pd1.id.CompareTo(pd2.id);
            });
            return queryResult;
        }

        static List<EffectModel> GetEffectModels(PictureData pd)
        {
            return new List<EffectModel>{
                new EffectModel(pd.picture_characteristic1_effect_type, pd.picture_characteristic1_effect_subtype, pd.picture_characteristic1_effect_range, pd.picture_characteristic1_effect_type, pd.picture_characteristic1_effect_turn),
                new EffectModel(pd.picture_characteristic2_effect_type, pd.picture_characteristic2_effect_subtype, pd.picture_characteristic2_effect_range, pd.picture_characteristic2_effect_type, pd.picture_characteristic2_effect_turn),
                new EffectModel(pd.picture_characteristic3_effect_type, pd.picture_characteristic3_effect_subtype, pd.picture_characteristic3_effect_range, pd.picture_characteristic3_effect_type, pd.picture_characteristic3_effect_turn)
            };
        }

        static bool FilterByEffect(PictureData pd, int? EffectId, int? SubeffectId, int? Range, int? UnitRoleTypeId, int? TurnTypeId)
        {
            if (EffectId == null)
                return true;
            int effectType = EffectId.GetValueOrDefault();

            List<EffectModel> pdEffects = GetEffectModels(pd);
            foreach (var effect in pdEffects)
            {
                bool currentMatch = true;
                currentMatch &= (SelectItemModel.CreateSelectItemForEffect(effect, SelectItemTypes.EffectType).id == effectType);
                if (SubeffectId != null)
                    currentMatch &= (SelectItemModel.CreateSelectItemForEffect(effect, SelectItemTypes.SubEffectType).id == SubeffectId.GetValueOrDefault());
                if (Range != null)
                    currentMatch &= (SelectItemModel.CreateSelectItemForEffect(effect, SelectItemTypes.RangeType).id == Range.GetValueOrDefault());
                if (UnitRoleTypeId != null)
                    currentMatch &= (SelectItemModel.CreateSelectItemForEffect(effect, SelectItemTypes.UnitRoleType).id == UnitRoleTypeId.GetValueOrDefault());
                if (TurnTypeId != null)
                    currentMatch &= (SelectItemModel.CreateSelectItemForEffect(effect, SelectItemTypes.TurnType).id == TurnTypeId.GetValueOrDefault());
                if (currentMatch)
                    return true;
            }
            return false;
        }

        static void CreateSelectLists(ref PictureDataViewModel request, List<PictureData> pictureDatasList)
        {
            request.EffectTypes = new SelectList(GetSelectListItems(pictureDatasList, SelectItemTypes.EffectType), "id", "name", null);
            request.SubeffectTypes = new SelectList(GetSelectListItems(pictureDatasList, SelectItemTypes.SubEffectType), "id", "name", null);
            request.RangeTypes = new SelectList(GetSelectListItems(pictureDatasList, SelectItemTypes.RangeType), "id", "name", null);
            request.UnitRoleTypes = new SelectList(GetSelectListItems(pictureDatasList, SelectItemTypes.UnitRoleType), "id", "name", null);
            request.TurnTypes = new SelectList(GetSelectListItems(pictureDatasList, SelectItemTypes.TurnType), "id", "name", null);
        }

        static List<SelectItemModel> GetSelectListItems(List<PictureData> PictureDatasList, SelectItemTypes selectItemTypes)
        {
            List<SelectItemModel> list = new();
            HashSet<int> vis = new();
            foreach (PictureData pd in PictureDatasList)
            {
                List<EffectModel> pdEffects = GetEffectModels(pd);
                foreach (var effect in pdEffects)
                {
                    SelectItemModel sim = SelectItemModel.CreateSelectItemForEffect(effect, selectItemTypes);
                    if (sim.id == 0)
                        continue;
                    if (!vis.Contains(sim.id))
                    {
                        vis.Add(sim.id);
                        list.Add(sim);
                    }
                }
            }
            list.Sort(delegate (SelectItemModel sim1, SelectItemModel sim2) { return sim1.id.CompareTo(sim2.id); });
            return list;
        }

        static int GetCorrTypeValue(int CorrType, PictureData pd, int DisplayPictureLevel)
        {
            if (pd.correction1_type == CorrType)
                return CorrectionValueByLevel(pd.correction1_value, pd.correction1_diff, DisplayPictureLevel);
            else if (pd.correction2_type == CorrType)
                return CorrectionValueByLevel(pd.correction2_value, pd.correction2_diff, DisplayPictureLevel);
            else
                return -1;
        }
    }
}
