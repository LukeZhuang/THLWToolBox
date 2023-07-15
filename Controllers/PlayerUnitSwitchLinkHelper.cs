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
    public class PlayerUnitSwitchLinkHelper : Controller
    {
        private readonly THLWToolBoxContext _context;

        public PlayerUnitSwitchLinkHelper(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: PlayerUnitSwitchLinkHelper
        public async Task<IActionResult> Index(string? UnitSymbolName)
        {
            if (_context.PictureData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }
            var playerUnitDatas = from pud in _context.PlayerUnitData
                               select pud;
            var playerUnitDatasList = await playerUnitDatas.Distinct().ToListAsync();

            var relations = from prd in _context.PersonRelationData
                                  select prd;
            var relationList = await relations.Distinct().ToListAsync();

            var symbols = playerUnitDatas.Select(pud => pud.symbol_name);
            var symbolList = await symbols.Distinct().ToListAsync();

            List<PlayerUnitData> QueryUnit = new();
            List<PlayerUnitData> RelatedUnits = new();
            if (UnitSymbolName != null && UnitSymbolName.Length > 0)
            {
                foreach (var pud in playerUnitDatasList)
                {
                    if (UnitSymbolName.Equals(pud.name + pud.symbol_name))
                    {
                        QueryUnit.Add(pud);
                        HashSet<int> relatedUnitIds = new HashSet<int>();
                        foreach (var prd in relationList)
                        {
                            if (prd.person_id == pud.person_id)
                            {
                                relatedUnitIds.Add(prd.target_person_id);
                            }
                            else if (prd.target_person_id == pud.person_id)
                            {
                                relatedUnitIds.Add(prd.person_id);
                            }
                        }
                        foreach (var pud2 in playerUnitDatasList)
                        {
                            if (relatedUnitIds.Contains(pud2.person_id))
                            {
                                RelatedUnits.Add(pud2);
                            }
                        }
                    }
                }
            }

            var playerUnitDataVM = new PlayerUnitSwitchLinkHelperViewModel
            {
                QueryUnit = QueryUnit,
                RelatedUnits = RelatedUnits,
                UnitSymbolName = UnitSymbolName
            };
            return View(playerUnitDataVM);
        }

        [Produces("application/json")]
        public IActionResult SearchUser(string? term)
        {
            var playerUnitDatas = from pud in _context.PlayerUnitData
                               select pud;
            var result = playerUnitDatas.Where(pud => pud.name.Contains(term)).Select(pud => (pud.name + pud.symbol_name)).Distinct().ToList();

            return Json(result);
        }
    }
}
