using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Numpy;
using Python.Runtime;
using THLWToolBox.Data;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Models.GeneralModels;

namespace THLWToolBox.Controllers
{
    public class UnitShotSpiritRecycleHelper : Controller
    {
        private const int MONTE_CARLO = 10000;

        private readonly THLWToolBoxContext _context;

        public UnitShotSpiritRecycleHelper(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: UnitShotSpiritRecycleHelper
        public async Task<IActionResult> Index(UnitShotSpiritRecycleHelperViewModel request)
        {
            if (_context.PictureData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }

            // --- query data tables ---
            var unitTable = from pud in _context.PlayerUnitData select pud;
            var unitList = await unitTable.Distinct().ToListAsync();

            var shotTable = from pusd in _context.PlayerUnitShotData select pusd;
            var shotList = await shotTable.Distinct().ToListAsync();

            var spellcardTable = from puscd in _context.PlayerUnitSpellcardData select puscd;
            var spellcardList = await spellcardTable.Distinct().ToListAsync();

            var bulletTable = from pubd in _context.PlayerUnitBulletData select pubd;
            var bulletList = await bulletTable.Distinct().ToListAsync();

            Dictionary<int, PlayerUnitShotData> shotDict = new();
            foreach (var shotRecord in shotList)
                shotDict[shotRecord.id] = shotRecord;

            Dictionary<int, PlayerUnitSpellcardData> spellcardDict = new();
            foreach (var spellcardRecord in spellcardList)
                spellcardDict[spellcardRecord.id] = spellcardRecord;

            Dictionary<int, PlayerUnitBulletData> bulletDict = new();
            foreach (var bulletRecord in bulletList)
                bulletDict[bulletRecord.id] = bulletRecord;
            // ------ query end ------


            List<PlayerUnitData> queryUnits = new();
            List<UnitShotSpiritRecycleDisplayModel> spiritRecycleDatas = new();

            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                foreach (var unitRecord in unitList)
                {
                    string curUnitSymbolName = unitRecord.name + unitRecord.symbol_name;
                    if (!request.UnitSymbolName.Equals(curUnitSymbolName))
                        continue;
                    queryUnits.Add(unitRecord);
                    List<UnitShotSpiritRecycleDisplayModel> unitSpiritRecycleDatas = new()
                    {
                        CreateSpiritPowerRecycleDisplayModel(new AttackData("扩散", shotDict[unitRecord.shot1_id]), bulletDict, request),
                        CreateSpiritPowerRecycleDisplayModel(new AttackData("集中", shotDict[unitRecord.shot2_id]), bulletDict, request),
                        CreateSpiritPowerRecycleDisplayModel(new AttackData("一符", spellcardDict[unitRecord.spellcard1_id]), bulletDict, request),
                        CreateSpiritPowerRecycleDisplayModel(new AttackData("二符", spellcardDict[unitRecord.spellcard2_id]), bulletDict, request),
                        CreateSpiritPowerRecycleDisplayModel(new AttackData("终符", spellcardDict[unitRecord.spellcard5_id]), bulletDict, request),
                    };
                    spiritRecycleDatas.AddRange(unitSpiritRecycleDatas);
                }
            }

            request.QueryUnits = queryUnits;
            request.SpiritRecycleDatas = spiritRecycleDatas;

            return View(request);
        }

        static void AddBulletToBoost(BulletMagazineModel magazine, Dictionary<int, PlayerUnitBulletData> bulletDict, ref List<List<MagazineInfo>> boosts_info)
        {
            if (!bulletDict.ContainsKey(magazine.BulletId))
                return;
            PlayerUnitBulletData bulletRecord = bulletDict[magazine.BulletId];
            int hit = bulletRecord.hit;
            bool isSureHit = false;
            if (bulletRecord.bullet1_addon_id == 1 || bulletRecord.bullet2_addon_id == 1 || bulletRecord.bullet3_addon_id == 1)
                isSureHit = true;
            boosts_info[magazine.BoostCount].Add(new MagazineInfo(magazine.BulletValue, hit, isSureHit));
        }

        static UnitShotSpiritRecycleDisplayModel CreateSpiritPowerRecycleDisplayModel(AttackData attack, Dictionary<int, PlayerUnitBulletData> bulletDict, UnitShotSpiritRecycleHelperViewModel request)
        {
            List<List<MagazineInfo>> boosts_info = new();
            for (int boostId = 0; boostId < 4; boostId++)
                boosts_info.Add(new List<MagazineInfo>());
            for (int magazineId = 0; magazineId < 6; magazineId++)
                AddBulletToBoost(attack.Magazines[magazineId], bulletDict, ref boosts_info);
            SpiritRecycleModel spiritRecycleInput = new(attack.PhantasmPowerUpRate, attack.Magazines[0].BulletRange, boosts_info);
            List<double> spiritRecycles = CalcShotModel(spiritRecycleInput, request);
            return new UnitShotSpiritRecycleDisplayModel(attack.AttackTypeName, attack.Name, attack.Magazines[0].BulletRange, spiritRecycles);
        }

        static double GetActualHitRate(int bulletHit, int hitRank, int sourceSmoke, int targetCharge)
        {
            double hitRate = 1.0 + 0.2 * Math.Abs(hitRank);
            if (hitRank < 0)
                hitRate = 1.0 / hitRate;
            double smokeRate = Math.Pow(0.8, sourceSmoke);
            double chargeRate = Math.Pow(0.8, targetCharge);
            hitRate *= bulletHit * (smokeRate / chargeRate);
            return Math.Clamp(hitRate, 0.0, 100.0) / 100.0;
        }

        static List<double> CalcShotModel(SpiritRecycleModel spiritRecycleInput, UnitShotSpiritRecycleHelperViewModel request)
        {
            List<double> spiritRecycles = new();
            int spRate = spiritRecycleInput.PhantasmPowerUpRate;
            int enemyCount = (spiritRecycleInput.BulletRange == 1) ? 1 : request.EnemyCount.GetValueOrDefault(1);
            int hitRank = request.HitRank.GetValueOrDefault(0);
            if (hitRank < -10 || hitRank > 10)
                throw new NotImplementedException();
            int sourceSmoke = request.SourceSmoke.GetValueOrDefault(0);
            int targetCharge = request.TargetCharge.GetValueOrDefault(0);
            int confidenceLevel = request.ConfidenceLevel.GetValueOrDefault(0);

            using (Py.GIL())
            {
                var accumulatedSP = np.zeros(MONTE_CARLO);
                foreach (List<MagazineInfo> boost in spiritRecycleInput.BoostsInfo)
                {
                    foreach (MagazineInfo magazineInfo in boost)
                    {
                        int bulletCount = magazineInfo.BulletValue * enemyCount;
                        bool isSureHit = magazineInfo.IsSureHit;
                        double actualHit = GetActualHitRate(magazineInfo.Hit, hitRank, sourceSmoke, targetCharge);
                        var spiritRecycleMatrix = np.floor(spRate * np.random.randint(3, 8, new int[] { bulletCount, MONTE_CARLO }) * 0.04) * 0.01;
                        if (!isSureHit)
                        {
                            var hitMask = np.random.rand(bulletCount, MONTE_CARLO) < actualHit;
                            spiritRecycleMatrix *= hitMask;
                        }
                        var magazineSP = np.sum(spiritRecycleMatrix, 0);
                        accumulatedSP += magazineSP;
                    }
                    var tmpSP = np.sort(np.copy(accumulatedSP));
                    double spiritRecycle;
                    if (confidenceLevel == 0)
                        spiritRecycle = np.average(tmpSP);
                    else
                        spiritRecycle = double.Parse(tmpSP[Convert.ToInt32(MONTE_CARLO * (1000 - confidenceLevel) / 1000.0)].repr);
                    spiritRecycles.Add(spiritRecycle);
                }
            }
            return spiritRecycles;
        }

        // used for calculation inside this helper only
        private class MagazineInfo
        {
            public int BulletValue { get; set; }
            public int Hit { get; set; }
            public bool IsSureHit { get; set; }
            public MagazineInfo(int bulletValue, int hit, bool isSureHit)
            {
                this.BulletValue = bulletValue;
                this.Hit = hit;
                this.IsSureHit = isSureHit;
            }
        }

        // used for calculation inside this helper only
        private class SpiritRecycleModel
        {
            public int PhantasmPowerUpRate { get; set; }
            public int BulletRange { get; set; }

            // 4 boosts, each boosts contains a list of bullet information, including value/hit/isSureHit
            public List<List<MagazineInfo>> BoostsInfo { get; set; }
            public SpiritRecycleModel(int phantasmPowerUpRate, int bulletRange, List<List<MagazineInfo>> boostsInfo)
            {
                this.PhantasmPowerUpRate = phantasmPowerUpRate;
                this.BulletRange = bulletRange;
                this.BoostsInfo = boostsInfo;
            }
        }
    }
}
