using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;

namespace THLWToolBox.Controllers
{
    public class UnitRaceHelper : Controller
    {
        private readonly THLWToolBoxContext _context;

        public UnitRaceHelper(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: PlayerUnitRaceFilter
        public async Task<IActionResult> Index(UnitRaceHelperViewModel request)
        {
            if (_context.PictureData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }


            // --- query data tables ---
            var unitTable = from pud in _context.PlayerUnitData
                            select pud;
            var unitList = await unitTable.Distinct().ToListAsync();

            var unitRaceTable = from purd in _context.PlayerUnitRaceData
                                select purd;
            var unitRaceList = await unitRaceTable.Distinct().ToListAsync();

            var raceTable = from rd in _context.RaceData
                            select rd;
            var raceList = await raceTable.Distinct().ToListAsync();
            // ------ query end ------


            List<UnitRaceDisplayModel> queryUnits = new();
            List<UnitRaceDisplayModel> queryRaces = new();

            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                foreach (var unitRecord in unitList)
                {
                    string curUnitSymbolName = unitRecord.name + unitRecord.symbol_name;
                    if (!request.UnitSymbolName.Equals(curUnitSymbolName))
                        continue;
                    HashSet<int> targetRaceIds = GetRaceIdsOfUnit(unitRecord, unitRaceList);
                    string racesString = CreateRacesStringOfUnit(targetRaceIds, raceList);
                    queryUnits.Add(new UnitRaceDisplayModel(unitRecord, racesString));
                }
            }
            
            if (request.RaceName != null && request.RaceName.Length > 0)
            {
                int raceId = GetRaceIdByName(request.RaceName, raceList);
                GetUnitsOfRace(raceId, unitList, unitRaceList, raceList, ref queryRaces);
            }

            request.QueryUnits = queryUnits;
            request.QueryRaces = queryRaces;
            request.AllRacesString = string.Join(", ", from rd in raceList select rd.name);

            return View(request);
        }

        public static HashSet<int> GetRaceIdsOfUnit(PlayerUnitData unitRecord, List<PlayerUnitRaceData> unitRaceList)
        {
            return new(from unitRaceRecord in unitRaceList where (unitRaceRecord.unit_id == unitRecord.id) select unitRaceRecord.race_id);
        }

        public static string CreateRacesStringOfUnit(HashSet<int> raceIdSet, List<RaceData> raceList, int highlightRaceId = -1)
        {
            List<string> races = new();

            foreach (var raceRecord in raceList)
            {
                if (raceIdSet.Contains(raceRecord.id))
                {
                    string raceName = raceRecord.name;
                    if (highlightRaceId != -1 && raceRecord.id == highlightRaceId)
                        raceName = "<b><color=#FF6600>" + raceName + "</color></b>";
                    races.Add(raceName);
                }
            }

            return string.Join(", ", races);
        }

        static int GetRaceIdByName(string raceName, List<RaceData> raceList)
        {
            int raceId = -1;
            foreach (var raceRecord in raceList)
            {
                if (raceRecord.name.Equals(raceName))
                {
                    if (raceId != -1)
                        throw new NotImplementedException();
                    raceId = raceRecord.id;
                }
            }
            if (raceId == -1)
                throw new NotImplementedException();

            return raceId;
        }

        static HashSet<int> GetUnitIdsOfRace(int raceId, List<PlayerUnitRaceData> unitRaceList)
        {
            return new(from unitRaceRecord in unitRaceList where (unitRaceRecord.race_id == raceId) select unitRaceRecord.id);
        }

        static void GetUnitsOfRace(int raceId, List<PlayerUnitData> unitList, List<PlayerUnitRaceData> unitRaceList, List<RaceData> raceList, ref List<UnitRaceDisplayModel> queryRaces)
        {
            HashSet<int> unitIds = GetUnitIdsOfRace(raceId, unitRaceList);

            foreach (var unitRecord in unitList)
            {
                if (!unitIds.Contains(unitRecord.id))
                    continue;
                HashSet<int> unitRaceIds = GetRaceIdsOfUnit(unitRecord, unitRaceList);
                string racesStr = CreateRacesStringOfUnit(unitRaceIds, raceList, raceId);
                queryRaces.Add(new UnitRaceDisplayModel(unitRecord, racesStr));
            }
        }
    }
}