using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using THLWToolBox.Data;
using static THLWToolBox.Models.GeneralModels;
using THLWToolBox.Helpers;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;

namespace THLWToolBox.Controllers
{
    public class PlayerUnitElementFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        public PlayerUnitElementFilter(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: PlayerUnitElementFilter
        public async Task<IActionResult> Index(bool? Shot1, bool? Shot2, bool? NormalSpellcard, bool? LastWord, int? MainBulletElement, int? MainBulletCategory, int? MainBulletType, int? SubBulletElement, int? SubBulletCategory, int? SubBulletType)
        {
            if (_context.PlayerUnitData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }

            var playerUnitDatas = from pud in _context.PlayerUnitData
                                  select pud;
            var playerUnitShotDatas = from pusd in _context.PlayerUnitShotData
                                  select pusd;
            var playerUnitSpellcardDatas = from puscd in _context.PlayerUnitSpellcardData
                                      select puscd;
            var playerUnitBulletDatas = from pubd in _context.PlayerUnitBulletData
                                           select pubd;

            Dictionary<int, PlayerUnitShotData> shotDataDict = new Dictionary<int, PlayerUnitShotData>();
            Dictionary<int, PlayerUnitSpellcardData> spellcardDataDict = new Dictionary<int, PlayerUnitSpellcardData>();
            Dictionary<int, PlayerUnitBulletData> bulletDataDict = new Dictionary<int, PlayerUnitBulletData>();
            foreach (var pusd in playerUnitShotDatas)
                shotDataDict[pusd.id] = pusd;
            foreach (var puscd in playerUnitSpellcardDatas)
                spellcardDataDict[puscd.id] = puscd;
            foreach (var pubd in playerUnitBulletDatas)
                bulletDataDict[pubd.id] = pubd;

            List<PlayerUnitElementDisplayModel> displayUnitElementDatas = new();
            if (MainBulletElement != null || MainBulletCategory != null || SubBulletElement != null || SubBulletCategory != null)
            {
                foreach (var pud in playerUnitDatas)
                {
                    bool Found = true;
                    double TotalMainScore = 0;
                    double TotalSubScore = 0;
                    Dictionary<string, List<Tuple<PlayerUnitBulletData?, int>>> unitBulletDict = new();

                    if (MainBulletElement != null || MainBulletCategory != null)
                        Found &= FilterByBullet(pud, 1, MainBulletElement, MainBulletCategory, MainBulletType, Shot1, Shot2, NormalSpellcard, LastWord, shotDataDict, spellcardDataDict, bulletDataDict, ref TotalMainScore, ref unitBulletDict);
                    if (SubBulletElement != null || SubBulletCategory != null)
                        Found &= FilterByBullet(pud, 2, SubBulletElement, SubBulletCategory, SubBulletType, Shot1, Shot2, NormalSpellcard, LastWord, shotDataDict, spellcardDataDict, bulletDataDict, ref TotalSubScore, ref unitBulletDict);

                    if (Found)
                        displayUnitElementDatas.Add(new PlayerUnitElementDisplayModel(pud, SortBulletList(unitBulletDict), TotalMainScore, TotalSubScore));
                }
                displayUnitElementDatas.Sort(delegate (PlayerUnitElementDisplayModel pd1, PlayerUnitElementDisplayModel pd2)
                {
                    return (-pd1.MainScore - pd1.SubScore).CompareTo(-pd2.MainScore - pd2.SubScore);
                });
            }
            var playerUnitElementFilterVM = new PlayerUnitElementFilterModel
            {
                QueryResults = displayUnitElementDatas,
                MainBulletElement = MainBulletElement,
                MainBulletCategory = MainBulletCategory,
                MainBulletType = MainBulletType,
                SubBulletElement = SubBulletElement,
                SubBulletCategory = SubBulletCategory,
                SubBulletType = SubBulletType,
                Shot1 = Shot1,
                Shot2 = Shot2,
                NormalSpellcard = NormalSpellcard,
                LastWord = LastWord
            };
            return View(playerUnitElementFilterVM);
        }

        public bool FilterByBullet(PlayerUnitData pud, int FilterId, int? BulletElement, int? BulletCategory, int? BulletType, bool? Shot1, bool? Shot2, bool? NormalSpellcard, bool? LastWord, Dictionary<int, PlayerUnitShotData> shotDataDict, Dictionary<int, PlayerUnitSpellcardData> spellcardDataDict, Dictionary<int, PlayerUnitBulletData> bulletDataDict, ref double TotalScore, ref Dictionary<string, List<Tuple<PlayerUnitBulletData?, int>>> unitBulletDict)
        {
            bool Found = false;
            List<Tuple<PlayerUnitShotData, string, double>> shots = new();
            List<Tuple<PlayerUnitSpellcardData, string, double>> scs = new();

            if (Shot1 == null || Shot1.GetValueOrDefault() == true)
                shots.Add(new Tuple<PlayerUnitShotData, string, double>(shotDataDict[pud.shot1_id], "扩散", 1.0));
            if (Shot2 == null || Shot2.GetValueOrDefault() == true)
                shots.Add(new Tuple<PlayerUnitShotData, string, double>(shotDataDict[pud.shot2_id], "集中", 1.2));

            foreach (var shot in shots)
                TotalScore += CalcShotElementScore(FilterId, shot.Item1, shot.Item2, shot.Item3, bulletDataDict, pud, BulletElement, BulletCategory, BulletType, ref Found, ref unitBulletDict);

            if (NormalSpellcard == null || NormalSpellcard.GetValueOrDefault() == true)
            {
                scs.Add(new Tuple<PlayerUnitSpellcardData, string, double>(spellcardDataDict[pud.spellcard1_id], "一符", 3.0));
                scs.Add(new Tuple<PlayerUnitSpellcardData, string, double>(spellcardDataDict[pud.spellcard2_id], "二符", 3.0));
            }
            if (LastWord == null || LastWord.GetValueOrDefault() == true)
                scs.Add(new Tuple<PlayerUnitSpellcardData, string, double>(spellcardDataDict[pud.spellcard5_id], "终符", 5.0));

            foreach (var sc in scs)
                TotalScore += CalcSpellcardElementScore(FilterId, sc.Item1, sc.Item2, sc.Item3, bulletDataDict, pud, BulletElement, BulletCategory, BulletType, ref Found, ref unitBulletDict);

            return Found;
        }

        public double CalcShotElementScore(int FilterId, PlayerUnitShotData pusd, string name, double weight, Dictionary<int, PlayerUnitBulletData> bulletDataDict, PlayerUnitData pud, int? BulletElement, int? BulletCategory, int? BulletType, ref bool Found, ref Dictionary<string, List<Tuple<PlayerUnitBulletData?, int>>> unitBulletDict)
        {
            int level5PowerRate = pusd.shot_level5_power_rate;
            List<BulletMagazineModel> bulletMagazineList = new()
            {
                new BulletMagazineModel(1, pusd.magazine0_bullet_id, pusd.magazine0_bullet_range, pusd.magazine0_bullet_value, pusd.magazine0_bullet_power_rate, 0),
                new BulletMagazineModel(2, pusd.magazine1_bullet_id, pusd.magazine1_bullet_range, pusd.magazine1_bullet_value, pusd.magazine1_bullet_power_rate, pusd.magazine1_boost_count),
                new BulletMagazineModel(3, pusd.magazine2_bullet_id, pusd.magazine2_bullet_range, pusd.magazine2_bullet_value, pusd.magazine2_bullet_power_rate, pusd.magazine2_boost_count),
                new BulletMagazineModel(4, pusd.magazine3_bullet_id, pusd.magazine3_bullet_range, pusd.magazine3_bullet_value, pusd.magazine3_bullet_power_rate, pusd.magazine3_boost_count),
                new BulletMagazineModel(5, pusd.magazine4_bullet_id, pusd.magazine4_bullet_range, pusd.magazine4_bullet_value, pusd.magazine4_bullet_power_rate, pusd.magazine4_boost_count),
                new BulletMagazineModel(6, pusd.magazine5_bullet_id, pusd.magazine5_bullet_range, pusd.magazine5_bullet_value, pusd.magazine5_bullet_power_rate, pusd.magazine5_boost_count)
            };

            List<Tuple<PlayerUnitBulletData?, int>> searchResult;
            if (unitBulletDict.ContainsKey(name))
                searchResult = unitBulletDict[name];
            else
            {
                searchResult = new();
                for (int i = 0; i < bulletMagazineList.Count; i++)
                    searchResult.Add(new Tuple<PlayerUnitBulletData?, int>(null, 0));
            }

            double score = CalcBulletListScore(FilterId, name, weight, bulletMagazineList, bulletDataDict, pud, level5PowerRate, BulletElement, BulletCategory, BulletType, ref Found, ref searchResult);
            if (score > 0)
                unitBulletDict[name] = searchResult;
            return score;
        }

        public double CalcSpellcardElementScore(int FilterId, PlayerUnitSpellcardData puscd, string name, double weight, Dictionary<int, PlayerUnitBulletData> bulletDataDict, PlayerUnitData pud, int? BulletElement, int? BulletCategory, int? BulletType, ref bool Found, ref Dictionary<string, List<Tuple<PlayerUnitBulletData?, int>>> unitBulletDict)
        {
            int level5PowerRate = puscd.shot_level5_power_rate;
            List<BulletMagazineModel> bulletMagazineList = new()
            {
                new BulletMagazineModel(1, puscd.magazine0_bullet_id, puscd.magazine0_bullet_range, puscd.magazine0_bullet_value, puscd.magazine0_bullet_power_rate, 0),
                new BulletMagazineModel(2, puscd.magazine1_bullet_id, puscd.magazine1_bullet_range, puscd.magazine1_bullet_value, puscd.magazine1_bullet_power_rate, puscd.magazine1_boost_count),
                new BulletMagazineModel(3, puscd.magazine2_bullet_id, puscd.magazine2_bullet_range, puscd.magazine2_bullet_value, puscd.magazine2_bullet_power_rate, puscd.magazine2_boost_count),
                new BulletMagazineModel(4, puscd.magazine3_bullet_id, puscd.magazine3_bullet_range, puscd.magazine3_bullet_value, puscd.magazine3_bullet_power_rate, puscd.magazine3_boost_count),
                new BulletMagazineModel(5, puscd.magazine4_bullet_id, puscd.magazine4_bullet_range, puscd.magazine4_bullet_value, puscd.magazine4_bullet_power_rate, puscd.magazine4_boost_count),
                new BulletMagazineModel(6, puscd.magazine5_bullet_id, puscd.magazine5_bullet_range, puscd.magazine5_bullet_value, puscd.magazine5_bullet_power_rate, puscd.magazine5_boost_count)
            };
            
            List<Tuple<PlayerUnitBulletData?, int>> searchResult;
            if (unitBulletDict.ContainsKey(name))
                searchResult = unitBulletDict[name];
            else
            {
                searchResult = new();
                for (int i = 0; i < bulletMagazineList.Count; i++)
                    searchResult.Add(new Tuple<PlayerUnitBulletData?, int>(null, 0));
            }

            double score = CalcBulletListScore(FilterId, name, weight, bulletMagazineList, bulletDataDict, pud, level5PowerRate, BulletElement, BulletCategory, BulletType, ref Found, ref searchResult);
            if (score > 0)
                unitBulletDict[name] = searchResult;
            return score;
        }

        public double CalcBulletListScore(int FilterId, string name, double weight, List<BulletMagazineModel> bulletMagazineList, Dictionary<int, PlayerUnitBulletData> bulletDataDict, PlayerUnitData pud, int level5PowerRate, int? BulletElement, int? BulletCategory, int? BulletType, ref bool Found, ref List<Tuple<PlayerUnitBulletData?, int>> searchResult)
        {
            double BulletListScore = 0.0;

            // about the second item (that "int") in the list, this integer record the bullet "status"
            // lowest bit means "yin"/"yang"
            // 2nd bit means selected by main element
            // 3rd bit means selected by sub element

            for (int j = 0; j < bulletMagazineList.Count; j++)
            {
                BulletMagazineModel bulletMagazine = bulletMagazineList[j];

                int bulletId = bulletMagazine.BulletId;
                if (!bulletDataDict.ContainsKey(bulletId))
                    continue;

                PlayerUnitBulletData bulletRecord = bulletDataDict[bulletMagazine.BulletId];
                int bulletStatus = searchResult[j].Item2;
                if (bulletRecord.type != 0 && bulletRecord.type != 1)
                    throw new NotImplementedException("unknown bullet type");
                bulletStatus |= (bulletRecord.type & 1);

                bool match = true;
                if (BulletElement != null)
                    match &= (bulletRecord.element == BulletElement.GetValueOrDefault(0));
                if (BulletCategory != null)
                    match &= (bulletRecord.category == BulletCategory.GetValueOrDefault(0));
                if (BulletType != null)
                    match &= (BulletType == 0 || BulletType - 1 == bulletRecord.type);

                if (match)
                {
                    BulletListScore += GeneralHelper.CalcBulletPower(bulletMagazine, bulletRecord, pud, level5PowerRate, weight, false);
                    bulletStatus |= (1 << FilterId);

                    Found = true;
                }

                searchResult[j] = new Tuple<PlayerUnitBulletData?, int>(bulletRecord, bulletStatus);
            }
            return BulletListScore;
        }

        static List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> SortBulletList(Dictionary<string, List<Tuple<PlayerUnitBulletData?, int>>> unitBulletDict)
        {
            List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> result = new();
            List<string> shotNameOrder = new List<string> { "扩散", "集中", "一符", "二符", "终符" };
            foreach (var shotName in shotNameOrder)
                if (unitBulletDict.ContainsKey(shotName))
                    result.Add(new Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>(shotName, unitBulletDict[shotName]));
            return result;
        }
    }
}
