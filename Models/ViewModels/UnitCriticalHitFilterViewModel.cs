using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static Python.Runtime.TypeSpec;
using static THLWToolBox.Models.GeneralModels;

namespace THLWToolBox.Models.ViewModels
{
    public class MagazineCriticalHitInfo
    {
        public int Id { get; set; }
        public string CriticalHits { get; set; }
        public MagazineCriticalHitInfo(int id, string criticalHits)
        {
            this.Id = id;
            this.CriticalHits = criticalHits;
        }
    }
    public class AttackCriticalHitInfo
    {
        public AttackData AttackData { get; set; }
        public List<MagazineCriticalHitInfo> MagazineCriticalHits { get; set; }
        public AttackCriticalHitInfo(AttackData attackData, List<MagazineCriticalHitInfo> magazineCriticalHits)
        {
            this.AttackData = attackData;
            this.MagazineCriticalHits = magazineCriticalHits;
        }
    }

    // Display model definition
    public class UnitCriticalHitDisplayModel
    {
        public PlayerUnitData Unit { get; set; }
        public List<AttackCriticalHitInfo> UnitAttackCriticalHitList { get; set; }
        public double TotalScore { get; set; }
        public UnitCriticalHitDisplayModel(PlayerUnitData unit, List<AttackCriticalHitInfo> unitAttackCriticalHitList, double totalScore)
        {
            this.Unit = unit;
            this.UnitAttackCriticalHitList = unitAttackCriticalHitList;
            this.TotalScore = totalScore;
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
        public string? AllRacesStr { get; set; }


        // Methods
        public AttackSelectionModel CreateAttackSelectionModel()
        {
            return new AttackSelectionModel(SpreadShot.GetValueOrDefault(true),
                                            FocusShot.GetValueOrDefault(true),
                                            NormalSpellcard.GetValueOrDefault(true),
                                            LastWord.GetValueOrDefault(true));
        }
        public static string DisplayUnitCriticalHit(List<AttackCriticalHitInfo> unitAttackCriticalHitList)
        {
            string attackWrapper = "<div class=\"attack-wrapper\">";
            string magazineWrapper = "<div class=\"magazine-wrapper\">";

            string text = "<div class=\"attacks-grid\">";
            text += attackWrapper + "射击类型" + "</div>";
            text += "<div class=\"magazines-grid\">";
            text += magazineWrapper + "段落" + "</div>";
            text += magazineWrapper + "匹配的特攻" + "</div>";
            text += "</div>";
            foreach (var unitAttackCriticalHit in unitAttackCriticalHitList)
            {
                text += attackWrapper + unitAttackCriticalHit.AttackData.AttackTypeName + "</div>";
                text += "<div class=\"magazines-grid\">";
                foreach (var magazineCriticalHit in unitAttackCriticalHit.MagazineCriticalHits) {
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
