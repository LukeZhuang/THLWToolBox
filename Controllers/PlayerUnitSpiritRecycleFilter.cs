using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Numpy;
using Python.Runtime;
using THLWToolBox.Data;
using THLWToolBox.Models;

namespace THLWToolBox.Controllers
{
    public class PlayerUnitSpiritRecycleFilter : Controller
    {
        private const int MONTE_CARLO = 10000;

        class BulletInfo
        {
            public int bullet_value { get; set; }
            public int hit { get; set; }
            public bool is_sure_hit { get; set; }
            public BulletInfo(int bullet_value, int hit, bool is_sure_hit)
            {
                this.bullet_value = bullet_value;
                this.hit = hit;
                this.is_sure_hit = is_sure_hit;
            }
        }
        class SpiritRecycleModel
        {
            public int phantasm_power_up_rate { get; set; }
            public int bullet_range { get; set; }

            // 4 boosts, each boosts contains a list of bullet information, including value/hit/isSureHit
            public List<List<BulletInfo>> Boosts { get; set; }
            public SpiritRecycleModel(int phantasm_power_up_rate, int bullet_range, List<List<BulletInfo>> Boosts)
            {
                this.phantasm_power_up_rate = phantasm_power_up_rate;
                this.bullet_range = bullet_range;
                this.Boosts = Boosts;
            }
        }
        private readonly THLWToolBoxContext _context;

        public PlayerUnitSpiritRecycleFilter(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: PlayerUnitShotDataFilter
        public async Task<IActionResult> Index(string? UnitSymbolName, int? EnemyCount, int? HitRank, int? SourceSmoke, int? TargetCharge, int? ConfidenceLevel)
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

            var playerUnitBulletDatas = from pubd in _context.PlayerUnitBulletData select pubd;
            var playerUnitBulletDatasList = await playerUnitBulletDatas.Distinct().ToListAsync();

            Dictionary<int, PlayerUnitShotData> shotDataDict = new();
            Dictionary<int, PlayerUnitSpellcardData> spellcardDataDict = new();
            Dictionary<int, PlayerUnitBulletData> bulletDataDict = new();

            foreach (var pusd in playerUnitShotDataList)
                shotDataDict[pusd.id] = pusd;
            foreach (var puscd in playerUnitSpellcardDataList)
                spellcardDataDict[puscd.id] = puscd;
            foreach (var pubd in playerUnitBulletDatasList)
                bulletDataDict[pubd.id] = pubd;

            List<PlayerUnitData> QueryUnit = new();
            List<PlayerUnitSpiritRecycleDisplayModel> displayPlayerUnitDatas = new();

            if (UnitSymbolName != null && UnitSymbolName.Length > 0)
            {
                foreach (var pud in playerUnitDatasList)
                {
                    if (UnitSymbolName.Equals(pud.name + pud.symbol_name))
                    {
                        QueryUnit.Add(pud);
                        displayPlayerUnitDatas.Add(CreateSpiritPowerRecycleDisplayModel("扩散射击", shotDataDict[pud.shot1_id], bulletDataDict, EnemyCount, HitRank, SourceSmoke, TargetCharge, ConfidenceLevel));
                        displayPlayerUnitDatas.Add(CreateSpiritPowerRecycleDisplayModel("集中射击", shotDataDict[pud.shot2_id], bulletDataDict, EnemyCount, HitRank, SourceSmoke, TargetCharge, ConfidenceLevel));
                        displayPlayerUnitDatas.Add(CreateSpiritPowerRecycleDisplayModel("符卡一", spellcardDataDict[pud.spellcard1_id], bulletDataDict, EnemyCount, HitRank, SourceSmoke, TargetCharge, ConfidenceLevel));
                        displayPlayerUnitDatas.Add(CreateSpiritPowerRecycleDisplayModel("符卡二", spellcardDataDict[pud.spellcard2_id], bulletDataDict, EnemyCount, HitRank, SourceSmoke, TargetCharge, ConfidenceLevel));
                        displayPlayerUnitDatas.Add(CreateSpiritPowerRecycleDisplayModel("终符", spellcardDataDict[pud.spellcard5_id], bulletDataDict, EnemyCount, HitRank, SourceSmoke, TargetCharge, ConfidenceLevel));
                    }
                }
            }

            var playerUnitDataVM = new PlayerUnitSpiritRecycleFilterViewModel
            {
                QueryUnit = QueryUnit,
                ShotDatas = displayPlayerUnitDatas,
                UnitSymbolName = UnitSymbolName,
                EnemyCount = EnemyCount,
                HitRank = HitRank,
                SourceSmoke = SourceSmoke,
                TargetCharge = TargetCharge,
                ConfidenceLevel = ConfidenceLevel
            };
            return View(playerUnitDataVM);
        }

        static void AddBulletToBoost(int bulletId, int bulletValue, int bulletBoostId, Dictionary<int, PlayerUnitBulletData> bulletDataDict, ref List<List<BulletInfo>> Boosts)
        {
            if (!bulletDataDict.ContainsKey(bulletId))
                return;
            var bulletRecord = bulletDataDict[bulletId];
            int Hit = bulletRecord.hit;
            bool SureHit = false;
            if (bulletRecord.bullet1_addon_id == 1 || bulletRecord.bullet2_addon_id == 1 || bulletRecord.bullet3_addon_id == 1)
                SureHit = true;
            Boosts[bulletBoostId].Add(new BulletInfo(bulletValue, Hit, SureHit));
        }

        static PlayerUnitSpiritRecycleDisplayModel CreateSpiritPowerRecycleDisplayModel(string shotType, PlayerUnitShotData shot, Dictionary<int, PlayerUnitBulletData> bulletDataDict, int? EnemyCount, int? HitRank, int? SourceSmoke, int? TargetCharge, int? ConfidenceLevel)
        {
            List<List<BulletInfo>> Boosts = new();
            for (int i = 0; i < 4; i++)
                Boosts.Add(new List<BulletInfo>());
            AddBulletToBoost(shot.magazine0_bullet_id, shot.magazine0_bullet_value, 0, bulletDataDict, ref Boosts);
            AddBulletToBoost(shot.magazine1_bullet_id, shot.magazine1_bullet_value, shot.magazine1_boost_count, bulletDataDict, ref Boosts);
            AddBulletToBoost(shot.magazine2_bullet_id, shot.magazine2_bullet_value, shot.magazine2_boost_count, bulletDataDict, ref Boosts);
            AddBulletToBoost(shot.magazine3_bullet_id, shot.magazine3_bullet_value, shot.magazine3_boost_count, bulletDataDict, ref Boosts);
            AddBulletToBoost(shot.magazine4_bullet_id, shot.magazine4_bullet_value, shot.magazine4_boost_count, bulletDataDict, ref Boosts);
            AddBulletToBoost(shot.magazine5_bullet_id, shot.magazine5_bullet_value, shot.magazine5_boost_count, bulletDataDict, ref Boosts);
            var ShotModel = new SpiritRecycleModel(shot.phantasm_power_up_rate, shot.magazine0_bullet_range, Boosts);
            List<double> BoostRecycles = CalcShotModel(ShotModel, EnemyCount, HitRank, SourceSmoke, TargetCharge, ConfidenceLevel);
            return new PlayerUnitSpiritRecycleDisplayModel(shotType, shot.name, shot.magazine0_bullet_range, BoostRecycles);
        }

        static PlayerUnitSpiritRecycleDisplayModel CreateSpiritPowerRecycleDisplayModel(string shotType, PlayerUnitSpellcardData shot, Dictionary<int, PlayerUnitBulletData> bulletDataDict, int? EnemyCount, int? HitRank, int? SourceSmoke, int? TargetCharge, int? ConfidenceLevel)
        {
            List<List<BulletInfo>> Boosts = new();
            for (int i = 0; i < 4; i++)
                Boosts.Add(new List<BulletInfo>());
            AddBulletToBoost(shot.magazine0_bullet_id, shot.magazine0_bullet_value, 0, bulletDataDict, ref Boosts);
            AddBulletToBoost(shot.magazine1_bullet_id, shot.magazine1_bullet_value, shot.magazine1_boost_count, bulletDataDict, ref Boosts);
            AddBulletToBoost(shot.magazine2_bullet_id, shot.magazine2_bullet_value, shot.magazine2_boost_count, bulletDataDict, ref Boosts);
            AddBulletToBoost(shot.magazine3_bullet_id, shot.magazine3_bullet_value, shot.magazine3_boost_count, bulletDataDict, ref Boosts);
            AddBulletToBoost(shot.magazine4_bullet_id, shot.magazine4_bullet_value, shot.magazine4_boost_count, bulletDataDict, ref Boosts);
            AddBulletToBoost(shot.magazine5_bullet_id, shot.magazine5_bullet_value, shot.magazine5_boost_count, bulletDataDict, ref Boosts);
            var ShotModel = new SpiritRecycleModel(shot.phantasm_power_up_rate, shot.magazine0_bullet_range, Boosts);
            List<double> BoostRecycles = CalcShotModel(ShotModel, EnemyCount, HitRank, SourceSmoke, TargetCharge, ConfidenceLevel);
            return new PlayerUnitSpiritRecycleDisplayModel(shotType, shot.name, shot.magazine0_bullet_range, BoostRecycles);
        }

        static double GetActualHitRate(int bulletHit, int? HitRank, int? SourceSmoke, int? TargetCharge)
        {
            int HitRankVal = HitRank.GetValueOrDefault(0);
            if (HitRankVal < -10 || HitRankVal > 10)
                throw new NotImplementedException();
            double Rate = 1.0 + 0.2 * Math.Abs(HitRankVal);
            if (HitRankVal < 0)
                Rate = 1.0 / Rate;
            double SmokeRate = Math.Pow(0.8, SourceSmoke.GetValueOrDefault(0));
            double ChargeRate = Math.Pow(0.8, TargetCharge.GetValueOrDefault(0));
            double UpdatedBulletHit = Rate * bulletHit * (SmokeRate / ChargeRate);
            return Math.Clamp(UpdatedBulletHit, 0.0, 100.0) / 100.0;
        }

        static List<double> CalcShotModel(SpiritRecycleModel shotModel, int? EnemyCount, int? HitRank, int? SourceSmoke, int? TargetCharge, int? ConfidenceLevel)
        {
            List<double> BoostRecycles = new();
            using (Py.GIL())
            {
                int SPRate = shotModel.phantasm_power_up_rate;
                if (shotModel.bullet_range == 1)
                    EnemyCount = 1;
                var AccumulateSP = np.zeros(MONTE_CARLO);
                foreach (var CurBoost in shotModel.Boosts)
                {
                    foreach (var BulletInfo in CurBoost)
                    {
                        int BulletCount = BulletInfo.bullet_value * EnemyCount.GetValueOrDefault(1);
                        bool IsSureHit = BulletInfo.is_sure_hit;
                        double ActualHit = IsSureHit ? 0 : GetActualHitRate(BulletInfo.hit, HitRank, SourceSmoke, TargetCharge);
                        int[] Dim = new int[] { BulletCount, MONTE_CARLO };
                        var SP = np.floor(SPRate * np.random.randint(3, 8, Dim) * 0.04) * 0.01;
                        if (!IsSureHit)
                        {
                            var HitMask = np.random.rand(BulletCount, MONTE_CARLO) < ActualHit;
                            SP = SP * HitMask;
                        }
                        var MagazineSP = np.sum(SP, 0);
                        AccumulateSP += MagazineSP;
                    }
                    var TmpSP = np.sort(np.copy(AccumulateSP));
                    double SPRecycle;
                    if (ConfidenceLevel == null || ConfidenceLevel.GetValueOrDefault() == 0)
                        SPRecycle = np.average(TmpSP);
                    else
                        SPRecycle = double.Parse(TmpSP[Convert.ToInt32(MONTE_CARLO * (1000 - ConfidenceLevel) / 1000.0)].repr);
                    BoostRecycles.Add(SPRecycle);
                }
            }
            return BoostRecycles;
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
