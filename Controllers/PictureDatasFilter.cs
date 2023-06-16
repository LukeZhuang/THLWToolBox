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
        public async Task<IActionResult> Index(int? EffectId, int? SubeffectId, int? Range, int? UnitRoleTypeId, int? TurnTypeId,
                                               bool? RareType3, bool? RareType4, bool? RareType5,
                                               bool? SimplifiedEffect, int? DisplayPictureLevel,
                                               int? CorrTypeMain, int? CorrTypeSub)
        {
            //return _context.PictureData != null ?
            //            View(await _context.PictureData.ToListAsync()) :
            //            Problem("Entity set 'THLWToolBoxContext.PictureData'  is null.");
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

            var displayPictureDatas = GetSelectedPictureDatas(pictureDatasList, EffectId, SubeffectId, Range, UnitRoleTypeId, TurnTypeId,
                                                              RareType3, RareType4, RareType5,
                                                              CorrTypeMain, CorrTypeSub, DisplayPictureLevel);

            var pictureDataVM = new PictureDataViewModel
            {
                EffectTypes = new SelectList(GetSelectListItems<PictureDataSelectItemEffectModel>(pictureDatasList), "id", "name", null),
                SubeffectTypes = new SelectList(GetSelectListItems<PictureDataSelectItemSubeffectModel>(pictureDatasList), "id", "name", null),
                RangeTypes = new SelectList(GetSelectListItems<PictureDataSelectItemRangeModel>(pictureDatasList), "id", "name", null),
                UnitRoleTypes = new SelectList(GetSelectListItems<PictureDataSelectItemUnitRoleTypeModel>(pictureDatasList), "id", "name", null),
                TurnTypes = new SelectList(GetSelectListItems<PictureDataSelectItemTurnTypeModel>(pictureDatasList), "id", "name", null),
                PictureDatas = displayPictureDatas,
                //PictureDatas = pictureDatasList,
                EffectId = EffectId,
                SubeffectId = SubeffectId,
                Range = Range,
                UnitRoleTypeId = UnitRoleTypeId,
                TurnTypeId = TurnTypeId,
                RareType3 = RareType3,
                RareType4 = RareType4,
                RareType5 = RareType5,
                SimplifiedEffect = SimplifiedEffect,
                RaceDict= raceDict,
                DisplayPictureLevel = DisplayPictureLevel.GetValueOrDefault(0),
                CorrTypeMain = CorrTypeMain,
                CorrTypeSub = CorrTypeSub
            };
            return View(pictureDataVM);
        }

        /* It's too complex for LINQ, so just use naive list operation */
        List<PictureData> GetSelectedPictureDatas(List<PictureData> pds, int? EffectId, int? SubeffectId, int? Range, int? UnitRoleTypeId, int? TurnTypeId,
                                                  bool? RareType3, bool? RareType4, bool? RareType5,
                                                  int? CorrTypeMain, int? CorrTypeSub, int? DisplayPictureLevel)
        {
            List <PictureData> queryResult = new List<PictureData>();
            foreach (var pd in pds)
            {
                bool isSelected = true;
                /* --- check rare --- */
                if (   (pd.rare == 3 && RareType3 != null && RareType3.GetValueOrDefault() == false)
                    || (pd.rare == 4 && RareType4 != null && RareType4.GetValueOrDefault() == false)
                    || (pd.rare == 5 && RareType5 != null && RareType5.GetValueOrDefault() == false))
                {
                    isSelected = false;
                    continue;
                }

                /* --- check correction_type --- */
                List<int> currentCorrTypes = new List<int> { pd.correction1_type, pd.correction2_type };
                List<int> selectedCorrTypes = new List<int>();
                if (CorrTypeMain != null)
                    selectedCorrTypes.Add(CorrTypeMain.GetValueOrDefault());
                if (CorrTypeSub != null)
                    selectedCorrTypes.Add(CorrTypeSub.GetValueOrDefault());
                if (selectedCorrTypes.Except(currentCorrTypes).Any())
                {
                    isSelected = false;
                    continue;
                }

                /* --- check effect --- */
                if (EffectId != null)
                {
                    bool AnyEffectMatch = false;
                    int effectType = EffectId.GetValueOrDefault();
                    List<int> effectTypes = new List<int> { pd.picture_characteristic1_effect_type, pd.picture_characteristic2_effect_type, pd.picture_characteristic3_effect_type };
                    List<int> subEffectTypes = new List<int> { pd.picture_characteristic1_effect_subtype, pd.picture_characteristic2_effect_subtype, pd.picture_characteristic3_effect_subtype };
                    List<int> rangeTypes = new List<int> { pd.picture_characteristic1_effect_range, pd.picture_characteristic2_effect_range, pd.picture_characteristic3_effect_range };
                    List<int> turnTypes = new List<int> { pd.picture_characteristic1_effect_turn, pd.picture_characteristic2_effect_turn, pd.picture_characteristic3_effect_turn };

                    for (int i = 0; i < 3; i++)
                    {
                        bool currentMatch = true;
                        int curEffectType = effectTypes[i];
                        int curSubEffectType = subEffectTypes[i];
                        int curRangeType = rangeTypes[i];
                        int curTurnType = turnTypes[i];
                        //Console.WriteLine("raw: " + curEffectType + " " + curSubEffectType + " " + curRangeType);
                        //Console.WriteLine("mod: " + GeneralTypeMaster.GetEffectRemappedInfo(curEffectType).Item1 + " " + GeneralTypeMaster.GetSubEffectRemappedInfo(curEffectType, curSubEffectType).Item1 + " " + GeneralTypeMaster.GetRangeRemappedInfo(curRangeType).Item1);
                        currentMatch &= (GeneralTypeMaster.GetEffectRemappedInfo(curEffectType).Item1 == effectType);
                        if (SubeffectId != null)
                            currentMatch &= (GeneralTypeMaster.GetSubEffectRemappedInfo(curEffectType, curSubEffectType).Item1 == SubeffectId.GetValueOrDefault());
                        if (Range != null)
                            currentMatch &= (GeneralTypeMaster.GetRangeRemappedInfo(curRangeType).Item1 == Range.GetValueOrDefault());
                        if (UnitRoleTypeId != null)
                            currentMatch &= (GeneralTypeMaster.GetEffectByRoleRemappedInfo(curEffectType).Item1 == UnitRoleTypeId.GetValueOrDefault());
                        if (TurnTypeId != null)
                            currentMatch &= (curTurnType == TurnTypeId.GetValueOrDefault());
                        if (currentMatch)
                        {
                            AnyEffectMatch = true;
                            break;
                        }
                    }
                    if (!AnyEffectMatch)
                        isSelected = false;
                }

                if (isSelected)
                {
                    queryResult.Add(pd);
                }
            }
            queryResult.Sort(delegate (PictureData pd1, PictureData pd2)
            {
                if (CorrTypeMain != null && GetCorrTypeValue(CorrTypeMain.GetValueOrDefault(), pd1, DisplayPictureLevel.GetValueOrDefault()) != GetCorrTypeValue(CorrTypeMain.GetValueOrDefault(), pd2, DisplayPictureLevel.GetValueOrDefault()))
                    return -GetCorrTypeValue(CorrTypeMain.GetValueOrDefault(), pd1, DisplayPictureLevel.GetValueOrDefault()).CompareTo(GetCorrTypeValue(CorrTypeMain.GetValueOrDefault(), pd2, DisplayPictureLevel.GetValueOrDefault()));
                else if (CorrTypeSub != null && GetCorrTypeValue(CorrTypeSub.GetValueOrDefault(), pd1, DisplayPictureLevel.GetValueOrDefault()) != GetCorrTypeValue(CorrTypeSub.GetValueOrDefault(), pd2, DisplayPictureLevel.GetValueOrDefault()))
                    return -GetCorrTypeValue(CorrTypeSub.GetValueOrDefault(), pd1, DisplayPictureLevel.GetValueOrDefault()).CompareTo(GetCorrTypeValue(CorrTypeSub.GetValueOrDefault(), pd2, DisplayPictureLevel.GetValueOrDefault()));
                else if (pd1.rare != pd2.rare)
                    return -pd1.rare.CompareTo(pd2.rare);
                else
                    return -pd1.id.CompareTo(pd2.id);
            });
            return queryResult;
        }

        int GetCorrTypeValue(int CorrType, PictureData pd, int DisplayPictureLevel)
        {
            if (pd.correction1_type == CorrType)
                return GeneralTypeMaster.CorrectionValueByLevel(pd.correction1_value, pd.correction1_diff, DisplayPictureLevel);
            else if (pd.correction2_type == CorrType)
                return GeneralTypeMaster.CorrectionValueByLevel(pd.correction2_value, pd.correction2_diff, DisplayPictureLevel);
            else
                return -1;
        }

        List<PictureDataSelectItemModel> GetSelectListItems<T>(List<PictureData> PictureDatasList)
        {
            List<PictureDataSelectItemModel> list = new List<PictureDataSelectItemModel>();
            HashSet<int> vis = new HashSet<int>();
            foreach (PictureData pd in PictureDatasList)
            {
                for (int effectId = 1; effectId <= 3; effectId++)
                {
                    PictureDataSelectItemModel pdim = (PictureDataSelectItemModel)Activator.CreateInstance(typeof(T), new object[] { pd, effectId });
                    if (pdim.id == 0)
                        continue;
                    if (!vis.Contains(pdim.id))
                    {
                        vis.Add(pdim.id);
                        list.Add(pdim);
                    }
                }
            }
            list.Sort(delegate (PictureDataSelectItemModel pdim1, PictureDataSelectItemModel pdim2) { return pdim1.id.CompareTo(pdim2.id); });
            return list;
        }
    }
}
