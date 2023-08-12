using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Models.GeneralModels;
using static THLWToolBox.Models.ViewModels.PictureFilterViewModel;

namespace THLWToolBox.Controllers
{
    public class PictureFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        // Data structures used across this controller
        List<PictureData> pictureList;

        public PictureFilter(THLWToolBoxContext context)
        {
            _context = context;
            pictureList = new();
        }

        // POST: PictureFilter
        public async Task<IActionResult> Index(PictureFilterViewModel request)
        {
            if (_context.PictureData == null)
                return Problem("Entity set 'THLWToolBoxContext.PictureData' is null.");

            // ------ query data ------
            Dictionary<int, string> raceDict = (await _context.RaceData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x.name);

            pictureList = await _context.PictureData.Distinct().ToListAsync();
            // ------ query end ------

            List<PictureData> displayPictureList = GetSelectedPictureDatas(request);

            CreateSelectLists(ref request);
            request.DisplayPictureList = displayPictureList;
            request.RaceDict = raceDict;

            return View(request);
        }

        // It's too complex for LINQ, so just use naive list operation
        List<PictureData> GetSelectedPictureDatas(PictureFilterViewModel request)
        {
            List <PictureData> queryResult = new();

            // Filter pictureList by chosen rules
            foreach (var pictureRecord in pictureList)
            {
                bool isSelected = true;
                // --- check rare ---
                if (   (pictureRecord.rare == 3 && request.RareType3 != null && request.RareType3.GetValueOrDefault() == false)
                    || (pictureRecord.rare == 4 && request.RareType4 != null && request.RareType4.GetValueOrDefault() == false)
                    || (pictureRecord.rare == 5 && request.RareType5 != null && request.RareType5.GetValueOrDefault() == false))
                {
                    isSelected = false;
                    continue;
                }

                // --- check correction_type ---
                List<int> currentCorrTypes = new() { pictureRecord.correction1_type, pictureRecord.correction2_type };
                List<int> selectedCorrTypes = new();
                if (request.CorrTypeMain != null)
                    selectedCorrTypes.Add(request.CorrTypeMain.GetValueOrDefault());
                if (request.CorrTypeSub != null)
                    selectedCorrTypes.Add(request.CorrTypeSub.GetValueOrDefault());
                if (selectedCorrTypes.Except(currentCorrTypes).Any())
                {
                    // means all types from request are in this picture record
                    isSelected = false;
                    continue;
                }

                // --- check effect ---
                isSelected &= FilterByEffect(pictureRecord, request.EffectId1, request.SubeffectId1, request.Range1, request.UnitRoleTypeId1, request.TurnTypeId1);
                isSelected &= FilterByEffect(pictureRecord, request.EffectId2, request.SubeffectId2, request.Range2, request.UnitRoleTypeId2, request.TurnTypeId2);

                if (isSelected)
                {
                    queryResult.Add(pictureRecord);
                }
            }

            // Sort query results by these orders: CorrTypeMain, corrTypeSub, rare, id
            queryResult.Sort(delegate (PictureData pd1, PictureData pd2)
            {
                int corrLevel = request.CorrLevel.GetValueOrDefault(0);
                int corrTypeMain = request.CorrTypeMain.GetValueOrDefault();
                int corrTypeSub = request.CorrTypeMain.GetValueOrDefault();
                if (request.CorrTypeMain != null && GetCorrTypeValue(corrTypeMain, pd1, corrLevel) != GetCorrTypeValue(corrTypeMain, pd2, corrLevel))
                    return -GetCorrTypeValue(corrTypeMain, pd1, corrLevel).CompareTo(GetCorrTypeValue(corrTypeMain, pd2, corrLevel));
                else if (request.CorrTypeSub != null && GetCorrTypeValue(corrTypeSub, pd1, corrLevel) != GetCorrTypeValue(corrTypeSub, pd2, corrLevel))
                    return -GetCorrTypeValue(corrTypeSub, pd1, corrLevel).CompareTo(GetCorrTypeValue(corrTypeSub, pd2, corrLevel));
                else if (pd1.rare != pd2.rare)
                    return -pd1.rare.CompareTo(pd2.rare);
                else
                    return -pd1.id.CompareTo(pd2.id);
            });
            return queryResult;
        }

        static List<EffectModel> GetEffectModels(PictureData pictureRecord)
        {
            return new() {
                new EffectModel(pictureRecord.picture_characteristic1_effect_type,
                                pictureRecord.picture_characteristic1_effect_subtype,
                                pictureRecord.picture_characteristic1_effect_range,
                                pictureRecord.picture_characteristic1_effect_type,
                                pictureRecord.picture_characteristic1_effect_turn),

                new EffectModel(pictureRecord.picture_characteristic2_effect_type,
                                pictureRecord.picture_characteristic2_effect_subtype,
                                pictureRecord.picture_characteristic2_effect_range,
                                pictureRecord.picture_characteristic2_effect_type,
                                pictureRecord.picture_characteristic2_effect_turn),

                new EffectModel(pictureRecord.picture_characteristic3_effect_type,
                                pictureRecord.picture_characteristic3_effect_subtype,
                                pictureRecord.picture_characteristic3_effect_range,
                                pictureRecord.picture_characteristic3_effect_type,
                                pictureRecord.picture_characteristic3_effect_turn),
            };
        }

        static bool FilterByEffect(PictureData pictureRecord, int? EffectId, int? SubeffectId, int? Range, int? UnitRoleTypeId, int? TurnTypeId)
        {
            // EffectId is the main filter, if it's null then it's meaningless to do the filtering
            if (EffectId == null)
                return true;

            List<EffectModel> effects = GetEffectModels(pictureRecord);
            foreach (var effect in effects)
            {
                bool currentMatch = true;
                currentMatch &= (SelectItemModel.CreateSelectItemForEffect(effect, SelectItemTypes.EffectType).id == EffectId.GetValueOrDefault());
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

        void CreateSelectLists(ref PictureFilterViewModel request)
        {
            request.EffectTypes = new SelectList(GetSelectListItems(SelectItemTypes.EffectType), "id", "name", null);
            request.SubeffectTypes = new SelectList(GetSelectListItems(SelectItemTypes.SubEffectType), "id", "name", null);
            request.RangeTypes = new SelectList(GetSelectListItems(SelectItemTypes.RangeType), "id", "name", null);
            request.UnitRoleTypes = new SelectList(GetSelectListItems(SelectItemTypes.UnitRoleType), "id", "name", null);
            request.TurnTypes = new SelectList(GetSelectListItems(SelectItemTypes.TurnType), "id", "name", null);
        }

        List<SelectItemModel> GetSelectListItems(SelectItemTypes selectItemTypes)
        {
            List<SelectItemModel> selectList = new();
            HashSet<int> visited = new();
            foreach (PictureData pictureRecord in pictureList)
            {
                List<EffectModel> pictureEffects = GetEffectModels(pictureRecord);
                foreach (var effect in pictureEffects)
                {
                    SelectItemModel sim = SelectItemModel.CreateSelectItemForEffect(effect, selectItemTypes);
                    if (sim.id == 0)
                        continue;
                    if (!visited.Contains(sim.id))
                    {
                        visited.Add(sim.id);
                        selectList.Add(sim);
                    }
                }
            }
            selectList.Sort(delegate (SelectItemModel sim1, SelectItemModel sim2) { return sim1.id.CompareTo(sim2.id); });
            return selectList;
        }

        static int GetCorrTypeValue(int CorrType, PictureData pictureRecord, int corrLevel)
        {
            if (pictureRecord.correction1_type == CorrType)
                return CorrectionValueByLevel(pictureRecord.correction1_value, pictureRecord.correction1_diff, corrLevel);
            else if (pictureRecord.correction2_type == CorrType)
                return CorrectionValueByLevel(pictureRecord.correction2_value, pictureRecord.correction2_diff, corrLevel);
            else
                return -1;
        }
    }
}
