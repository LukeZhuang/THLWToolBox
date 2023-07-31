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
            var playerUnitDatas = from pud in _context.PlayerUnitData
                                  select pud;
            var result = playerUnitDatas.Where(pud => pud.name.Contains(term)).Select(pud => (pud.name + pud.symbol_name)).Distinct().ToList();

            return Json(result);
        }

        public List<string> GetRaces()
        {
            var playerUnitDatas = from rd in _context.RaceData
                                  select rd;
            var result = playerUnitDatas.Select(pud => pud.name).Distinct().ToList();
            return result;
        }

        [Produces("application/json")]
        public IActionResult SearchRace(string? term)
        {
            var result = GetRaces().Where(pud => pud.Contains(term));
            return Json(result);
        }
    }
}
