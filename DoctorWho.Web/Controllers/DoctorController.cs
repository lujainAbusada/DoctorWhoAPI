using AutoMapper;
using DoctorWho.Db.DataModels;
using DoctorWho.Db.Repositories;
using DoctorWho.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/doctors")]

    public class DoctorController : ControllerBase
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorController(IMapper mapper)
        {
            _doctorRepository = new DoctorRepository();
            _mapper = mapper;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            var doctorsFromRepo = _doctorRepository.GetDoctors();
            return Ok(_mapper.Map<List<DoctorDto>>(doctorsFromRepo));
        }

        [HttpGet("{doctorId}", Name = "GetDoctor")]
        public ActionResult<IEnumerable<Doctor>> GetDoctor(int doctorId)
        {
            if (_doctorRepository.GetDoctors().Where(d => d.DoctorId == doctorId).FirstOrDefault() == null)
            {
                return NotFound();
            }
            else
            {
                var doctorFromRepo = _doctorRepository.GetDoctors().Where(d => d.DoctorId == doctorId).FirstOrDefault();
                return Ok(_mapper.Map<DoctorDto>(doctorFromRepo));
            }
        }
    }
}