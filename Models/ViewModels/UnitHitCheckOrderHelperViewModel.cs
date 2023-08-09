using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    public class MagazineHitCheckInfo
    {
        public int magazine_id { get; set; }
        public int first_bullet_id { get; set; }
        public string element_info { get; set; }
        public string break_abnormal_info { get; set; }
        public MagazineHitCheckInfo(int magazine_id, int first_bullet_id, string element_info, string break_abnormal_info)
        {
            this.magazine_id = magazine_id;
            this.first_bullet_id = first_bullet_id;
            this.element_info = element_info;
            this.break_abnormal_info = break_abnormal_info;
        }
    }

    // Display model definition
    public class UnitHitCheckOrderHelperDisplayModel
    {
        public string type_name { get; set; }
        public string shot_name { get; set; }
        public int boost_id { get; set; }
        public int total_bullet_count { get; set; }
        public List<MagazineHitCheckInfo> hit_check_order_info { get; set; }
        public UnitHitCheckOrderHelperDisplayModel(string type_name, string shot_name, int boost_id, int total_bullet_count, List<MagazineHitCheckInfo> hit_check_order_info)
        {
            this.type_name = type_name;
            this.shot_name = shot_name;
            this.boost_id = boost_id;
            this.total_bullet_count = total_bullet_count;
            this.hit_check_order_info = hit_check_order_info;
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
                text += bulletGridWrapper + "<b><font color=#FF6600>" + hitCheckInfo.magazine_id + "</font></b>" + "</div>";
                text += bulletGridWrapper + hitCheckInfo.first_bullet_id + "</div>";
                text += bulletGridWrapper + hitCheckInfo.element_info + "</div>";
                text += bulletGridWrapper + hitCheckInfo.break_abnormal_info + "</div>";
            }
            text += "</div>";
            return text;
        }
    }
}
