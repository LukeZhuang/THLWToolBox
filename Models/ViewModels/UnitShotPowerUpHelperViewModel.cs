using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    public class UnitShotPowerUpHelperViewModel
    {
        // Display model definition
        public class UnitShotPowerUpHelperDisplayModel
        {
            public AttackData attack_data { get; set; }
            public UnitShotPowerUpHelperDisplayModel(AttackData attack_data)
            {
                this.attack_data = attack_data;
            }
        }

        // Display models
        public List<PlayerUnitData>? QueryUnits { get; set; }
        public List<UnitShotPowerUpHelperDisplayModel>? PowerUpDatas { get; set; }

        // Webpage query Parameters
        public string? UnitSymbolName { get; set; }


        // Methods
        public static string CreatePowerUpRateString(int powerUpRate)
        {
            return (0.01 * powerUpRate).ToString("0.00");
        }
    }
}
