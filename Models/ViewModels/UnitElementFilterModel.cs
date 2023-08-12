using THLWToolBox.Models.DataTypes;
using static THLWToolBox.Helpers.TypeHelper;
using static THLWToolBox.Models.GeneralModels;

namespace THLWToolBox.Models.ViewModels
{
    public class MagazineElementInfo
    {
        public bool IsYang { get; set; }
        public bool IsSelectedByBox1 { get; set; }
        public bool IsSelectedByBox2 { get; set; }
        public string BulletElementTypeString { get; set; }
        public MagazineElementInfo(bool isYang, bool isSelectedByBox1, bool isSelectedByBox2, string bulletElementTypeString)
        {
            IsYang = isYang;
            IsSelectedByBox1 = isSelectedByBox1;
            IsSelectedByBox2 = isSelectedByBox2;
            BulletElementTypeString = bulletElementTypeString;
        }
    }
    public class AttackElementInfo
    {
        public AttackData AttackData { get; set; }
        public List<MagazineElementInfo> MagazineElementInfos { get; set; }
        public AttackElementInfo(AttackData attackData, List<MagazineElementInfo> magazineElementInfos)
        {
            AttackData = attackData;
            MagazineElementInfos = magazineElementInfos;
        }
    }
    public class UnitElementDisplayModel
    {
        public PlayerUnitData Unit { get; set; }
        public List<AttackElementInfo> UnitAttackElementInfos { get; set; }
        public double TotalScore { get; set; }
        public UnitElementDisplayModel(PlayerUnitData unit, List<AttackElementInfo> unitAttackElementInfos, double totalScore)
        {
            Unit = unit;
            UnitAttackElementInfos = unitAttackElementInfos;
            TotalScore = totalScore;
        }
    }
    public class UnitElementFilterModel
    {
        // Display models
        public List<UnitElementDisplayModel>? UnitElementInfos { get; set; }

        // Webpage query Parameters
        public bool? SpreadShot { get; set; }
        public bool? FocusShot { get; set; }
        public bool? NormalSpellcard { get; set; }
        public bool? LastWord { get; set; }
        // TODO: add selectLists
        public int? BulletElement1 { get; set; }
        public int? BulletCategory1 { get; set; }
        public int? BulletType1 { get; set; }
        public int? BulletElement2 { get; set; }
        public int? BulletCategory2 { get; set; }
        public int? BulletType2 { get; set; }


        // Methods
        public AttackSelectionModel CreateAttackSelectionModel()
        {
            return new AttackSelectionModel(SpreadShot.GetValueOrDefault(true),
                                            FocusShot.GetValueOrDefault(true),
                                            NormalSpellcard.GetValueOrDefault(true),
                                            LastWord.GetValueOrDefault(true));
        }
        public static string CreateMagazineElementString(MagazineElementInfo magazineElementInfo)
        {
            string color = "";
            bool selected = true;

            if (magazineElementInfo.IsSelectedByBox1)
                color = "#FF6600";
            else if (magazineElementInfo.IsSelectedByBox2)
                color = "#4CAFFF";
            else
            {
                color = "#000000";
                selected = false;
            }

            string text = "<font color=" + color + ">" + magazineElementInfo.BulletElementTypeString + "</font>";
            if (selected)
                text = "<b>" + text + "</b>";
            return text;
        }

        public static string DisplayUnitElement(List<AttackElementInfo> unitAttackElementInfos)
        {
            string attackWrapper = "<div class=\"attack-wrapper\">";

            string text = "<div class=\"attacks-grid\">";
            text += attackWrapper + "射击类型" + "</div>";
            text += attackWrapper + "子弹信息" + "</div>";
            foreach (var unitAttackElementInfo in unitAttackElementInfos)
            {
                text += attackWrapper + unitAttackElementInfo.AttackData.AttackTypeName + "</div>";
                text += "<div class=\"magazines-grid\">";
                foreach (var magazineCriticalHit in unitAttackElementInfo.MagazineElementInfos)
                {
                    text += "<div class=\"magazine-wrapper " + (magazineCriticalHit.IsYang ? "yang-background" : "yin-background") + "\">" + CreateMagazineElementString(magazineCriticalHit) + "</div>";
                }
                text += "</div>";
            }
            text += "</div>";
            return text;
        }

        public static string CreateTotalScoreString(UnitElementDisplayModel unitElementInfo)
        {
            return (unitElementInfo.TotalScore).ToString("0.00");
        }
    }
}
