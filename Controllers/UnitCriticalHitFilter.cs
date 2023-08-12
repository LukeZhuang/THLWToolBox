using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Models.GeneralModels;
using static THLWToolBox.Helpers.GeneralHelper;
using System.Collections.Immutable;

namespace THLWToolBox.Controllers
{
    public class UnitCriticalHitFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        // Data structures used across this controller
        Dictionary<int, PlayerUnitBulletData> bulletDict;
        Dictionary<int, string> raceDict;
        Dictionary<int, List<int>> bulletToCriticalRaces;

        public UnitCriticalHitFilter(THLWToolBoxContext context)
        {
            _context = context;
            bulletDict = new();
            raceDict = new();
            bulletToCriticalRaces = new();
        }

        // POST: UnitCriticalHitFilter
        public async Task<IActionResult> Index(UnitCriticalHitFilterViewModel request)
        {
            if (_context.PlayerUnitData == null)
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");

            // ------ query data ------
            List<PlayerUnitData> unitList = await _context.PlayerUnitData.Distinct().ToListAsync();
            List<RaceData> raceList = await _context.RaceData.Distinct().ToListAsync();
            Dictionary<int, PlayerUnitShotData> shotDict = (await _context.PlayerUnitShotData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            Dictionary<int, PlayerUnitSpellcardData> spellcardDict = (await _context.PlayerUnitSpellcardData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            Dictionary<int, HashSet<int>> unitToRaceIds =
                (await _context.PlayerUnitRaceData.Distinct().ToListAsync()).GroupBy(x => x.unit_id)
                                                                            .ToDictionary(y => y.Key, y => new HashSet<int>(y.Select(z => z.race_id)));

            bulletDict = (await _context.PlayerUnitBulletData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            raceDict = (await _context.RaceData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x.name);
            bulletToCriticalRaces =
                (await _context.PlayerUnitBulletCriticalRaceData.Distinct().ToListAsync()).GroupBy(x => x.bullet_id)
                                                                                          .ToDictionary(y => y.Key, y => new List<int>(y.Select(z => z.race_id)));
            // ------ query end ------


            List<UnitRaceDisplayModel> queryUnits = new();
            List<UnitCriticalHitDisplayModel> criticalMatchUnits = new();

            HashSet<int> targetRaceIds = new();
            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                PlayerUnitData unitRecord = GetUnitByNameSymbol(unitList, request.UnitSymbolName);
                targetRaceIds = unitToRaceIds.GetValueOrDefault(unitRecord.id, new());
                queryUnits = CreateUnitRaceDisplayModelByUnit(unitRecord, targetRaceIds, raceList, null);
            }
            else if (request.RaceName != null && request.RaceName.Length > 0)
                targetRaceIds = new() { GetRaceIdByName(raceList, request.RaceName) };

            if (targetRaceIds.Count > 0)
            {
                foreach (var unitRecord in unitList)
                {
                    List<AttackWithWeightModel> attacks = GetUnitAttacksWithWeight(unitRecord, request.CreateAttackSelectionModel(), shotDict, spellcardDict);
                    UnitCriticalHitDisplayModel? unitCriticalHitData = CreateUnitCriticalHitDisplayModel(unitRecord, attacks, targetRaceIds);
                    if (unitCriticalHitData != null)
                        criticalMatchUnits.Add(unitCriticalHitData);
                }
            }
            criticalMatchUnits = criticalMatchUnits.OrderByDescending(x => x.TotalScore).ToList();

            request.QueryUnits = queryUnits;
            request.CriticalMatchUnits = criticalMatchUnits;
            request.AllRacesString = string.Join(", ", raceList.Select(x => x.name));

            return View(request);
        }

        UnitCriticalHitDisplayModel? CreateUnitCriticalHitDisplayModel(PlayerUnitData unitRecord, List<AttackWithWeightModel> attacks, HashSet<int> targetRaceIds)
        {
            double unitTotalScore = 0;
            List<AttackCriticalHitInfo?> UnitAttackCriticalHitList =
                attacks.Select(attack => SearchCriticalRaceInAttack(unitRecord, attack, targetRaceIds, ref unitTotalScore)).ToList();

            UnitAttackCriticalHitList = RemoveNullElements(UnitAttackCriticalHitList);
            if (UnitAttackCriticalHitList.Count > 0)
                return new UnitCriticalHitDisplayModel(unitRecord, CastToNonNullList(UnitAttackCriticalHitList), unitTotalScore);
            
            // null means this unit does not have critial hit
            return null;
        }

        AttackCriticalHitInfo? SearchCriticalRaceInAttack(PlayerUnitData unitRecord, AttackWithWeightModel attack, HashSet<int> targetRaceIds, ref double unitTotalScore)
        {
            double attackTotalScore = 0;
            List<MagazineCriticalHitInfo?> criticalHits =
                attack.AttackData.Magazines.Select(magazine => SearchCriticalRaceInMagazine(unitRecord, attack, magazine, targetRaceIds, ref attackTotalScore)).ToList();
            
            criticalHits = RemoveNullElements(criticalHits);

            if (criticalHits.Count > 0)
            {
                unitTotalScore += attackTotalScore;
                return new AttackCriticalHitInfo(attack.AttackData, CastToNonNullList(criticalHits));
            }

            // null means this attack does not have critial hit
            return null;
        }

        MagazineCriticalHitInfo? SearchCriticalRaceInMagazine(PlayerUnitData unitRecord, AttackWithWeightModel attack, BulletMagazineModel magazine,
                                                              HashSet<int> targetRaceIds, ref double attackTotalScore)
        {
            int bulletId = magazine.BulletId;
            if (!bulletDict.ContainsKey(bulletId))
                return null;

            List<int> bulletCriticalRaceIds = bulletToCriticalRaces.GetValueOrDefault(bulletId, new());
            List<string> foundCriticalRaceList = bulletCriticalRaceIds.Where(targetRaceIds.Contains).Select(x => raceDict[x]).ToList();

            bool magazineIsCriticalHit = (foundCriticalRaceList.Count > 0);
            attackTotalScore += CalcBulletPower(magazine, bulletDict[bulletId], unitRecord, attack.AttackData.PowerUpRates[5], attack.AttackWeight, magazineIsCriticalHit, magazineIsCriticalHit);
            if (magazineIsCriticalHit)
                return new MagazineCriticalHitInfo(magazine.MagazineId, string.Join(", ", foundCriticalRaceList));

            // null means this magazine does not have critial hit
            return null;
        }
    }
}
