
using System.ComponentModel.DataAnnotations;


namespace DoctorWho.Web.Models
{
    public class EpisodeForCreationDto
    {
        public int SeriesNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeType { get; set; }
        public string Title { get; set; }
        public string EpisodeDate { get; set; }
        public string Notes { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}