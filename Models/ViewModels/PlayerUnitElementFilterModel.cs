using THLWToolBox.Models.DataTypes;
using static THLWToolBox.Helpers.TypeHelper;

namespace THLWToolBox.Models.ViewModels
{
    public class PlayerUnitElementDisplayModel
    {
        public PlayerUnitData PlayerUnitData { get; set; }
        public List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> UnitBulletList { get; set; }
        public double MainScore { get; set; }
        public double SubScore { get; set; }
        public PlayerUnitElementDisplayModel(PlayerUnitData playerUnitData, List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> unitBulletList, double mainScore, double subScore)
        {
            PlayerUnitData = playerUnitData;
            UnitBulletList = unitBulletList;
            MainScore = mainScore;
            SubScore = subScore;
        }
    }
    public class PlayerUnitElementFilterModel
    {
        public List<PlayerUnitElementDisplayModel> QueryResults { get; set; }
        public bool? Shot1 { get; set; }
        public bool? Shot2 { get; set; }
        public bool? NormalSpellcard { get; set; }
        public bool? LastWord { get; set; }
        public int? MainBulletElement { get; set; }
        public int? MainBulletType { get; set; }
        public int? MainBulletCategory { get; set; }
        public int? SubBulletElement { get; set; }
        public int? SubBulletType { get; set; }
        public int? SubBulletCategory { get; set; }



        public static string BulletStrStyle(int BulletStatus, string ElementStr)
        {
            // 0-bit: yin/yang
            // 1-bit: selected by main
            // 2-bit: selected by sub

            string color = "";
            bool selected = true;

            if ((BulletStatus >> 1 & 1) == 1)
                color = "#FF6600";
            else if ((BulletStatus >> 2 & 1) == 1)
                color = "#4CAFFF";
            else
            {
                color = "#000000";
                selected = false;
            }

            string str = "<font color=" + color + ">" + ElementStr + "</font>";
            if (selected)
            {
                str = "<b>" + str + "</b>";
            }
            return str;
        }

        public static string DisplayShotElements(List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> unitBulletList)
        {
            string text = "";
            foreach (var shot in unitBulletList)
            {
                string rowText = "<div class=shot-row>";
                string shotName = shot.Item1;
                rowText += "<div class=shot-name-grid>" + shotName + "</div>";
                rowText += "<div class=element-grids>";
                foreach (var bulletRecord in shot.Item2)
                {
                    PlayerUnitBulletData? bullet = bulletRecord.Item1;
                    int status = bulletRecord.Item2;
                    string bulletStr = (bullet == null ? "null" : BulletStrStyle(status, GetElementTypeString(bullet.element) + "-" + GetBulletTypeString(bullet.category)));
                    rowText += "<div class=\"element-grid " + ((status & 1) == 1 ? "yang-background" : "yin-background") + "\">" + bulletStr + "</div>";
                }
                rowText += "</div>";
                rowText += "</div>";
                text += rowText;
            }
            return text;
        }
    }
}
