namespace THLWToolBox.Models
{
    public class PlayerUnitHitCheckOrderData
    {
        public int id {  get; set; }
        public int unit_id { get; set; }
        public int barrage_id { get; set; }
        public int boost_id { get; set; }
        public string hit_check_order { get; set; }
    }
}
