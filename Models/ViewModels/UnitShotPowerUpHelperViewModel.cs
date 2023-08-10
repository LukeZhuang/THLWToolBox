using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    // Display model definition
    public class UnitShotPowerUpDisplayModel
    {
        public AttackData AttackData { get; set; }
        public UnitShotPowerUpDisplayModel(AttackData attackData)
        {
            this.AttackData = attackData;
        }
    }

    public class UnitShotPowerUpHelperViewModel
    {
        // Display models
        public List<PlayerUnitData>? QueryUnits { get; set; }
        public List<UnitShotPowerUpDisplayModel>? PowerUpDatas { get; set; }

        // Webpage query Parameters
        public string? UnitSymbolName { get; set; }


        // Methods
        public static string CreatePowerUpRateString(int powerUpRate)
        {
            return (0.01 * powerUpRate).ToString("0.00");
        }
    }
}
