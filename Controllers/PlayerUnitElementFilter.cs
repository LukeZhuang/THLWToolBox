using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using THLWToolBox.Data;
using THLWToolBox.Models;
using static THLWToolBox.Models.GeneralTypeMaster;

namespace THLWToolBox.Controllers
{
    public class PlayerUnitElementFilter : Controller
    {

        private const double spreadShotWeight = 1.0;
        private const double focusShotWeight = 1.2;
        private readonly THLWToolBoxContext _context;

        public PlayerUnitElementFilter(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: PlayerUnitElementFilter
        public async Task<IActionResult> Index(int? ShotType, int? MainBulletElement, int? MainBulletCategory, int? MainBulletType, int? SubBulletElement, int? SubBulletCategory, int? SubBulletType)
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

            bool calcShot = (ShotType.GetValueOrDefault(0) <= 0);
            bool calcSmallSC = (ShotType.GetValueOrDefault(0) <= 1);
            bool calcLastSC = true;

            List<PlayerUnitElementDisplayModel> displayUnitElementDatas = new();
            if (MainBulletElement != null || MainBulletCategory != null || SubBulletElement != null || SubBulletCategory != null)
            {
                foreach (var pud in playerUnitDatas)
                {
                    bool Found = true;
                    double TotalMainScore = 0;
                    double TotalSubScore = 0;
                    List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> unitBulletList = new();

                    if (MainBulletElement != null || MainBulletCategory != null)
                        Found &= FilterByBullet(pud, 1, MainBulletElement, MainBulletCategory, MainBulletType, calcShot, calcSmallSC, calcLastSC, shotDataDict, spellcardDataDict, bulletDataDict, ref TotalMainScore, ref unitBulletList);
                    if (SubBulletElement != null || SubBulletCategory != null)
                        Found &= FilterByBullet(pud, 2, SubBulletElement, SubBulletCategory, SubBulletType, calcShot, calcSmallSC, calcLastSC, shotDataDict, spellcardDataDict, bulletDataDict, ref TotalSubScore, ref unitBulletList);

                    if (Found)
                        displayUnitElementDatas.Add(new PlayerUnitElementDisplayModel(pud, unitBulletList, TotalMainScore, TotalSubScore));
                }
                displayUnitElementDatas.Sort(delegate (PlayerUnitElementDisplayModel pd1, PlayerUnitElementDisplayModel pd2)
                {
                    return (-pd1.MainScore - pd1.SubScore).CompareTo(-pd2.MainScore - pd2.SubScore);
                    //if (pd1.MainScore == pd2.MainScore)
                    //    return (-pd1.SubScore).CompareTo(-pd2.SubScore);
                    //else
                    //    return (-pd1.MainScore).CompareTo(-pd2.MainScore);
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
                SubBulletType = SubBulletType
            };
            return View(playerUnitElementFilterVM);
        }

        public bool FilterByBullet(PlayerUnitData pud, int FilterId, int? BulletElement, int? BulletCategory, int? BulletType, bool calcShot, bool calcSmallSC, bool calcLastSC, Dictionary<int, PlayerUnitShotData> shotDataDict, Dictionary<int, PlayerUnitSpellcardData> spellcardDataDict, Dictionary<int, PlayerUnitBulletData> bulletDataDict, ref double TotalScore, ref List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> unitBulletList)
        {
            bool Found = false;
            List<Tuple<PlayerUnitShotData, string, double>> shots = new();
            List<Tuple<PlayerUnitSpellcardData, string, double>> scs = new();

            if (calcShot)
            {
                shots.Add(new Tuple<PlayerUnitShotData, string, double>(shotDataDict[pud.shot1_id], "扩散", 1.0));
                shots.Add(new Tuple<PlayerUnitShotData, string, double>(shotDataDict[pud.shot2_id], "集中", 1.2));
            }

            foreach (var shot in shots)
                TotalScore += CalcShotElementScore(FilterId, shot.Item1, shot.Item2, shot.Item3, bulletDataDict, pud.yang_attack, pud.yin_attack, BulletElement, BulletCategory, BulletType, ref Found, ref unitBulletList);


            if (calcSmallSC)
            {
                scs.Add(new Tuple<PlayerUnitSpellcardData, string, double>(spellcardDataDict[pud.spellcard1_id], "一符", 3.0));
                scs.Add(new Tuple<PlayerUnitSpellcardData, string, double>(spellcardDataDict[pud.spellcard2_id], "二符", 3.0));
            }
            if (calcLastSC)
                scs.Add(new Tuple<PlayerUnitSpellcardData, string, double>(spellcardDataDict[pud.spellcard5_id], "终符", 5.0));

            foreach (var sc in scs)
                TotalScore += CalcSpellcardElementScore(FilterId, sc.Item1, sc.Item2, sc.Item3, bulletDataDict, pud.yang_attack, pud.yin_attack, BulletElement, BulletCategory, BulletType, ref Found, ref unitBulletList);

            return Found;
        }

        public double CalcShotElementScore(int FilterId, PlayerUnitShotData pusd, string name, double weight, Dictionary<int, PlayerUnitBulletData> bulletDataDict, int yangATK, int yinATK, int? BulletElement, int? BulletCategory, int? BulletType, ref bool Found, ref List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> unitBulletList)
        {
            int level5PowerRate = pusd.shot_level5_power_rate;
            List<SingleBulletInfo> bulletList = new List<SingleBulletInfo> {
                new SingleBulletInfo(pusd.magazine0_bullet_id, pusd.magazine0_bullet_range, pusd.magazine0_bullet_value, pusd.magazine0_bullet_power_rate),
                new SingleBulletInfo(pusd.magazine1_bullet_id, pusd.magazine1_bullet_range, pusd.magazine1_bullet_value, pusd.magazine1_bullet_power_rate),
                new SingleBulletInfo(pusd.magazine2_bullet_id, pusd.magazine2_bullet_range, pusd.magazine2_bullet_value, pusd.magazine2_bullet_power_rate),
                new SingleBulletInfo(pusd.magazine3_bullet_id, pusd.magazine3_bullet_range, pusd.magazine3_bullet_value, pusd.magazine3_bullet_power_rate),
                new SingleBulletInfo(pusd.magazine4_bullet_id, pusd.magazine4_bullet_range, pusd.magazine4_bullet_value, pusd.magazine4_bullet_power_rate),
                new SingleBulletInfo(pusd.magazine5_bullet_id, pusd.magazine5_bullet_range, pusd.magazine5_bullet_value, pusd.magazine5_bullet_power_rate) };
            return CalcBulletListScore(FilterId, name, weight, bulletList, bulletDataDict, yangATK, yinATK, level5PowerRate, BulletElement, BulletCategory, BulletType, ref Found, ref unitBulletList);
        }

        public double CalcSpellcardElementScore(int FilterId, PlayerUnitSpellcardData puscd, string name, double weight, Dictionary<int, PlayerUnitBulletData> bulletDataDict, int yangATK, int yinATK, int? BulletElement, int? BulletCategory, int? BulletType, ref bool Found, ref List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> unitBulletList)
        {
            int level5PowerRate = puscd.shot_level5_power_rate;
            List<SingleBulletInfo> bulletList = new List<SingleBulletInfo> {
                new SingleBulletInfo(puscd.magazine0_bullet_id, puscd.magazine0_bullet_range, puscd.magazine0_bullet_value, puscd.magazine0_bullet_power_rate),
                new SingleBulletInfo(puscd.magazine1_bullet_id, puscd.magazine1_bullet_range, puscd.magazine1_bullet_value, puscd.magazine1_bullet_power_rate),
                new SingleBulletInfo(puscd.magazine2_bullet_id, puscd.magazine2_bullet_range, puscd.magazine2_bullet_value, puscd.magazine2_bullet_power_rate),
                new SingleBulletInfo(puscd.magazine3_bullet_id, puscd.magazine3_bullet_range, puscd.magazine3_bullet_value, puscd.magazine3_bullet_power_rate),
                new SingleBulletInfo(puscd.magazine4_bullet_id, puscd.magazine4_bullet_range, puscd.magazine4_bullet_value, puscd.magazine4_bullet_power_rate),
                new SingleBulletInfo(puscd.magazine5_bullet_id, puscd.magazine5_bullet_range, puscd.magazine5_bullet_value, puscd.magazine5_bullet_power_rate) };
            return CalcBulletListScore(FilterId, name, weight, bulletList, bulletDataDict, yangATK, yinATK, level5PowerRate, BulletElement, BulletCategory, BulletType, ref Found, ref unitBulletList);
        }

        public double CalcBulletListScore(int FilterId, string name, double weight, List<SingleBulletInfo> bulletList, Dictionary<int, PlayerUnitBulletData> bulletDataDict, int yangATK, int yinATK, int level5PowerRate, int? BulletElement, int? BulletCategory, int? BulletType, ref bool Found, ref List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> unitBulletList)
        {
            double BulletListScore = 0.0;

            // find target shot row
            int id = -1;
            for (int i = 0; i < unitBulletList.Count; i++)
            {
                if (unitBulletList[i].Item1 == name)
                {
                    id = i;
                    break;
                }
            }
            if (id == -1)
            {
                List<Tuple<PlayerUnitBulletData?, int>> bulletRow = new();
                for (int j = 0; j < bulletList.Count; j++)
                    bulletRow.Add(new Tuple<PlayerUnitBulletData?, int>(null, 0));
                unitBulletList.Add(new Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>(name, bulletRow));
                id = unitBulletList.Count - 1;
            }

            // about the second item (that "int") in the list, this integer record the bullet "status"
            // lowest bit means "yin"/"yang"
            // 2nd bit means selected by main element
            // 3rd bit means selected by sub element

            for (int j = 0; j < bulletList.Count; j++)
            {
                SingleBulletInfo bullet = bulletList[j];

                if (bullet.bullet_id == 0)
                    continue;

                PlayerUnitBulletData bulletRecord = bulletDataDict[bullet.bullet_id];
                int bulletStatus = unitBulletList[id].Item2[j].Item2;
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
                    double atk;
                    if (bulletRecord.type == 0)
                        atk = yinATK;
                    else if (bulletRecord.type == 1)
                        atk = yangATK;
                    else
                        throw new NotImplementedException("unknown bullet type");

                    BulletListScore += (atk / 1000.0) * (bullet.bullet_power_rate / 100.0) * bullet.bullet_value * (bulletRecord.hit / 100.0) * (1 + bulletRecord.critical / 100.0) * (level5PowerRate / 100.0) * (bullet.bullet_range == 2 ? 1.5 : 1.0) * weight;
                    bulletStatus |= (1 << FilterId);

                    Found = true;
                }

                unitBulletList[id].Item2[j] = new Tuple<PlayerUnitBulletData?, int>(bulletRecord, bulletStatus);
            }
            return BulletListScore;
        }
    }
}
