using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Helpers.GeneralHelper;

namespace THLWToolBox.Controllers
{
    public class UnitRaceHelper : Controller
    {
        private readonly THLWToolBoxContext _context;

        // Data structures used across this controller
        List<PlayerUnitData> unitList;
        List<RaceData> raceList;
        Dictionary<int, HashSet<int>> unitToRaceIds;
        Dictionary<int, HashSet<int>> raceToUnitIds;

        public UnitRaceHelper(THLWToolBoxContext context)
        {
            _context = context;
            unitList = new();
            raceList = new();
            unitToRaceIds = new();
            raceToUnitIds = new();
        }

        // POST: PlayerUnitRaceFilter
        public async Task<IActionResult> Index(UnitRaceHelperViewModel request)
        {
            if (_context.PictureData == null)
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");


            // ------ query data ------
            var unitRaceList = await _context.PlayerUnitRaceData.Distinct().ToListAsync();

            unitList = await _context.PlayerUnitData.Distinct().ToListAsync();
            raceList = await _context.RaceData.Distinct().ToListAsync();
            unitToRaceIds = unitRaceList.GroupBy(x => x.unit_id).ToDictionary(y => y.Key, y => new HashSet<int>(y.Select(z => z.race_id)));
            raceToUnitIds = unitRaceList.GroupBy(x => x.race_id).ToDictionary(y => y.Key, y => new HashSet<int>(y.Select(z => z.unit_id)));
            // ------ query end ------


            List<UnitRaceDisplayModel> queryUnits = new();
            List<UnitRaceDisplayModel> queryRaces = new();

            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                PlayerUnitData? unitRecord = GetUnitByNameSymbol(unitList, request.UnitSymbolName);
                if (unitRecord != null)
                    queryUnits = CreateUnitRaceDisplayModelByUnit(unitRecord, unitToRaceIds.GetValueOrDefault(unitRecord.id, new()), raceList, null);
            }
            
            if (request.RaceName != null && request.RaceName.Length > 0)
            {
                int? raceId = GetRaceIdByName(raceList, request.RaceName);
                if (raceId != null)
                    queryRaces = CreateUnitRaceDisplayModelByRace(raceId.GetValueOrDefault());
            }

            request.QueryUnits = queryUnits;
            request.QueryRaces = queryRaces;
            request.AllRacesString = string.Join(", ", raceList.Select(x => x.name));

            return View(request);
        }

        List<UnitRaceDisplayModel> CreateUnitRaceDisplayModelByRace(int raceId)
        {
            List<UnitRaceDisplayModel> queryRaces = new();
            HashSet<int> unitIds = raceToUnitIds.GetValueOrDefault(raceId, new());
            foreach (var unitRecord in unitList)
                if (unitIds.Contains(unitRecord.id))
                   queryRaces.AddRange(CreateUnitRaceDisplayModelByUnit(unitRecord, unitToRaceIds.GetValueOrDefault(unitRecord.id, new()), raceList, raceId));
            return queryRaces;
        }
    }
}