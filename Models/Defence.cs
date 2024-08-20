namespace IronDomeAPI.Models
{
    public class Defence
    {
        public Guid? Id { get; set; }
        public static int MissileCount { get; set; }
        public static List<string> MissileTypes { get; set; }
        public static string Status { get; set; }
    }
}
