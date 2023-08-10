using System.ComponentModel.DataAnnotations;

namespace THLWToolBox.Models.DataTypes
{
    public class VersionHistoryData
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
    }
}
