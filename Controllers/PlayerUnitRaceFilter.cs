using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models;

namespace THLWToolBox.Controllers
{
    public class PlayerUnitRaceFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        public PlayerUnitRaceFilter(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: PlayerUnitRaceFilter
        public async Task<IActionResult> Index(string? UnitName, string? SymbolId, string? RaceName)
        {
            if (_context.PictureData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }
            var playerUnitDatas = from pud in _context.PlayerUnitData
                                  select pud;
            var playerUnitDatasList = await playerUnitDatas.Distinct().ToListAsync();

            var symbols = playerUnitDatas.Select(pud => pud.symbol_name);
            var symbolList = await symbols.Distinct().ToListAsync();

            var playerUnitRaceDatas = from purd in _context.PlayerUnitRaceData
                                  select purd;
            var playerUnitRaceDataList = await playerUnitRaceDatas.Distinct().ToListAsync();

            var raceDatas = from rd in _context.RaceData
                                      select rd;
            var raceDataList = await raceDatas.Distinct().ToListAsync();

            List<PlayerUnitRaceDisplayModel> displayUnitRaceDatas = new List<PlayerUnitRaceDisplayModel>();

            if (UnitName != null && UnitName.Length > 0)
            {
                foreach (var pud in playerUnitDatasList)
                {
                    if (pud.name.Equals(UnitName))
                    {
                        if (SymbolId == null || SymbolId.Equals("null") || SymbolId.Equals(pud.symbol_name))
                        {
                            List<string> queryRaces = new List<string>();
                            HashSet<int> raceIds = new HashSet<int>();
                            foreach (var purd in playerUnitRaceDataList)
                            {
                                if (purd.unit_id == pud.id)
                                {
                                    raceIds.Add(purd.race_id);
                                }
                            }
                            foreach (var rd in raceDataList)
                            {
                                if (raceIds.Contains(rd.id))
                                {
                                    queryRaces.Add(rd.name);
                                }
                            }
                            displayUnitRaceDatas.Add(new PlayerUnitRaceDisplayModel(pud, queryRaces));
                        }
                    }
                }
            }
            else if (RaceName != null && RaceName.Length > 0)
            {
                int raceId = -1;
                foreach (var rd in raceDataList)
                {
                    if (rd.name.Equals(RaceName))
                    {
                        if (raceId != -1)
                            throw new NotImplementedException();
                        raceId = rd.id;
                    }
                }
                Console.WriteLine(raceId + " " + RaceName);
                HashSet<int> unitIds = new HashSet<int>();
                foreach (var purd in playerUnitRaceDataList)
                {
                    if (purd.race_id == raceId)
                    {
                        unitIds.Add(purd.unit_id);
                    }
                }
                foreach (var pud in playerUnitDatasList)
                {
                    if (unitIds.Contains(pud.id))
                    {
                        List<string> queryRaces = new List<string>();
                        HashSet<int> raceIds = new HashSet<int>();
                        foreach (var purd in playerUnitRaceDataList)
                        {
                            if (purd.unit_id == pud.id)
                            {
                                raceIds.Add(purd.race_id);
                            }
                        }
                        foreach (var rd in raceDataList)
                        {
                            if (raceIds.Contains(rd.id))
                            {
                                string nameStr = rd.name;
                                if (rd.name.Equals(RaceName))
                                {
                                    nameStr = "<color=#FF6600>" + nameStr + "</color>";
                                }
                                queryRaces.Add(nameStr);
                            }
                        }
                        displayUnitRaceDatas.Add(new PlayerUnitRaceDisplayModel(pud, queryRaces));
                    }
                }
            }

            var playerUnitDataVM = new PlayerUnitRaceFilterViewModel
            {
                RaceList = String.Join(", ", GetRaces()),
                Symbols = new SelectList(symbolList),
                SymbolId = SymbolId,
                QueryResults = displayUnitRaceDatas,
                UnitName = UnitName,
                RaceName = RaceName
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
