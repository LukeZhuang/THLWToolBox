using THLWToolBox.Models.DataTypes;
using static THLWToolBox.Models.GeneralModels;

namespace THLWToolBox.Models.ViewModels
{
    public class BarrierBreakingInfo
    {
        public AttackData AttackData { get; set; }
        public List<string> MagazineBarrierBreaking { get; set; }
        public BarrierBreakingInfo(AttackData attackData, List<string> magazineBarrierBreaking)
        {
            AttackData = attackData;
            MagazineBarrierBreaking = magazineBarrierBreaking;
        }
    }

    public class UnitBarrierStatusDisplayModel
    {
        public PlayerUnitData Unit { get; set; }
        public string? BarrierAbility { get; set; }
        public List<SkillEffectInfo>? BarrierSkillInfos { get; set; }
        public List<BarrierBreakingInfo>? BarrierBreakingInfos { get; set; }
        public string? AbilityBarrierStatusType { get; set; }
        public string? SkillBarrierStatusType { get; set; }
        public string? BreakingBarrierStatusType { get; set; }
        public UnitBarrierStatusDisplayModel(PlayerUnitData unit, string? barrierAbility, List<SkillEffectInfo>? barrierSkillInfos, List<BarrierBreakingInfo>? barrierBreakingInfos, string? abilityBarrierStatusType, string? skillBarrierStatusType, string? breakingBarrierStatusType)
        {
            Unit = unit;
            BarrierAbility = barrierAbility;
            BarrierSkillInfos = barrierSkillInfos;
            BarrierBreakingInfos = barrierBreakingInfos;
            AbilityBarrierStatusType = abilityBarrierStatusType;
            SkillBarrierStatusType = skillBarrierStatusType;
            BreakingBarrierStatusType = breakingBarrierStatusType;
        }
    }
    public class UnitBarrierStatusFilterViewModel
    {
        // Display models
        public List<UnitBarrierStatusDisplayModel>? UnitBarrierStatusInfos { get; set; }

        // Webpage query Parameters
        public bool? SimplifiedEffect { get; set; }

        public int? AbilityBarrierStatusType { get; set; }
        public int? BarrierAbility { get; set; }

        public int? SkillBarrierStatusType { get; set; }
        public bool? Self { get; set; }
        public bool? SelfAll { get; set; }
        public bool? Enemy { get; set; }
        public bool? EnemyAll { get; set; }

        public int? BreakingBarrierStatusType { get; set; }
        public bool? SpreadShot { get; set; }
        public bool? FocusShot { get; set; }
        public bool? NormalSpellcard { get; set; }
        public bool? LastWord { get; set; }


        // Methods
        public AttackSelectionModel CreateAttackSelectionModel()
        {
            return new AttackSelectionModel(SpreadShot.GetValueOrDefault(true),
                                            FocusShot.GetValueOrDefault(true),
                                            NormalSpellcard.GetValueOrDefault(true),
                                            LastWord.GetValueOrDefault(true));
        }

        public HashSet<int> GetBarrierSkillRangeSelector()
        {
            HashSet<int> ranges = new();
            if (Self.GetValueOrDefault(false))
                ranges.Add(1);
            if (SelfAll.GetValueOrDefault(false))
                ranges.Add(2);
            if (Enemy.GetValueOrDefault(false))
                ranges.Add(3);
            if (EnemyAll.GetValueOrDefault(false))
                ranges.Add(4);
            return ranges;
        }

        public static string DisplayUnitBarrierStatus(UnitBarrierStatusDisplayModel unitBarrierStatus)
        {
            string text = "<div class=\"barrier-status-display-grid\">";
            if (unitBarrierStatus.BarrierAbility != null)
            {
                text += "<div class=\"barrier-status-wrapper\">[" + unitBarrierStatus.AbilityBarrierStatusType + "]相关能力" + "</div>";
                text += "<div class=\"barrier-status-wrapper\">" + unitBarrierStatus.BarrierAbility + "</div>";
            }
            if (unitBarrierStatus.BarrierBreakingInfos != null)
            {
                text += "<div class=\"barrier-status-wrapper\">[" + unitBarrierStatus.BreakingBarrierStatusType + "]击破" + "</div>";
                text += "<div class=\"barrier-status-wrapper\">";
                text += "<div class=\"barrier-breaking-grid\">";
                foreach (var barrierBreakingInfo in unitBarrierStatus.BarrierBreakingInfos)
                {
                    text += "<div class=\"barrier-breaking-wrapper\">" + barrierBreakingInfo.AttackData.AttackTypeName + "</div>";
                    text += "<div class=\"barrier-breaking-wrapper\">" + barrierBreakingInfo.AttackData.Name + "</div>";
                    text += "<div class=\"barrier-breaking-wrapper\">" + string.Join("<br>", barrierBreakingInfo.MagazineBarrierBreaking) + "</div>";
                }
                text += "</div>";
                text += "</div>";
            }
            text += "</div>";
            return text;
        }
    }
}
