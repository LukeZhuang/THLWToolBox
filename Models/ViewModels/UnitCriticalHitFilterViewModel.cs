using THLWToolBox.Models.DataTypes;
using static THLWToolBox.Models.GeneralModels;

namespace THLWToolBox.Models.ViewModels
{
    public class MagazineCriticalHitInfo
    {
        public int Id { get; set; }
        public string CriticalHits { get; set; }
        public MagazineCriticalHitInfo(int id, string criticalHits)
        {
            Id = id;
            CriticalHits = criticalHits;
        }
    }
    public class AttackCriticalHitInfo
    {
        public AttackData AttackData { get; set; }
        public List<MagazineCriticalHitInfo> MagazineCriticalHitInfos { get; set; }
        public AttackCriticalHitInfo(AttackData attackData, List<MagazineCriticalHitInfo> magazineCriticalHitInfos)
        {
            AttackData = attackData;
            MagazineCriticalHitInfos = magazineCriticalHitInfos;
        }
    }

    // Display model definition
    public class UnitCriticalHitDisplayModel
    {
        public PlayerUnitData Unit { get; set; }
        public List<AttackCriticalHitInfo> UnitAttackCriticalHitInfos { get; set; }
        public double TotalScore { get; set; }
        public UnitCriticalHitDisplayModel(PlayerUnitData unit, List<AttackCriticalHitInfo> unitAttackCriticalHitInfos, double totalScore)
        {
            Unit = unit;
            UnitAttackCriticalHitInfos = unitAttackCriticalHitInfos;
            TotalScore = totalScore;
        }
    }
    public class UnitCriticalHitFilterViewModel
    {
        // Display models
        public List<UnitRaceDisplayModel>? QueryUnits { get; set; }
        public List<UnitCriticalHitDisplayModel>? CriticalMatchUnits { get; set; }

        // Webpage query Parameters
        public string? UnitSymbolName { get; set; }
        public string? RaceName { get; set; }
        public bool? SpreadShot { get; set; }
        public bool? FocusShot { get; set; }
        public bool? NormalSpellcard { get; set; }
        public bool? LastWord { get; set; }

        // Other Helper Data
        public string? AllRacesString { get; set; }


        // Methods
        public AttackSelectionModel CreateAttackSelectionModel()
        {
            return new AttackSelectionModel(SpreadShot.GetValueOrDefault(true),
                                            FocusShot.GetValueOrDefault(true),
                                            NormalSpellcard.GetValueOrDefault(true),
                                            LastWord.GetValueOrDefault(true));
        }
        public static string DisplayUnitCriticalHit(List<AttackCriticalHitInfo> unitAttackCriticalHitInfos)
        {
            string attackWrapper = "<div class=\"attack-wrapper\">";
            string magazineWrapper = "<div class=\"magazine-wrapper\">";

            string text = "<div class=\"attacks-grid\">";
            text += attackWrapper + "射击类型" + "</div>";
            text += "<div class=\"magazines-grid\">";
            text += magazineWrapper + "段落" + "</div>";
            text += magazineWrapper + "匹配的特攻" + "</div>";
            text += "</div>";
            foreach (var unitAttackCriticalHitInfo in unitAttackCriticalHitInfos)
            {
                text += attackWrapper + unitAttackCriticalHitInfo.AttackData.AttackTypeName + "</div>";
                text += "<div class=\"magazines-grid\">";
                foreach (var magazineCriticalHit in unitAttackCriticalHitInfo.MagazineCriticalHitInfos) {
                    text += magazineWrapper + magazineCriticalHit.Id + "</div>";
                    text += magazineWrapper + "<b><font color=#FF6600>" + magazineCriticalHit.CriticalHits + "</font></b>" + "</div>";
                }
                text += "</div>";
            }
            text += "</div>";
            return text;
        }
    }
}
