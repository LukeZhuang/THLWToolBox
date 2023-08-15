using Microsoft.AspNetCore.Mvc;
using THLWToolBox.Data;

namespace THLWToolBox.Controllers
{
    public class InputAutoCompleteHelper : Controller
    {
        private readonly THLWToolBoxContext _context;

        public InputAutoCompleteHelper(THLWToolBoxContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        public IActionResult SearchUser(string? term)
        {
            var matchedUnitNameList = _context.PlayerUnitData.Select(x => x.name + x.symbol_name)
                                                             .Where(x => x.Contains(term))
                                                             .Distinct().ToList();
            return Json(matchedUnitNameList);
        }

        [Produces("application/json")]
        public IActionResult SearchRace(string? term)
        {
            var matchedRaceNameList = _context.RaceData.Select(x => x.name)
                                                       .Where(x => x.Contains(term))
                                                       .Distinct().ToList();
            return Json(matchedRaceNameList);
        }

        [Produces("application/json")]
        public IActionResult SearchPicture(string? term)
        {
            var matchedPictureNameList = _context.PictureData.Select(x => x.name)
                                                             .Where(x => x.Contains(term))
                                                             .Distinct().ToList();
            return Json(matchedPictureNameList);
        }
    }
}
