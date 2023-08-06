using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models.ViewModels;

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
        public async Task<IActionResult> Index(string? UnitSymbolName, string? RaceName)
        {
            if (_context.PictureData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }
            var playerUnitDatas = from pud in _context.PlayerUnitData
                                  select pud;
            var playerUnitDatasList = await playerUnitDatas.Distinct().ToListAsync();

            var playerUnitRaceDatas = from purd in _context.PlayerUnitRaceData
                                  select purd;
            var playerUnitRaceDataList = await playerUnitRaceDatas.Distinct().ToListAsync();

            var raceDatas = from rd in _context.RaceData
                                      select rd;
            var raceDataList = await raceDatas.Distinct().ToListAsync();

            List<PlayerUnitRaceDisplayModel> displayUnitRaceDatas = new List<PlayerUnitRaceDisplayModel>();

            if (UnitSymbolName != null && UnitSymbolName.Length > 0)
            {
                foreach (var pud in playerUnitDatasList)
                {
                    if (UnitSymbolName.Equals(pud.name + pud.symbol_name))
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
                                    nameStr = "<b><color=#FF6600>" + nameStr + "</color></b>";
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
                QueryResults = displayUnitRaceDatas,
                UnitSymbolName = UnitSymbolName,
                RaceName = RaceName
            };
            return View(playerUnitDataVM);
        }

        public List<string> GetRaces()
        {
            var playerUnitDatas = from rd in _context.RaceData
                                  select rd;
            var result = playerUnitDatas.Select(pud => pud.name).Distinct().ToList();
            return result;
        }
    }
}
