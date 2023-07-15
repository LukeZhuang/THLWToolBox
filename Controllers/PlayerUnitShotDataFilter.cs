using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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

        // POST: PlayerUnitShotDataFilter
        public async Task<IActionResult> Index(string? UnitSymbolName)
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

            Dictionary<int, PlayerUnitShotData> shotDataDict = new();
            Dictionary<int, PlayerUnitSpellcardData> spellcardDataDict = new();

            foreach (var pusd in playerUnitShotDataList)
                shotDataDict[pusd.id] = pusd;
            foreach (var puscd in playerUnitSpellcardDataList)
                spellcardDataDict[puscd.id] = puscd;

            List<PlayerUnitData> QueryUnit = new();
            List<PlayerUnitShotDataDisplayModel> displayPlayerUnitDatas = new();

            if (UnitSymbolName != null && UnitSymbolName.Length > 0)
            {
                foreach (var pud in playerUnitDatasList)
                {
                    if (UnitSymbolName.Equals(pud.name + pud.symbol_name))
                    {
                        QueryUnit.Add(pud);
                        displayPlayerUnitDatas.Add(CreateShotDataDisplayModel("扩散射击", shotDataDict[pud.shot1_id]));
                        displayPlayerUnitDatas.Add(CreateShotDataDisplayModel("集中射击", shotDataDict[pud.shot2_id]));
                        displayPlayerUnitDatas.Add(CreateShotDataDisplayModel("符卡一", spellcardDataDict[pud.spellcard1_id]));
                        displayPlayerUnitDatas.Add(CreateShotDataDisplayModel("符卡二", spellcardDataDict[pud.spellcard2_id]));
                        displayPlayerUnitDatas.Add(CreateShotDataDisplayModel("终符", spellcardDataDict[pud.spellcard5_id]));
                    }
                }
            }

            var playerUnitDataVM = new PlayerUnitShotDataFilterViewModel
            {
                QueryUnit = QueryUnit,
                ShotDatas = displayPlayerUnitDatas,
                UnitSymbolName = UnitSymbolName
            };
            return View(playerUnitDataVM);
        }

        static PlayerUnitShotDataDisplayModel CreateShotDataDisplayModel(string shotType, PlayerUnitShotData shot)
        {
            return new PlayerUnitShotDataDisplayModel(shotType, shot.name, shot.phantasm_power_up_rate, shot.shot_level0_power_rate, shot.shot_level1_power_rate, shot.shot_level2_power_rate, shot.shot_level3_power_rate, shot.shot_level4_power_rate, shot.shot_level5_power_rate);
        }

        static PlayerUnitShotDataDisplayModel CreateShotDataDisplayModel(string shotType, PlayerUnitSpellcardData shot)
        {
            return new PlayerUnitShotDataDisplayModel(shotType, shot.name, shot.phantasm_power_up_rate, shot.shot_level0_power_rate, shot.shot_level1_power_rate, shot.shot_level2_power_rate, shot.shot_level3_power_rate, shot.shot_level4_power_rate, shot.shot_level5_power_rate);
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
