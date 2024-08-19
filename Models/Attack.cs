using System.ComponentModel.DataAnnotations;

namespace IronDomeAPI.Models
{
    public class Attack
    {
        public Guid? id { get; set; }

        [AllowedValues("iran", "hutim")]
        public string origin { get; set; }

        [Range(50, 500)]
        public int? damege { get; set; }
        public string type { get; set; }
        public string? status { get; set; }
        public DateTime? StartedAt { get; set; }
    }
}
