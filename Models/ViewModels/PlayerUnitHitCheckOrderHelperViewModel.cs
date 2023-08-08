using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    public class PlayerUnitHitCheckOrderDisplayModel
    {
        public string type_name { get; set; }
        public string shot_name { get; set; }
        public int boost_id { get; set; }
        public int total_bullet_count { get; set; }
        public List<Tuple<int, int, string, string>> hit_check_order_info { get; set; }
        public PlayerUnitHitCheckOrderDisplayModel(string type_name, string shot_name, int boost_id, int total_bullet_count, List<Tuple<int, int, string, string>> hit_check_order_info)
        {
            this.type_name = type_name;
            this.shot_name = shot_name;
            this.boost_id = boost_id;
            this.total_bullet_count = total_bullet_count;
            this.hit_check_order_info = hit_check_order_info;
        }
    }
    public class PlayerUnitHitCheckOrderHelperViewModel
    {
        public List<PlayerUnitData>? QueryUnit { get; set; }
        public List<PlayerUnitHitCheckOrderDisplayModel>? HitCheckOrderDatas { get; set; }
        public string? UnitSymbolName { get; set; }
        public int? BarrageId { get; set; }


        public static string DisplayHitCheckOrder(List<Tuple<int, int, string, string>> hit_check_order_info)
        {
            string text = "";
            foreach (var orderInfo in hit_check_order_info)
            {
                text += "<div class=bullet-wrapper><b><font color=#FF6600>" + orderInfo.Item1 + "</font></b></div>";
                text += "<div class=bullet-wrapper>" + orderInfo.Item2 + "</div>";
                text += "<div class=bullet-wrapper>" + orderInfo.Item3 + "</div>";
                text += "<div class=bullet-wrapper>" + orderInfo.Item4 + "</div>";
            }
            Console.WriteLine(text);
            return text;
        }
    }
}
