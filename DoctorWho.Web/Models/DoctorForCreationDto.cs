using System.ComponentModel.DataAnnotations;

namespace DoctorWho.Web.Models
{
    public class DoctorForCreationDto
    {
        [Required]
        public long DoctorNumber { get; set; }
        [Required]
        public string DoctorName { get; set; }
        public string BirthDate { get; set; }
        public string FirstEpisodeDate { get; set; }
        public string LastEpisodeDate { get; set; }
    }
}