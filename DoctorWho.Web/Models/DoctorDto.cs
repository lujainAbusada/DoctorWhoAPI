using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorWho.Web.Models
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public long DoctorNumber { get; set; }
        public string DoctorName { get; set; }
        public string BirthDate { get; set; }
    }
}