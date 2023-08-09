using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Linq;
using System.Net;
using THLWToolBox.Data;
using THLWToolBox.Helpers;
using THLWToolBox.Models;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Models.GeneralModels;

namespace THLWToolBox.Controllers
{
    public class PlayerUnitCriticalHitFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        public PlayerUnitCriticalHitFilter(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: PlayerUnitCriticalHitFilter
        public async Task<IActionResult> Index(bool? Shot1, bool? Shot2, bool? NormalSpellcard, bool? LastWord, string? UnitSymbolName, string? RaceName)
        {
            if (_context.PlayerUnitData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }
            var playerUnitDatas = from pud in _context.PlayerUnitData
                                  select pud;
            var playerUnitDatasList = await playerUnitDatas.Distinct().ToListAsync();

            var raceDatas = from rd in _context.RaceData
                            select rd;
            var raceDataList = await raceDatas.Distinct().ToListAsync();

            var playerUnitRaceDatas = from purd in _context.PlayerUnitRaceData
                                      select purd;
            var playerUnitRaceDataList = await playerUnitRaceDatas.Distinct().ToListAsync();

            var playerUnitShotDatas = from pusd in _context.PlayerUnitShotData
                                      select pusd;
            var playerUnitShotDataList = await playerUnitShotDatas.Distinct().ToListAsync();

            var playerUnitSpellcardDatas = from puscd in _context.PlayerUnitSpellcardData
                                           select puscd;
            var playerUnitSpellcardDataList = await playerUnitSpellcardDatas.Distinct().ToListAsync();

            var playerUnitBulletDatas = from pubd in _context.PlayerUnitBulletData
                                        select pubd;
            var playerUnitBulletDatasList = await playerUnitBulletDatas.Distinct().ToListAsync();

            var playerUnitBulletCriticalRaceDatas = from pubcrd in _context.PlayerUnitBulletCriticalRaceData
                                                    select pubcrd;
            var playerUnitBulletCriticalRaceDataList = await playerUnitBulletCriticalRaceDatas.Distinct().ToListAsync();

            Dictionary<int, PlayerUnitShotData> shotDataDict = new();
            Dictionary<int, PlayerUnitSpellcardData> spellcardDataDict = new();
            Dictionary<int, PlayerUnitBulletData> bulletDataDict = new();
            Dictionary<int, List<int>> bulletToRaceDict = new();
            Dictionary<int, string> raceDataDict = new();

            foreach (var pusd in playerUnitShotDataList)
                shotDataDict[pusd.id] = pusd;
            foreach (var puscd in playerUnitSpellcardDataList)
                spellcardDataDict[puscd.id] = puscd;
            foreach (var pubd in playerUnitBulletDatasList)
                bulletDataDict[pubd.id] = pubd;
            foreach (var pubcrd in playerUnitBulletCriticalRaceDataList)
            {
                if (!bulletToRaceDict.ContainsKey(pubcrd.bullet_id))
                    bulletToRaceDict[pubcrd.bullet_id] = new List<int>();
                bulletToRaceDict[pubcrd.bullet_id].Add(pubcrd.race_id);
            }
            foreach (var rd in raceDataList)
                raceDataDict[rd.id] = rd.name;

            HashSet<int> targetRaceIds = new();

            List<UnitRaceDisplayModel> QueryUnit = new();

            if (UnitSymbolName != null && UnitSymbolName.Length > 0)
            {
                foreach (var pud in playerUnitDatasList)
                {
                    if (UnitSymbolName.Equals(pud.name + pud.symbol_name))
                    {
                        List<string> queryRaces = new();
                        foreach (var purd in playerUnitRaceDataList)
                        {
                            if (purd.unit_id == pud.id)
                            {
                                targetRaceIds.Add(purd.race_id);
                                queryRaces.Add(raceDataDict[purd.race_id]);
                            }
                        }
                        QueryUnit.Add(new UnitRaceDisplayModel(pud, string.Join(", ", queryRaces)));
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
                        targetRaceIds.Add(raceId);
                    }
                }
            }

            List<PlayerUnitCriticalDisplayModel> displayUnitCriticalDatas = new();

            if (targetRaceIds.Count > 0)
            {
                foreach (var pud in playerUnitDatas)
                {
                    List<Tuple<string, List<string>>> unitBulletList = new();
                    double TotalScore = 0;
                    bool Found = FilterByBullet(pud, targetRaceIds, Shot1, Shot2, NormalSpellcard, LastWord, shotDataDict, spellcardDataDict, bulletDataDict, bulletToRaceDict, raceDataDict, ref unitBulletList, ref TotalScore);
                    if (Found)
                        displayUnitCriticalDatas.Add(new PlayerUnitCriticalDisplayModel(pud, unitBulletList, TotalScore));
                }
            }
            displayUnitCriticalDatas.Sort(delegate (PlayerUnitCriticalDisplayModel pd1, PlayerUnitCriticalDisplayModel pd2)
            {
                return (-pd1.TotalScore).CompareTo(-pd2.TotalScore);
            });


            var playerUnitCriticalFilterVM = new PlayerUnitCriticalFilterModel
            {
                QueryUnit = QueryUnit,
                RaceList = String.Join(", ", GetRaces()),
                CriticalMatchUnitResults = displayUnitCriticalDatas,
                UnitSymbolName = UnitSymbolName,
                RaceName = RaceName,
                Shot1 = Shot1,
                Shot2 = Shot2,
                NormalSpellcard = NormalSpellcard,
                LastWord = LastWord
            };
            return View(playerUnitCriticalFilterVM);
        }

        static bool FilterByBullet(PlayerUnitData pud, HashSet<int> targetRaceIds, bool? Shot1, bool? Shot2, bool? NormalSpellcard, bool? LastWord, Dictionary<int, PlayerUnitShotData> shotDataDict, Dictionary<int, PlayerUnitSpellcardData> spellcardDataDict, Dictionary<int, PlayerUnitBulletData> bulletDataDict, Dictionary<int, List<int>> bulletToRaceDict, Dictionary<int, string> raceDataDict, ref List<Tuple<string, List<string>>> unitBulletList, ref double TotalScore)
        {
            TotalScore = 0;
            bool Found = false;
            List<Tuple<PlayerUnitShotData, string, double>> shots = new();
            List<Tuple<PlayerUnitSpellcardData, string, double>> scs = new();

            if (Shot1 == null || Shot1.GetValueOrDefault() == true)
                shots.Add(new Tuple<PlayerUnitShotData, string, double>(shotDataDict[pud.shot1_id], "扩散", 1.0));
            if (Shot2 == null || Shot2.GetValueOrDefault() == true)
                shots.Add(new Tuple<PlayerUnitShotData, string, double>(shotDataDict[pud.shot2_id], "集中", 1.2));

            foreach (var shot in shots)
                TotalScore += SearchCriticalRaceInShot(targetRaceIds, shot.Item1, shot.Item2, shot.Item3, pud, bulletDataDict, bulletToRaceDict, raceDataDict, ref Found, ref unitBulletList);

            if (NormalSpellcard == null || NormalSpellcard.GetValueOrDefault() == true)
            {
                scs.Add(new Tuple<PlayerUnitSpellcardData, string, double>(spellcardDataDict[pud.spellcard1_id], "一符", 3.0));
                scs.Add(new Tuple<PlayerUnitSpellcardData, string, double>(spellcardDataDict[pud.spellcard2_id], "二符", 3.0));
            }
            if (LastWord == null || LastWord.GetValueOrDefault() == true)
                scs.Add(new Tuple<PlayerUnitSpellcardData, string, double>(spellcardDataDict[pud.spellcard5_id], "终符", 5.0));

            foreach (var sc in scs)
                TotalScore += SearchCriticalRaceInSpellCard(targetRaceIds, sc.Item1, sc.Item2, sc.Item3, pud, bulletDataDict, bulletToRaceDict, raceDataDict, ref Found, ref unitBulletList);

            return Found;
        }

        static double SearchCriticalRaceInShot(HashSet<int> targetRaceIds, PlayerUnitShotData pusd, string name, double shotTypeWeight, PlayerUnitData pud, Dictionary<int, PlayerUnitBulletData> bulletDataDict, Dictionary<int, List<int>> bulletToRaceDict, Dictionary<int, string> raceDataDict, ref bool Found, ref List<Tuple<string, List<string>>> unitBulletList)
        {
            int level5PowerRate = pusd.shot_level5_power_rate;
            List<BulletMagazineModel> bulletMagazineList = new List<BulletMagazineModel> {
                new BulletMagazineModel(pusd.magazine0_bullet_id, pusd.magazine0_bullet_range, pusd.magazine0_bullet_value, pusd.magazine0_bullet_power_rate),
                new BulletMagazineModel(pusd.magazine1_bullet_id, pusd.magazine1_bullet_range, pusd.magazine1_bullet_value, pusd.magazine1_bullet_power_rate),
                new BulletMagazineModel(pusd.magazine2_bullet_id, pusd.magazine2_bullet_range, pusd.magazine2_bullet_value, pusd.magazine2_bullet_power_rate),
                new BulletMagazineModel(pusd.magazine3_bullet_id, pusd.magazine3_bullet_range, pusd.magazine3_bullet_value, pusd.magazine3_bullet_power_rate),
                new BulletMagazineModel(pusd.magazine4_bullet_id, pusd.magazine4_bullet_range, pusd.magazine4_bullet_value, pusd.magazine4_bullet_power_rate),
                new BulletMagazineModel(pusd.magazine5_bullet_id, pusd.magazine5_bullet_range, pusd.magazine5_bullet_value, pusd.magazine5_bullet_power_rate) };
            List<string> searchResult = new();
            double score = SearchCriticalRaceInBulletList(targetRaceIds, bulletMagazineList, shotTypeWeight, pud, level5PowerRate, bulletDataDict, bulletToRaceDict, raceDataDict, ref Found, ref searchResult);
            if (searchResult.Count > 0)
                unitBulletList.Add(new Tuple<string, List<string>>(name, searchResult));
            return score;
        }

        static double SearchCriticalRaceInSpellCard(HashSet<int> targetRaceIds, PlayerUnitSpellcardData puscd, string name, double shotTypeWeight, PlayerUnitData pud, Dictionary<int, PlayerUnitBulletData> bulletDataDict, Dictionary<int, List<int>> bulletToRaceDict, Dictionary<int, string> raceDataDict, ref bool Found, ref List<Tuple<string, List<string>>> unitBulletList)
        {
            int level5PowerRate = puscd.shot_level5_power_rate;
            List<BulletMagazineModel> bulletMagazineList = new List<BulletMagazineModel> {
                new BulletMagazineModel(puscd.magazine0_bullet_id, puscd.magazine0_bullet_range, puscd.magazine0_bullet_value, puscd.magazine0_bullet_power_rate),
                new BulletMagazineModel(puscd.magazine1_bullet_id, puscd.magazine1_bullet_range, puscd.magazine1_bullet_value, puscd.magazine1_bullet_power_rate),
                new BulletMagazineModel(puscd.magazine2_bullet_id, puscd.magazine2_bullet_range, puscd.magazine2_bullet_value, puscd.magazine2_bullet_power_rate),
                new BulletMagazineModel(puscd.magazine3_bullet_id, puscd.magazine3_bullet_range, puscd.magazine3_bullet_value, puscd.magazine3_bullet_power_rate),
                new BulletMagazineModel(puscd.magazine4_bullet_id, puscd.magazine4_bullet_range, puscd.magazine4_bullet_value, puscd.magazine4_bullet_power_rate),
                new BulletMagazineModel(puscd.magazine5_bullet_id, puscd.magazine5_bullet_range, puscd.magazine5_bullet_value, puscd.magazine5_bullet_power_rate) };
            List<string> searchResult = new();
            double score = SearchCriticalRaceInBulletList(targetRaceIds, bulletMagazineList, shotTypeWeight, pud, level5PowerRate, bulletDataDict, bulletToRaceDict, raceDataDict, ref Found, ref searchResult);
            if (searchResult.Count > 0)
                unitBulletList.Add(new Tuple<string, List<string>>(name, searchResult));
            return score;
        }

        static double SearchCriticalRaceInBulletList(HashSet<int> targetRaceIds, List<BulletMagazineModel> bulletMagazineList, double shotTypeWeight, PlayerUnitData pud, int level5PowerRate, Dictionary<int, PlayerUnitBulletData> bulletDataDict, Dictionary<int, List<int>> bulletToRaceDict, Dictionary<int, string> raceDataDict, ref bool Found, ref List<string> searchResult)
        {
            double score = 0;
            searchResult.Clear();
            int index = 1;
            foreach (var bulletMagazine in bulletMagazineList)
            {
                int bulletId = bulletMagazine.bullet_id;
                if (!bulletDataDict.ContainsKey(bulletId))
                    continue;

                bool IsCriticalRace = false;
                List<int>? bulletCriticalRaceList = new();
                List<string> foundCriticalRaceList = new();
                if (bulletToRaceDict.TryGetValue(bulletId, out bulletCriticalRaceList))
                {
                    foreach (int raceId in bulletCriticalRaceList)
                    {
                        if (targetRaceIds.Contains(raceId))
                        {
                            IsCriticalRace = true;
                            foundCriticalRaceList.Add(raceDataDict[raceId]);
                        }
                    }
                }

                if (IsCriticalRace)
                {
                    Found = true;
                    //searchResult.Add("第" + Convert.ToString(index) + "段对 " + string.Join(",", foundCriticalRaceList) + " 特攻");
                    string RaceListStr = "<b><font color=#FF6600>" + string.Join(",", foundCriticalRaceList) + "</font></b>";
                    searchResult.Add("第" + Convert.ToString(index) + "段对" + RaceListStr + "特攻");
                }

                score += GeneralHelper.CalcBulletPower(bulletMagazine, bulletDataDict[bulletId], pud, level5PowerRate, shotTypeWeight, IsCriticalRace);

                index += 1;
            }
            return score;
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
