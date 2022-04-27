using AutoMapper;
using DoctorWho.Db.DataModels;
using DoctorWho.Db.Repositories;
using DoctorWho.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    public class CompanionController : ControllerBase
    {
        private readonly CompanionAsyncRepository _companionAsyncRepository;
        private readonly IMapper _mapper;
        private readonly EpisodeAsyncRepository _episodeAsyncRepository;

        public CompanionController(IMapper mapper)
        {
            _companionAsyncRepository = new CompanionAsyncRepository();
            _episodeAsyncRepository = new EpisodeAsyncRepository();
            _mapper = mapper;
        }

        [HttpGet("api/companions")]
        public async Task<ActionResult> GetCompanions()
        {
            var companionsFromRepo = await _companionAsyncRepository.GetAsyncCompanions();
            return Ok(_mapper.Map<List<CompanionDto>>(companionsFromRepo));
        }

        [HttpPost("api/episodes/{episodeId}/companions")]
        public async Task<ActionResult> AddCompanionToEpisode(CompanionForCreationDto companion, int episodeId)
        {
            var newCompanion = _mapper.Map<Companion>(companion);
            await _companionAsyncRepository.InsertAsyncCompanion(newCompanion);
            await _episodeAsyncRepository.AddAsyncCompanion(await _episodeAsyncRepository.GetAsyncEpisode(episodeId), newCompanion.CompanionId);
            return Ok("Companion Added To Episode");
        }
    }
}
