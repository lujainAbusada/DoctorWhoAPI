using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DoctorWho.Db.Repositories;
using DoctorWho.Db.DataModels;
using DoctorWho.Web.Models;
using AutoMapper;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    public class CompanionController : ControllerBase
    {
        private readonly EpisodeRepository _episodeRepository;
        private readonly CompanionRepository _companionRepository;
        private readonly IMapper _mapper;

        public CompanionController(IMapper mapper)
        {
            _episodeRepository = new EpisodeRepository();
            _companionRepository = new CompanionRepository();
            _mapper = mapper;
        }

        [HttpGet("api/companions")]
        public ActionResult<IEnumerable<Enemy>> GetCompanions()
        {
            var companionsFromRepo = _companionRepository.GetCompanions();
            return Ok(_mapper.Map<List<CompanionDto>>(companionsFromRepo));
        }

        [HttpPost("api/episodes/{episodeId}/companions")]
        public ActionResult<CompanionDto> AddCompanionToEpisode(CompanionForCreationDto companion, int episodeId)
        {
            var newCompanion = _mapper.Map<Companion>(companion);
            _companionRepository.InsertCompanion(newCompanion);
            _episodeRepository.AddCompanion(_episodeRepository.GetEpisodes().Where(e => e.EpisodeId == episodeId).FirstOrDefault(), newCompanion.CompanionId);
            return Ok("Companion Added To Episode");
        }
    }
}
