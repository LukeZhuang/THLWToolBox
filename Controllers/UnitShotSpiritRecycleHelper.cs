using NumSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Models.GeneralModels;
using static THLWToolBox.Helpers.GeneralHelper;

namespace THLWToolBox.Controllers
{
    public class UnitShotSpiritRecycleHelper : Controller
    {
        private const int MONTE_CARLO = 10000;

        private readonly THLWToolBoxContext _context;

        // Data structures used across this controller
        Dictionary<int, PlayerUnitBulletData> bulletDict;

        public UnitShotSpiritRecycleHelper(THLWToolBoxContext context)
        {
            _context = context;
            bulletDict = new();
        }

        // POST: UnitShotSpiritRecycleHelper
        public async Task<IActionResult> Index(UnitShotSpiritRecycleHelperViewModel request)
        {
            if (_context.PictureData == null)
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");

            // --- query data tables ---
            List<PlayerUnitData> unitList = await _context.PlayerUnitData.Distinct().ToListAsync();
            Dictionary<int, PlayerUnitShotData> shotDict = (await _context.PlayerUnitShotData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            Dictionary<int, PlayerUnitSpellcardData> spellcardDict = (await _context.PlayerUnitSpellcardData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);

            bulletDict = (await _context.PlayerUnitBulletData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            // ------ query end ------


            List<PlayerUnitData> queryUnits = new();
            List<UnitShotSpiritRecycleDisplayModel> spiritRecycleDatas = new();

            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                PlayerUnitData unitRecord = GetUnitByNameSymbol(unitList, request.UnitSymbolName);
                queryUnits.Add(unitRecord);
                List<UnitShotSpiritRecycleDisplayModel> unitSpiritRecycleDatas = new()
                {
                    CreateSpiritPowerRecycleDisplayModel(new AttackData(AttackData.TypeStringSpreadShot, shotDict[unitRecord.shot1_id]), request),
                    CreateSpiritPowerRecycleDisplayModel(new AttackData(AttackData.TypeStringFocusShot, shotDict[unitRecord.shot2_id]), request),
                    CreateSpiritPowerRecycleDisplayModel(new AttackData(AttackData.TypeStringSpellcard1, spellcardDict[unitRecord.spellcard1_id]), request),
                    CreateSpiritPowerRecycleDisplayModel(new AttackData(AttackData.TypeStringSpellcard2, spellcardDict[unitRecord.spellcard2_id]), request),
                    CreateSpiritPowerRecycleDisplayModel(new AttackData(AttackData.TypeStringLastWord, spellcardDict[unitRecord.spellcard5_id]), request),
                };
                spiritRecycleDatas.AddRange(unitSpiritRecycleDatas);
            }

            request.QueryUnits = queryUnits;
            request.SpiritRecycleDatas = spiritRecycleDatas;

            return View(request);
        }

        void AddBulletToBoost(BulletMagazineModel magazine, ref List<List<MagazineInfo>> boosts_info)
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

        UnitShotSpiritRecycleDisplayModel CreateSpiritPowerRecycleDisplayModel(AttackData attack, UnitShotSpiritRecycleHelperViewModel request)
        {
            List<List<MagazineInfo>> boosts_info = new();
            for (int boostId = 0; boostId < 4; boostId++)
                boosts_info.Add(new List<MagazineInfo>());
            for (int magazineId = 0; magazineId < 6; magazineId++)
                AddBulletToBoost(attack.Magazines[magazineId], ref boosts_info);
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
                        spiritRecycleMatrix *= hitMask.astype(typeof(double));
                    }
                    var magazineSP = np.sum(spiritRecycleMatrix, 0);
                    accumulatedSP += magazineSP;
                }
                var tmpSP = accumulatedSP.copy().argsort<double>();
                double spiritRecycle;
                if (confidenceLevel == 0)
                    spiritRecycle = 1;
                    // spiritRecycle = tmpSP.mean()[0];
                else
                    spiritRecycle = 2;
                    // spiritRecycle = tmpSP[Convert.ToInt32(MONTE_CARLO * (1000 - confidenceLevel) / 1000.0)];
                spiritRecycles.Add(spiritRecycle);
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
