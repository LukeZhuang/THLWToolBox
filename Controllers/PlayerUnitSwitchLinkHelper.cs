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

            List<PlayerUnitSwitchLinkDisplayModel> displayPictureDatas = new List<PlayerUnitSwitchLinkDisplayModel>();
            if (UnitSymbolName != null && UnitSymbolName.Length > 0)
            {
                foreach (var pud in playerUnitDatasList)
                {
                    if (UnitSymbolName.Equals(pud.name + pud.symbol_name))
                    {
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
                        List<PlayerUnitData> relatedUnits = new List<PlayerUnitData>();
                        foreach (var pud2 in playerUnitDatasList)
                        {
                            if (relatedUnitIds.Contains(pud2.person_id))
                            {
                                relatedUnits.Add(pud2);
                            }
                        }
                        displayPictureDatas.Add(new PlayerUnitSwitchLinkDisplayModel(pud, relatedUnits));
                    }
                }
            }

            var playerUnitDataVM = new PlayerUnitSwitchLinkHelperViewModel
            {
                PlayerUnitDatas = displayPictureDatas,
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
