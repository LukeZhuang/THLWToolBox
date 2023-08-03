using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using THLWToolBox.Data;
using THLWToolBox.Models;
using static THLWToolBox.Models.GeneralTypeMaster;

namespace THLWToolBox.Controllers
{
    public class PlayerUnitHitCheckOrderHelper : Controller
    {
        private readonly THLWToolBoxContext _context;

        public PlayerUnitHitCheckOrderHelper(THLWToolBoxContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string? UnitSymbolName, int? BarrageId)
        {
            if (_context.PictureData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }
            var playerUnitDatas = from pud in _context.PlayerUnitData select pud;
            var playerUnitDatasList = await playerUnitDatas.Distinct().ToListAsync();

            var playerUnitShotDatas = from pusd in _context.PlayerUnitShotData select pusd;
            var playerUnitShotDataList = await playerUnitShotDatas.Distinct().ToListAsync();

            var playerUnitSpellcardDatas = from puscd in _context.PlayerUnitSpellcardData select puscd;
            var playerUnitSpellcardDataList = await playerUnitSpellcardDatas.Distinct().ToListAsync();

            var playerUnitBulletDatas = from pubd in _context.PlayerUnitBulletData select pubd;
            var playerUnitBulletDatasList = await playerUnitBulletDatas.Distinct().ToListAsync();

            var playerUnitHitCheckOrderDatas = from puhcod in _context.PlayerUnitHitCheckOrderData select puhcod;
            var playerUnitHitCheckOrderDatasList = await playerUnitHitCheckOrderDatas.Distinct().ToListAsync();

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
            List<PlayerUnitHitCheckOrderDisplayModel> displayPlayerUnitDatas = new();

            if (UnitSymbolName != null && UnitSymbolName.Length > 0)
            {
                foreach (var pud in playerUnitDatasList)
                {
                    if (UnitSymbolName.Equals(pud.name + pud.symbol_name))
                    {
                        QueryUnit.Add(pud);
                        foreach (var puhcod in playerUnitHitCheckOrderDatasList)
                        {
                            if (!(puhcod.unit_id == pud.id && puhcod.barrage_id == BarrageId.GetValueOrDefault(1)))
                                continue;
                            int barrage_id = puhcod.barrage_id;
                            int boost_id = puhcod.boost_id;
                            string type_name = "";
                            string shot_name = "";
                            List<Tuple<int, int, string, string>> hit_check_order_info = new();
                            int total_bullet_count = 0;
                            switch (barrage_id)
                            {
                                case 1:
                                    type_name = "扩散";
                                    shot_name = shotDataDict[pud.shot1_id].name;
                                    total_bullet_count = GetShotHitCheckOrderInfo(shotDataDict[pud.shot1_id], bulletDataDict, puhcod.boost_id, puhcod.hit_check_order, ref hit_check_order_info);
                                    break;
                                case 2:
                                    type_name = "集中";
                                    shot_name = shotDataDict[pud.shot2_id].name;
                                    total_bullet_count = GetShotHitCheckOrderInfo(shotDataDict[pud.shot2_id], bulletDataDict, puhcod.boost_id, puhcod.hit_check_order, ref hit_check_order_info);
                                    break;
                                case 3:
                                    type_name = "一符";
                                    shot_name = spellcardDataDict[pud.spellcard1_id].name;
                                    total_bullet_count = GetSpellcardHitCheckOrderInfo(spellcardDataDict[pud.spellcard1_id], bulletDataDict, puhcod.boost_id, puhcod.hit_check_order, ref hit_check_order_info);
                                    break;
                                case 4:
                                    type_name = "二符";
                                    shot_name = spellcardDataDict[pud.spellcard2_id].name;
                                    total_bullet_count = GetSpellcardHitCheckOrderInfo(spellcardDataDict[pud.spellcard2_id], bulletDataDict, puhcod.boost_id, puhcod.hit_check_order, ref hit_check_order_info);
                                    break;
                                case 7:
                                    type_name = "终符";
                                    shot_name = spellcardDataDict[pud.spellcard5_id].name;
                                    total_bullet_count = GetSpellcardHitCheckOrderInfo(spellcardDataDict[pud.spellcard5_id], bulletDataDict, puhcod.boost_id, puhcod.hit_check_order, ref hit_check_order_info);
                                    break;
                            }
                            displayPlayerUnitDatas.Add(new PlayerUnitHitCheckOrderDisplayModel(type_name, shot_name, boost_id, total_bullet_count, hit_check_order_info));
                        }
                    }
                }
            }
            var playerUnitDataVM = new PlayerUnitHitCheckOrderHelperViewModel
            {
                QueryUnit = QueryUnit,
                HitCheckOrderDatas = displayPlayerUnitDatas,
                UnitSymbolName = UnitSymbolName,
                BarrageId = BarrageId
            };
            return View(playerUnitDataVM);
        }

        public static int GetShotHitCheckOrderInfo(PlayerUnitShotData pusd, Dictionary<int, PlayerUnitBulletData> bulletDataDict, int boostId, string hit_check_order, ref List<Tuple<int, int, string, string>> hit_check_order_info)
        {
            List<SingleBulletInfo> bulletList = new() { new SingleBulletInfo(pusd.magazine0_bullet_id, 0, pusd.magazine0_bullet_value, 0) };
            if (pusd.magazine1_boost_count <= boostId)
                bulletList.Add(new SingleBulletInfo(pusd.magazine1_bullet_id, 0, pusd.magazine1_bullet_value, 0));
            if (pusd.magazine2_boost_count <= boostId)
                bulletList.Add(new SingleBulletInfo(pusd.magazine2_bullet_id, 0, pusd.magazine2_bullet_value, 0));
            if (pusd.magazine3_boost_count <= boostId)
                bulletList.Add(new SingleBulletInfo(pusd.magazine3_bullet_id, 0, pusd.magazine3_bullet_value, 0));
            if (pusd.magazine4_boost_count <= boostId)
                bulletList.Add(new SingleBulletInfo(pusd.magazine4_bullet_id, 0, pusd.magazine4_bullet_value, 0));
            if (pusd.magazine5_boost_count <= boostId)
                bulletList.Add(new SingleBulletInfo(pusd.magazine5_bullet_id, 0, pusd.magazine5_bullet_value, 0));
            int total_bullet_count = 0;
            foreach (var bullet in bulletList)
                total_bullet_count += bullet.bullet_value;
            if (hit_check_order != "(empty)")
            {
                if (total_bullet_count != hit_check_order.Length)
                    throw new NotImplementedException("mismatched bullet count");
                GetHitCheckOrderInfo(bulletList, bulletDataDict, hit_check_order, ref hit_check_order_info);
            }
            return total_bullet_count;
        }

        public static int GetSpellcardHitCheckOrderInfo(PlayerUnitSpellcardData puscd, Dictionary<int, PlayerUnitBulletData> bulletDataDict, int boostId, string hit_check_order, ref List<Tuple<int, int, string, string>> hit_check_order_info)
        {
            List<SingleBulletInfo> bulletList = new() { new SingleBulletInfo(puscd.magazine0_bullet_id, 0, puscd.magazine0_bullet_value, 0) };
            if (puscd.magazine1_boost_count <= boostId)
                bulletList.Add(new SingleBulletInfo(puscd.magazine1_bullet_id, 0, puscd.magazine1_bullet_value, 0));
            if (puscd.magazine2_boost_count <= boostId)
                bulletList.Add(new SingleBulletInfo(puscd.magazine2_bullet_id, 0, puscd.magazine2_bullet_value, 0));
            if (puscd.magazine3_boost_count <= boostId)
                bulletList.Add(new SingleBulletInfo(puscd.magazine3_bullet_id, 0, puscd.magazine3_bullet_value, 0));
            if (puscd.magazine4_boost_count <= boostId)
                bulletList.Add(new SingleBulletInfo(puscd.magazine4_bullet_id, 0, puscd.magazine4_bullet_value, 0));
            if (puscd.magazine5_boost_count <= boostId)
                bulletList.Add(new SingleBulletInfo(puscd.magazine5_bullet_id, 0, puscd.magazine5_bullet_value, 0));
            int total_bullet_count = 0;
            foreach (var bullet in bulletList)
                total_bullet_count += bullet.bullet_value;
            if (hit_check_order != "(empty)")
            {
                if (total_bullet_count != hit_check_order.Length)
                    throw new NotImplementedException("mismatched bullet count");
                GetHitCheckOrderInfo(bulletList, bulletDataDict, hit_check_order, ref hit_check_order_info);
            }
            return total_bullet_count;
        }

        public static void GetHitCheckOrderInfo(List<SingleBulletInfo> bulletList, Dictionary<int, PlayerUnitBulletData> bulletDataDict, string hit_check_order, ref List<Tuple<int, int, string, string>> hit_check_order_info)
        {
            Dictionary<int, int> FirstBulletId = new(); 
            for (int index = 0; index < hit_check_order.Length; index++)
            {
                int magazine_id = hit_check_order[index] - '0' - 1;
                if (!FirstBulletId.ContainsKey(magazine_id))
                {
                    FirstBulletId[magazine_id] = index;
                    SingleBulletInfo bulletInfo = bulletList[magazine_id];
                    PlayerUnitBulletData bulletRecord = bulletDataDict[bulletInfo.bullet_id];
                    string elementInfo = "";
                    if (bulletRecord.element != 9)
                        elementInfo = GeneralTypeMaster.GetElementTypeString(bulletRecord.element);
                    List<int> BulletAddons = new();
                    if (bulletRecord.bullet1_addon_id >= 12 && bulletRecord.bullet1_addon_id <= 16)
                        BulletAddons.Add(bulletRecord.bullet1_addon_id);
                    if (bulletRecord.bullet2_addon_id >= 12 && bulletRecord.bullet2_addon_id <= 16)
                        BulletAddons.Add(bulletRecord.bullet2_addon_id);
                    if (bulletRecord.bullet3_addon_id >= 12 && bulletRecord.bullet3_addon_id <= 16)
                        BulletAddons.Add(bulletRecord.bullet3_addon_id);
                    string abnormalInfo = "";
                    if (BulletAddons.Count > 0)
                    {
                        foreach (var bulletAddon in BulletAddons)
                        {
                            abnormalInfo += GeneralTypeMaster.GetAbnormalBreakString(bulletAddon) + "</br>";
                        }
                        abnormalInfo = abnormalInfo.Substring(0, abnormalInfo.Length - 5);
                    }
                    Tuple<int, int, string, string> magazine_info = new(hit_check_order[index] - '0', (index + 1), elementInfo, abnormalInfo);
                    hit_check_order_info.Add(magazine_info);
                }
            }
        }
    }
}
