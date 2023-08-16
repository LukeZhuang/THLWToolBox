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

        // Data structures used across this controller
        Dictionary<int, PlayerUnitShotData> shotDict;
        Dictionary<int, PlayerUnitSpellcardData> spellcardDict;
        Dictionary<int, PlayerUnitBulletData> bulletDict;

        public UnitHitCheckOrderHelper(THLWToolBoxContext context)
        {
            _context = context;
            shotDict = new();
            spellcardDict = new();
            bulletDict = new();
        }

        // POST: UnitHitCheckOrderHelper
        public async Task<IActionResult> Index(UnitHitCheckOrderHelperViewModel request)
        {
            if (_context.PictureData == null)
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");

            // ------ query data ------
            List<PlayerUnitData> unitList = await _context.PlayerUnitData.Distinct().ToListAsync();
            List<PlayerUnitHitCheckOrderData> hitCheckOrderList = await _context.PlayerUnitHitCheckOrderData.Distinct().ToListAsync();

            shotDict = (await _context.PlayerUnitShotData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            spellcardDict = (await _context.PlayerUnitSpellcardData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            bulletDict = (await _context.PlayerUnitBulletData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            // ------ query end ------


            List<PlayerUnitData> queryUnits = new();
            List<UnitHitCheckOrderHelperDisplayModel> hitCheckOrderDatas = new();

            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                PlayerUnitData? unitRecord = GetUnitByNameSymbol(unitList, request.UnitSymbolName);
                if (unitRecord != null)
                {
                    queryUnits.Add(unitRecord);
                    hitCheckOrderDatas.AddRange(hitCheckOrderList.Where(x => x.unit_id == unitRecord.id && x.barrage_id == request.BarrageId.GetValueOrDefault())
                                                                 .Select(x => CreateHitCheckOrderDisplayModel(unitRecord, x)));
                }
            }

            request.QueryUnits = queryUnits;
            request.HitCheckOrderDatas = hitCheckOrderDatas;

            return View(request);
        }

        UnitHitCheckOrderHelperDisplayModel CreateHitCheckOrderDisplayModel(PlayerUnitData unitRecord, PlayerUnitHitCheckOrderData hitCheckOrderRecord)
        {
            AttackData attack = hitCheckOrderRecord.barrage_id switch
            {
                1 => new AttackData("扩散", shotDict[unitRecord.shot1_id]),
                2 => new AttackData("集中", shotDict[unitRecord.shot2_id]),
                3 => new AttackData("一符", spellcardDict[unitRecord.spellcard1_id]),
                4 => new AttackData("二符", spellcardDict[unitRecord.spellcard2_id]),
                7 => new AttackData("终符", spellcardDict[unitRecord.spellcard5_id]),
                _ => throw new NotImplementedException(),
            };
            string typeName = attack.AttackTypeName;
            string shotName = attack.Name;
            int boostId = hitCheckOrderRecord.boost_id;

            if (hitCheckOrderRecord.hit_check_order.Equals("(empty)"))
                return new UnitHitCheckOrderHelperDisplayModel(typeName, shotName, boostId, 0, new List<MagazineHitCheckInfo>());
            
            List<BulletMagazineModel> boostMagazines = attack.Magazines.Where(x => x.BoostCount <= hitCheckOrderRecord.boost_id).ToList();
            int totalBulletCount = boostMagazines.Sum(x => x.BulletValue);
            List<MagazineHitCheckInfo> hitCheckInfos = GetHitCheckOrderInfo(boostMagazines, hitCheckOrderRecord.hit_check_order);

            return new UnitHitCheckOrderHelperDisplayModel(typeName, shotName, boostId, totalBulletCount, hitCheckInfos);
        }

        List<MagazineHitCheckInfo> GetHitCheckOrderInfo(List<BulletMagazineModel> boostMagazines, string hitCheckOrderStr)
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
                    PlayerUnitBulletData bulletRecord = bulletDict[magazine.BulletId];

                    string elementInfo = "";
                    if (bulletRecord.element != 9)
                        elementInfo = GetElementTypeString(bulletRecord.element);

                    List<string> abnormals = new();
                    List<BulletAddonModel> bulletAddons = GetBulletAddons(bulletRecord);
                    foreach (var bulletAddon in bulletAddons)
                        if (IsBreakingAbnormalAddon(bulletAddon.Id))
                            abnormals.Add(GetAbnormalBreakString(bulletAddon.Id));
                    string abnormalInfo = string.Join("</br>", abnormals);

                    MagazineHitCheckInfo magazine_info = new(magazineId, index + 1, elementInfo, abnormalInfo);
                    hitCheckInfos.Add(magazine_info);
                }
            }
            return hitCheckInfos;
        }
    }
}
