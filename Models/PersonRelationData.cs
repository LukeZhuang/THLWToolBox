namespace THLWToolBox.Models
{
    public class PersonRelationData
    {
        public int id { get; set; }
        public int person_id { get; set; }
        public int target_person_id { get; set; }
        public string description { get; set; }
    }
}
