using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Helpers;
using THLWToolBox.Models;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Models.GeneralModels;
using static THLWToolBox.Helpers.GeneralHelper;
using Azure.Core;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace THLWToolBox.Controllers
{
    public class UnitCriticalHitFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        public UnitCriticalHitFilter(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: UnitCriticalHitFilter
        public async Task<IActionResult> Index(UnitCriticalHitFilterViewModel request)
        {
            if (_context.PlayerUnitData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }

            // --- query data tables ---
            // TODO: try "var unitList = await _context.PlayerUnitData.Distinct().ToListAsync();" and replace other controllers
            var unitList = await (from pud in _context.PlayerUnitData select pud).Distinct().ToListAsync();
            var raceList = await (from rd in _context.RaceData select rd).Distinct().ToListAsync();
            var unitRaceList = await (from purd in _context.PlayerUnitRaceData select purd).Distinct().ToListAsync();
            var shotList = await (from pusd in _context.PlayerUnitShotData select pusd).Distinct().ToListAsync();
            var spellcardList = await (from puscd in _context.PlayerUnitSpellcardData select puscd).Distinct().ToListAsync();
            var bulletList = await (from pubd in _context.PlayerUnitBulletData select pubd).Distinct().ToListAsync();
            var bulletCriticalRaceList = await (from pubcrd in _context.PlayerUnitBulletCriticalRaceData select pubcrd).Distinct().ToListAsync();

            // TODO: try "Dictionary<int, PlayerUnitShotData> shotDict = shotList.ToDictionary(x => x.id, x => x);" and replace other controllers
            Dictionary<int, PlayerUnitShotData> shotDict = new();
            foreach (var shotRecord in shotList)
                shotDict[shotRecord.id] = shotRecord;

            Dictionary<int, PlayerUnitSpellcardData> spellcardDict = new();
            foreach (var spellcardRecord in spellcardList)
                spellcardDict[spellcardRecord.id] = spellcardRecord;

            Dictionary<int, PlayerUnitBulletData> bulletDict = new();
            foreach (var bulletRecord in bulletList)
                bulletDict[bulletRecord.id] = bulletRecord;

            Dictionary<int, string> raceDict = new();
            foreach (var raceRecord in raceList)
                raceDict[raceRecord.id] = raceRecord.name;

            Dictionary<int, List<int>> bulletCriticalRacesDict = new();
            foreach (var bulletCriticalRaceRecord in bulletCriticalRaceList)
            {
                // TODO: search for simplier statements
                if (!bulletCriticalRacesDict.ContainsKey(bulletCriticalRaceRecord.bullet_id))
                    bulletCriticalRacesDict[bulletCriticalRaceRecord.bullet_id] = new List<int>();
                bulletCriticalRacesDict[bulletCriticalRaceRecord.bullet_id].Add(bulletCriticalRaceRecord.race_id);
            }
            // ------ query end ------


            List<UnitRaceDisplayModel> queryUnits = new();
            List<UnitCriticalHitDisplayModel> criticalMatchUnits = new();

            HashSet<int> targetRaceIds = new();
            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                foreach (var unitRecord in unitList)
                {
                    string curUnitSymbolName = unitRecord.name + unitRecord.symbol_name;
                    if (!request.UnitSymbolName.Equals(curUnitSymbolName))
                        continue;
                    targetRaceIds = UnitRaceHelper.GetRaceIdsOfUnit(unitRecord, unitRaceList);
                    string racesStr = UnitRaceHelper.CreateRacesStringOfUnit(targetRaceIds, raceList);
                    queryUnits.Add(new UnitRaceDisplayModel(unitRecord, racesStr));
                }
            }
            else if (request.RaceName != null && request.RaceName.Length > 0)
                targetRaceIds = new(from raceRecord in raceList where raceRecord.name.Equals(request.RaceName) select raceRecord.id);

            if (targetRaceIds.Count > 0)
            {
                foreach (var unitRecord in unitList)
                {
                    UnitCriticalHitDisplayModel? unitCriticalHitData = CreateUnitCriticalHitDisplayModel(unitRecord, request.CreateAttackSelectionModel(),
                                                                                                         targetRaceIds, shotDict, spellcardDict, bulletDict,
                                                                                                         bulletCriticalRacesDict, raceDict);
                    if (unitCriticalHitData != null)
                        criticalMatchUnits.Add(unitCriticalHitData);
                }
            }

            criticalMatchUnits.Sort(delegate (UnitCriticalHitDisplayModel pd1, UnitCriticalHitDisplayModel pd2)
            {
                return (-pd1.TotalScore).CompareTo(-pd2.TotalScore);
            });

            request.QueryUnits = queryUnits;
            request.CriticalMatchUnits = criticalMatchUnits;
            request.AllRacesStr = string.Join(", ", from rd in raceList select rd.name);

            return View(request);
        }

        static UnitCriticalHitDisplayModel? CreateUnitCriticalHitDisplayModel(PlayerUnitData unitRecord, AttackSelectionModel attackSelection,
                                                                              HashSet<int> targetRaceIds,
                                                                              Dictionary<int, PlayerUnitShotData> shotDict,
                                                                              Dictionary<int, PlayerUnitSpellcardData> spellcardDict,
                                                                              Dictionary<int, PlayerUnitBulletData> bulletDict,
                                                                              Dictionary<int, List<int>> bulletCriticalRacesDict,
                                                                              Dictionary<int, string> raceDict)
        {
            List<AttackWithWeightModel> attacks = GetUnitAttacksWithWeight(unitRecord, attackSelection, shotDict, spellcardDict);
            double totalScore = 0;
            List<AttackCriticalHitInfo> UnitAttackCriticalHitList = new();
            foreach (var attack in attacks)
            {
                AttackCriticalHitInfo? unitAttackCriticalHit = SearchCriticalRaceInAttack(unitRecord, attack, targetRaceIds, bulletDict, bulletCriticalRacesDict, raceDict, ref totalScore);
                if (unitAttackCriticalHit != null)
                    UnitAttackCriticalHitList.Add(unitAttackCriticalHit);
            }
            if (UnitAttackCriticalHitList.Count > 0)
                return new UnitCriticalHitDisplayModel(unitRecord, UnitAttackCriticalHitList, totalScore);
            return null;
        }

        static AttackCriticalHitInfo? SearchCriticalRaceInAttack(PlayerUnitData unitRecord, AttackWithWeightModel attack, HashSet<int> targetRaceIds,
                                                                 Dictionary<int, PlayerUnitBulletData> bulletDict, Dictionary<int, List<int>> bulletCriticalRacesDict,
                                                                 Dictionary<int, string> raceDict, ref double totalScore)
        {
            List<MagazineCriticalHitInfo> criticalHits = new();
            foreach (var magazine in attack.AttackData.Magazines)
            {
                MagazineCriticalHitInfo? criticalHit = SearchCriticalRaceInMagazine(unitRecord, attack, magazine, targetRaceIds, bulletDict,
                                                                                    bulletCriticalRacesDict, raceDict, ref totalScore);
                if (criticalHit != null)
                    criticalHits.Add(criticalHit);
            }
            if (criticalHits.Count > 0)
                return new AttackCriticalHitInfo(attack.AttackData, criticalHits);
            return null;
        }

        static MagazineCriticalHitInfo? SearchCriticalRaceInMagazine(PlayerUnitData unitRecord, AttackWithWeightModel attack, BulletMagazineModel magazine,
                                                                     HashSet<int> targetRaceIds, Dictionary<int, PlayerUnitBulletData> bulletDict,
                                                                     Dictionary<int, List<int>> bulletCriticalRacesDict, Dictionary<int, string> raceDict, ref double totalScore)
        {
            int bulletId = magazine.BulletId;
            if (!bulletDict.ContainsKey(bulletId))
                return null;

            List<string> foundCriticalRaceList = new();
            if (bulletCriticalRacesDict.TryGetValue(bulletId, out List<int>? bulletCriticalRaceIds))
            {
                foreach (int raceId in bulletCriticalRaceIds)
                    if (targetRaceIds.Contains(raceId))
                        foundCriticalRaceList.Add(raceDict[raceId]);
            }
            bool magazineIsCriticalHit = (foundCriticalRaceList.Count > 0);
            totalScore += CalcBulletPower(magazine, bulletDict[bulletId], unitRecord, attack.AttackData.PowerUpRates[5], attack.AttackWeight, magazineIsCriticalHit);
            if (magazineIsCriticalHit)
                return new MagazineCriticalHitInfo(magazine.MagazineId, string.Join(", ", foundCriticalRaceList));
            return null;
        }
    }
}
