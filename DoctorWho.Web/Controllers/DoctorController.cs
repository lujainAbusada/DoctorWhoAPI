using AutoMapper;
using DoctorWho.Db.DataModels;
using DoctorWho.Db.Repositories;
using DoctorWho.Web.Models;
using DoctorWho.Web.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorAsyncRepository _doctorAsyncRepository;
        private readonly IMapper _mapper;

        public DoctorController(IMapper mapper)
        {
            _doctorAsyncRepository = new DoctorAsyncRepository();
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsAsync()
        {
            var doctorsFromRepo = await _doctorAsyncRepository.GetAsyncDoctors();
            return Ok(_mapper.Map<List<DoctorDto>>(doctorsFromRepo));
        }

        [HttpGet("{doctorId}", Name = "GetDoctor")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorAsync(int doctorId)
        {
            var item = await _doctorAsyncRepository.GetAsyncDoctor(doctorId);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DoctorDto>(item));
        }

        [HttpPut()]
        public async Task<ActionResult<DoctorDto>> UpsertDoctorAsync(DoctorForCreationDto doctor)
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
            await _doctorAsyncRepository.InsertAsyncDoctor(newDoctor);
            var doctorToReturn = _mapper.Map<DoctorDto>(newDoctor);
            return CreatedAtRoute("GetDoctor", new { doctorId = doctorToReturn.DoctorId }, doctorToReturn);
        }

        [HttpDelete("{doctorId}")]
        public async Task<ActionResult> DeleteDoctorAsync(int doctorId)
        {
            if (await _doctorAsyncRepository.GetAsyncDoctor(doctorId) == null)
            {
                return NotFound();
            }
            else
                await _doctorAsyncRepository.DeleteAsyncDoctor(doctorId);
            return NoContent();
        }
    }
}