namespace IronDomeAPI.Models
{
    public class Attack
    {
        public Guid? id { get; set; }
        public string origin { get; set; }
        public string type { get; set; }
        public bool status { get; set; } = false;

    }
}
