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
            var matchedUnitNames = from pud in _context.PlayerUnitData
                                   where (pud.name + pud.symbol_name).Contains(term)
                                   select (pud.name + pud.symbol_name);
            var matchedUnitNameList = matchedUnitNames.Distinct().ToList();

            return Json(matchedUnitNameList);
        }

        [Produces("application/json")]
        public IActionResult SearchRace(string? term)
        {
            var matchedRaceNames = from rd in _context.RaceData
                                   where rd.name.Contains(term)
                                   select rd.name;
            var matchedRaceNameList = matchedRaceNames.Distinct().ToList();
            return Json(matchedRaceNameList);
        }
    }
}
