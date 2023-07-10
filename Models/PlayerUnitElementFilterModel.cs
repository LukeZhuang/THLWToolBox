namespace THLWToolBox.Models
{
    public class PlayerUnitElementDisplayModel
    {
        public PlayerUnitData PlayerUnitData { get; set; }
        public List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> UnitBulletList { get; set; }
        public double MainScore { get; set; }
        public double SubScore { get; set; }
        public PlayerUnitElementDisplayModel(PlayerUnitData playerUnitData, List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> unitBulletList, double mainScore, double subScore)
        {
            this.PlayerUnitData = playerUnitData;
            this.UnitBulletList = unitBulletList;
            this.MainScore = mainScore;
            this.SubScore = subScore;
        }
    }
    public class PlayerUnitElementFilterModel
    {
        public List<PlayerUnitElementDisplayModel> QueryResults { get; set; }
        public int? ShotType { get; set; }
        public int? MainBulletElement { get; set; }
        public int? MainBulletType { get; set; }
        public int? MainBulletCategory { get; set; }
        public int? SubBulletElement { get; set; }
        public int? SubBulletType { get; set; }
        public int? SubBulletCategory { get; set; }
    }
}
