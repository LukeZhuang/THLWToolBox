using System;
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

        // GET: PictureDatasFilter
        public async Task<IActionResult> Index(int? EffectId, int? SubeffectId, int? Range)
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

            var displayPictureDatas = GetSelectedPictureDatas(pictureDatasList, EffectId, SubeffectId, Range);

            var pictureDataVM = new PictureDataViewModel
            {
                EffectTypes = new SelectList(GetSelectListItems<PictureDataSelectItemEffectModel>(pictureDatasList), "id", "name", null),
                SubeffectTypes = new SelectList(GetSelectListItems<PictureDataSelectItemSubeffectModel>(pictureDatasList), "id", "name", null),
                RangeTypes = new SelectList(GetSelectListItems<PictureDataSelectItemRangeModel>(pictureDatasList), "id", "name", null),
                PictureDatas = displayPictureDatas,
                EffectId = EffectId,
                SubeffectId = SubeffectId,
                Range = Range
            };
            return View(pictureDataVM);
        }

        /* It's too complex for LINQ, so just use naive list operation */
        List<PictureData> GetSelectedPictureDatas(List<PictureData> pds, int? EffectId, int? SubeffectId, int? Range)
        {
            List <PictureData> queryResult = new List<PictureData>();
            if (EffectId == null)
                return queryResult;
            int effectType = EffectId.GetValueOrDefault();
            foreach (var pd in pds)
            {
                //Console.WriteLine(pd.name);
                List<int> effectTypes= new List<int> { pd.picture_characteristic1_effect_type, pd.picture_characteristic2_effect_type, pd.picture_characteristic3_effect_type };
                List<int> subEffectTypes = new List<int> { pd.picture_characteristic1_effect_subtype, pd.picture_characteristic2_effect_subtype, pd.picture_characteristic3_effect_subtype };
                List<int> rangeTypes = new List<int> { pd.picture_characteristic1_effect_range, pd.picture_characteristic2_effect_range, pd.picture_characteristic3_effect_range };

                for (int i = 0; i < 3; i++)
                {
                    bool isSelected = true;
                    int curEffectType = effectTypes[i];
                    int curSubEffectType = subEffectTypes[i];
                    int curRangeType = rangeTypes[i];
                    //Console.WriteLine("raw: " + curEffectType + " " + curSubEffectType + " " + curRangeType);
                    //Console.WriteLine("mod: " + GeneralTypeMaster.GetEffectRemappedInfo(curEffectType).Item1 + " " + GeneralTypeMaster.GetSubEffectRemappedInfo(curEffectType, curSubEffectType).Item1 + " " + GeneralTypeMaster.GetRangeRemappedInfo(curRangeType).Item1);
                    isSelected &= (GeneralTypeMaster.GetEffectRemappedInfo(curEffectType).Item1 == effectType);
                    if (SubeffectId != null)
                        isSelected &= (GeneralTypeMaster.GetSubEffectRemappedInfo(curEffectType, curSubEffectType).Item1 == SubeffectId.GetValueOrDefault());
                    if (Range != null)
                        isSelected &= (GeneralTypeMaster.GetRangeRemappedInfo(curRangeType).Item1 == Range.GetValueOrDefault());
                    if (isSelected)
                    {
                        queryResult.Add(pd);
                        break;
                    }
                }
            }
            return queryResult;
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
