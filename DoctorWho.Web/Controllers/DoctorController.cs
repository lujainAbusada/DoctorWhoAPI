using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorWho.Db.Repositories;
using DoctorWho.Db.DataModels;
using DoctorWho.Web.Profiles;
using DoctorWho.Web.Models;
using AutoMapper;
using DoctorWho.Web.Validators;
using FluentValidation.Results;
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

        [HttpPut()]
        public ActionResult<DoctorDto> UpsertDoctor(DoctorForCreationDto doctor)
        {

            DoctorValidator validator = new DoctorValidator();

            ValidationResult results = validator.Validate(doctor);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    return BadRequest(failure);
                }
            }

            var newDoctor = _mapper.Map<DoctorWho.Db.DataModels.Doctor>(doctor);
            _doctorRepository.InsertDoctor(newDoctor);

            var doctorToReturn = _mapper.Map<DoctorDto>(newDoctor);
            return CreatedAtRoute("GetDoctor", new { doctorId = doctorToReturn.DoctorId }, doctorToReturn);

        }
    }
}