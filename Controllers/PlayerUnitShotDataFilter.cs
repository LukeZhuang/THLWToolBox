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
    public class PlayerUnitShotDataFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        public PlayerUnitShotDataFilter(THLWToolBoxContext context)
        {
            _context = context;
        }

        // GET: PlayerUnitShotDataFilter
        public async Task<IActionResult> Index(string? UnitName, string? SymbolId, int? ShotId)
        {
            if (_context.PictureData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }
            var playerUnitDatas = from pud in _context.PlayerUnitData select pud;
            var playerUnitDatasList = await playerUnitDatas.Distinct().ToListAsync();

            var symbols = playerUnitDatas.Select(pud => pud.symbol_name);
            var symbolList = await symbols.Distinct().ToListAsync();

            var playerUnitShotDatas = from pusd in _context.PlayerUnitShotData select pusd;
            var playerUnitShotDataList = await playerUnitShotDatas.Distinct().ToListAsync();

            var playerUnitSpellcardDatas = from puscd in _context.PlayerUnitSpellcardData select puscd;
            var playerUnitSpellcardDataList = await playerUnitSpellcardDatas.Distinct().ToListAsync();

            List<PlayerUnitShotDataDisplayModel> displayPlayerUnitDatas = new List<PlayerUnitShotDataDisplayModel>();

            if (UnitName != null && UnitName.Length > 0 && ShotId != null)
            {
                int shotIdVal = ShotId.GetValueOrDefault();
                foreach (var pud in playerUnitDatasList)
                {
                    if (pud.name.Equals(UnitName))
                    {
                        if (SymbolId == null || SymbolId.Equals("null") || SymbolId.Equals(pud.symbol_name))
                        {
                            if (shotIdVal <= 2)
                            {
                                int shotIdInTable = (shotIdVal == 1 ? pud.shot1_id : pud.shot2_id);
                                foreach (var pusd in playerUnitShotDataList)
                                {
                                    if (pusd.id == shotIdInTable)
                                    {
                                        displayPlayerUnitDatas.Add(new PlayerUnitShotDataDisplayModel(pud, pusd.name, pusd.phantasm_power_up_rate, pusd.shot_level0_power_rate, pusd.shot_level1_power_rate, pusd.shot_level2_power_rate, pusd.shot_level3_power_rate, pusd.shot_level4_power_rate, pusd.shot_level5_power_rate));
                                    }
                                }
                            }
                            else
                            {
                                int spellcardIdInTable = (shotIdVal == 3 ? pud.spellcard1_id : (shotIdVal == 4 ? pud.spellcard2_id : pud.spellcard5_id));
                                foreach (var puscd in playerUnitSpellcardDataList)
                                {
                                    if (puscd.id == spellcardIdInTable)
                                    {
                                        displayPlayerUnitDatas.Add(new PlayerUnitShotDataDisplayModel(pud, puscd.name, puscd.phantasm_power_up_rate, puscd.shot_level0_power_rate, puscd.shot_level1_power_rate, puscd.shot_level2_power_rate, puscd.shot_level3_power_rate, puscd.shot_level4_power_rate, puscd.shot_level5_power_rate));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var playerUnitDataVM = new PlayerUnitShotDataFilterViewModel
            {
                PlayerUnitDatas = displayPlayerUnitDatas,
                Symbols = new SelectList(symbolList),
                SymbolId = SymbolId,
                UnitName = UnitName
            };
            return View(playerUnitDataVM);
        }

        [Produces("application/json")]
        public IActionResult SearchUser(string? term)
        {
            var playerUnitDatas = from pud in _context.PlayerUnitData
                                  select pud;
            var result = playerUnitDatas.Where(pud => pud.name.Contains(term)).Select(pud => pud.name).Distinct().ToList();

            return Json(result);
        }
    }
}
