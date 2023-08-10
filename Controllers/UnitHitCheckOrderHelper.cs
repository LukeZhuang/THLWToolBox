using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Models.GeneralModels;
using static THLWToolBox.Helpers.TypeHelper;
using static THLWToolBox.Helpers.GeneralHelper;

namespace THLWToolBox.Controllers
{
    public class UnitHitCheckOrderHelper : Controller
    {
        private readonly THLWToolBoxContext _context;

        public UnitHitCheckOrderHelper(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: UnitHitCheckOrderHelper
        public async Task<IActionResult> Index(UnitHitCheckOrderHelperViewModel request)
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

            var hitCheckOrderTable = from puhcod in _context.PlayerUnitHitCheckOrderData select puhcod;
            var hitCheckOrderList = await hitCheckOrderTable.Distinct().ToListAsync();

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
            List<UnitHitCheckOrderHelperDisplayModel> hitCheckOrderDatas = new();

            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                foreach (var unitRecord in unitList)
                {
                    string curUnitSymbolName = unitRecord.name + unitRecord.symbol_name;
                    if (!request.UnitSymbolName.Equals(curUnitSymbolName))
                        continue;

                    queryUnits.Add(unitRecord);
                    foreach (var hitCheckOrderRecord in hitCheckOrderList)
                    {
                        if (hitCheckOrderRecord.unit_id != unitRecord.id || hitCheckOrderRecord.barrage_id != request.BarrageId.GetValueOrDefault())
                            continue;
                        AttackData attack = hitCheckOrderRecord.barrage_id switch
                        {
                            1 => new AttackData("扩散", shotDict[unitRecord.shot1_id]),
                            2 => new AttackData("集中", shotDict[unitRecord.shot2_id]),
                            3 => new AttackData("一符", spellcardDict[unitRecord.spellcard1_id]),
                            4 => new AttackData("二符", spellcardDict[unitRecord.spellcard2_id]),
                            7 => new AttackData("终符", spellcardDict[unitRecord.spellcard5_id]),
                            _ => throw new NotImplementedException(),
                        };
                        hitCheckOrderDatas.Add(CreateHitCheckOrderDisplayModel(attack, bulletDict, hitCheckOrderRecord));
                    }
                }
            }

            request.QueryUnits = queryUnits;
            request.HitCheckOrderDatas = hitCheckOrderDatas;

            return View(request);
        }

        static UnitHitCheckOrderHelperDisplayModel CreateHitCheckOrderDisplayModel(AttackData attack, Dictionary<int, PlayerUnitBulletData> bulletDict,
                                                                                   PlayerUnitHitCheckOrderData hitCheckOrderRecord)
        {
            string typeName = attack.attack_type_name;
            string shotName = attack.name;
            int boostId = hitCheckOrderRecord.boost_id;

            if (hitCheckOrderRecord.hit_check_order.Equals("(empty)"))
                return new UnitHitCheckOrderHelperDisplayModel(typeName, shotName, boostId, 0, new List<MagazineHitCheckInfo>());
            
            int totalBulletCount = 0;
            List<BulletMagazineModel> boostMagazines = new();
            foreach (var magazine in attack.magazines)
            {
                if (magazine.boost_count <= hitCheckOrderRecord.boost_id)
                {
                    boostMagazines.Add(magazine);
                    totalBulletCount += magazine.bullet_value;
                }
            }
            List<MagazineHitCheckInfo> hitCheckInfos = GetHitCheckOrderInfo(boostMagazines, bulletDict, hitCheckOrderRecord.hit_check_order);
            return new UnitHitCheckOrderHelperDisplayModel(typeName, shotName, boostId, totalBulletCount, hitCheckInfos);
        }

        static List<MagazineHitCheckInfo> GetHitCheckOrderInfo(List<BulletMagazineModel> boostMagazines, Dictionary<int, PlayerUnitBulletData> bulletDict, string hitCheckOrderStr)
        {
            List<MagazineHitCheckInfo> hitCheckInfos = new();
            HashSet<int> bulletVisited = new();

            for (int index = 0; index < hitCheckOrderStr.Length; index++)
            {
                int magazineId = hitCheckOrderStr[index] - '0';
                if (!bulletVisited.Contains(magazineId))
                {
                    bulletVisited.Add(magazineId);
                    BulletMagazineModel magazine = boostMagazines[magazineId - 1];
                    PlayerUnitBulletData bulletRecord = bulletDict[magazine.bullet_id];

                    string elementInfo = "";
                    if (bulletRecord.element != 9)
                        elementInfo = GetElementTypeString(bulletRecord.element);

                    List<string> abnormals = new();
                    List<BulletAddonModel> bulletAddons = GetBulletAddons(bulletRecord);
                    foreach (var bulletAddon in bulletAddons)
                        if (IsBreakingAbnormalAddon(bulletAddon.id))
                            abnormals.Add(GetAbnormalBreakString(bulletAddon.id));
                    string abnormalInfo = string.Join("</br>", abnormals);

                    MagazineHitCheckInfo magazine_info = new(magazineId, index + 1, elementInfo, abnormalInfo);
                    hitCheckInfos.Add(magazine_info);
                }
            }
            return hitCheckInfos;
        }
    }
}
