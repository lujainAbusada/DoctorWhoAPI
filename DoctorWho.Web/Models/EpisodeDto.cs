using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorWho.Web.Models
{
    public class EpisodeDto
    {
        public int EpisodeId { get; set; }
        public int SeriesNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeType { get; set; }
        public string Title { get; set; }
        public string EpisodeDate { get; set; }
        public string Notes { get; set; }
        public int DoctorId { get; set; }
        public int AuthorId { get; set; }
    }
}