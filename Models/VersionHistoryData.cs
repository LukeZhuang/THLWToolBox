using System.ComponentModel.DataAnnotations;

namespace THLWToolBox.Models
{
    public class VersionHistoryData
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? description { get; set; }
    }
}
