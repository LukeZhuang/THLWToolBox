using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Helpers.GeneralHelper;

namespace THLWToolBox.Controllers
{
    public class IdHelper : Controller
    {
        private readonly THLWToolBoxContext _context;
        public IdHelper(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: IdHelper
        public async Task<IActionResult> Index(IdHelperViewModel request)
        {
            if (_context.PlayerUnitData == null || _context.PictureData == null)
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' or 'THLWToolBoxContext.PictureData' is null.");


            // ------ query data ------
            List<PlayerUnitData> unitList = await _context.PlayerUnitData.Distinct().ToListAsync();
            List<PictureData> pictureList = await _context.PictureData.Distinct().ToListAsync();
            // ------ query end ------

            List<PlayerUnitData> queryUnits = new();
            List<PictureData> queryPictures = new();

            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                PlayerUnitData unitRecord = GetUnitByNameSymbol(unitList, request.UnitSymbolName);
                queryUnits.Add(unitRecord);
            }
            if (request.PictureName != null && request.PictureName.Length > 0)
            {
                PictureData pictureRecord = GetPictureByName(pictureList, request.PictureName);
                queryPictures.Add(pictureRecord);
            }

            request.QueryUnits = queryUnits;
            request.QueryPictures = queryPictures;

            return View(request);
        }
    }
}
