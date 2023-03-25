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

            List<PictureData> displayPictureDatas = null;

            if (EffectId != null)
            {
                int effectType = EffectId.GetValueOrDefault();
                IQueryable<PictureData> pds = from pd in _context.PictureData
                                              where (   (   pd.picture_characteristic1_effect_type == effectType
                                                         && (SubeffectId == null ? true : pd.picture_characteristic1_effect_subtype == GeneralTypeMaster.GetSubeffectDecodedId(effectType, SubeffectId.GetValueOrDefault()))
                                                         && (Range == null ? true : pd.picture_characteristic1_effect_range == Range.GetValueOrDefault()))
                                                     || (pd.picture_characteristic2_effect_type == effectType
                                                         && (SubeffectId == null ? true : pd.picture_characteristic2_effect_subtype == GeneralTypeMaster.GetSubeffectDecodedId(effectType, SubeffectId.GetValueOrDefault()))
                                                         && (Range == null ? true : pd.picture_characteristic2_effect_range == Range.GetValueOrDefault()))
                                                     || (pd.picture_characteristic3_effect_type == effectType
                                                         && (SubeffectId == null ? true : pd.picture_characteristic3_effect_subtype == GeneralTypeMaster.GetSubeffectDecodedId(effectType, SubeffectId.GetValueOrDefault()))
                                                         && (Range == null ? true : pd.picture_characteristic3_effect_range == Range.GetValueOrDefault())))
                                              select pd;
                displayPictureDatas = await pds.Distinct().ToListAsync();
            }
            else
            {
                displayPictureDatas = new List<PictureData>();
            }

            var pictureDataVM = new PictureDataViewModel
            {
                EffectTypes = new SelectList(GetEffectList(pictureDatasList), "id", "name", null),
                SubeffectTypes = new SelectList(GetSubeffectList(pictureDatasList), "id", "name", null),
                RangeTypes = new SelectList(GetRangeList(pictureDatasList), "id", "name", null),
                PictureDatas = displayPictureDatas,
                EffectId = EffectId,
                SubeffectId = SubeffectId,
                Range = Range
            };
            return View(pictureDataVM);
        }

        List<PictureDataSelectItemEffectModel> GetEffectList(List<PictureData> PictureDatasList)
        {
            List<PictureDataSelectItemEffectModel> list = new List<PictureDataSelectItemEffectModel>();
            HashSet<int> vis = new HashSet<int>();
            foreach (PictureData pd in PictureDatasList)
            {
                for (int effectId = 1; effectId <= 3; effectId++)
                {
                    PictureDataSelectItemEffectModel pdim = new PictureDataSelectItemEffectModel(pd, effectId);
                    if (pdim.id == 0)
                        continue;
                    if (!vis.Contains(pdim.id))
                    {
                        vis.Add(pdim.id);
                        list.Add(pdim);
                    }
                }
            }
            list.Sort(delegate(PictureDataSelectItemEffectModel pdim1, PictureDataSelectItemEffectModel pdim2) { return pdim1.id.CompareTo(pdim2.id); });
            return list;
        }

        List<PictureDataSelectItemSubeffectModel> GetSubeffectList(List<PictureData> PictureDatasList)
        {
            List<PictureDataSelectItemSubeffectModel> list = new List<PictureDataSelectItemSubeffectModel>();
            HashSet<int> vis = new HashSet<int>();
            foreach (PictureData pd in PictureDatasList)
            {
                for (int effectId = 1; effectId <= 3; effectId++)
                {
                    PictureDataSelectItemSubeffectModel pdim = new PictureDataSelectItemSubeffectModel(pd, effectId);
                    if (pdim.id == 0)
                        continue;
                    if (!vis.Contains(pdim.id))
                    {
                        vis.Add(pdim.id);
                        list.Add(pdim);
                    }
                }
            }
            list.Sort(delegate (PictureDataSelectItemSubeffectModel pdim1, PictureDataSelectItemSubeffectModel pdim2) { return pdim1.id.CompareTo(pdim2.id); });

            return list;
        }
        List<PictureDataSelectItemRangeModel> GetRangeList(List<PictureData> PictureDatasList)
        {
            List<PictureDataSelectItemRangeModel> list = new List<PictureDataSelectItemRangeModel>();
            HashSet<int> vis = new HashSet<int>();
            foreach (PictureData pd in PictureDatasList)
            {
                for (int effectId = 1; effectId <= 3; effectId++)
                {
                    PictureDataSelectItemRangeModel pdim = new PictureDataSelectItemRangeModel(pd, effectId);
                    if (pdim.id == 0)
                        continue;
                    if (!vis.Contains(pdim.id))
                    {
                        vis.Add(pdim.id);
                        list.Add(pdim);
                    }
                }
            }
            list.Sort(delegate (PictureDataSelectItemRangeModel pdim1, PictureDataSelectItemRangeModel pdim2) { return pdim1.id.CompareTo(pdim2.id); });

            return list;
        }
    }
}
