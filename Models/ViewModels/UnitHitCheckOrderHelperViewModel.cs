using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    public class MagazineHitCheckInfo
    {
        public int MagazineId { get; set; }
        public int FirstBulletId { get; set; }
        public string ElementInfo { get; set; }
        public string BreakAbnormalInfo { get; set; }
        public MagazineHitCheckInfo(int magazineId, int firstBulletId, string elementInfo, string breakAbnormalInfo)
        {
            MagazineId = magazineId;
            FirstBulletId = firstBulletId;
            ElementInfo = elementInfo;
            BreakAbnormalInfo = breakAbnormalInfo;
        }
    }

    // Display model definition
    public class UnitHitCheckOrderHelperDisplayModel
    {
        public string TypeName { get; set; }
        public string ShotName { get; set; }
        public int BoostId { get; set; }
        public int TotalBulletCount { get; set; }
        public List<MagazineHitCheckInfo> HitCheckOrderInfos { get; set; }
        public UnitHitCheckOrderHelperDisplayModel(string typeName, string shotName, int boostId, int totalBulletCount, List<MagazineHitCheckInfo> hitCheckOrderInfos)
        {
            TypeName = typeName;
            ShotName = shotName;
            BoostId = boostId;
            TotalBulletCount = totalBulletCount;
            HitCheckOrderInfos = hitCheckOrderInfos;
        }
    }
    public class UnitHitCheckOrderHelperViewModel
    {
        // Display models
        public List<PlayerUnitData>? QueryUnits { get; set; }
        public List<UnitHitCheckOrderHelperDisplayModel>? HitCheckOrderDatas { get; set; }

        // Webpage query Parameters
        public string? UnitSymbolName { get; set; }
        public int? BarrageId { get; set; }


        // Methods
        public static string DisplayHitCheckOrder(List<MagazineHitCheckInfo> hitCheckInfos)
        {
            string bulletGridWrapper = "<div class=\"bullet-wrapper\">";
            string text = "<div class=\"magazine-grid\">";
            text += bulletGridWrapper + "段落" + "</div>";
            text += bulletGridWrapper + "首发" + "</div>";
            text += bulletGridWrapper + "属性" + "</div>";
            text += bulletGridWrapper + "异常" + "</div>";
            foreach (var hitCheckInfo in hitCheckInfos)
            {
                text += bulletGridWrapper + "<b><font color=#FF6600>" + hitCheckInfo.MagazineId + "</font></b>" + "</div>";
                text += bulletGridWrapper + hitCheckInfo.FirstBulletId + "</div>";
                text += bulletGridWrapper + hitCheckInfo.ElementInfo + "</div>";
                text += bulletGridWrapper + hitCheckInfo.BreakAbnormalInfo + "</div>";
            }
            text += "</div>";
            return text;
        }
    }
}
