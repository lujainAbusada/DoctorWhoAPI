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
    [Route("api/episodes")]
    public class EpisodeController : ControllerBase
    {
        private readonly EpisodeAsyncRepository _episodeAsyncRepository;
        private readonly IMapper _mapper;

        public EpisodeController(IMapper mapper)
        {
            _episodeAsyncRepository = new EpisodeAsyncRepository();
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Episode>>> GetEpisodesAsync()
        {
            var episodesFromRepo = await _episodeAsyncRepository.GetAsyncEpisodes();
            return Ok(_mapper.Map<List<EpisodeDto>>(episodesFromRepo));
        }

        [HttpPost()]
        public async Task<ActionResult<EpisodeDto>> CreateEpisodeAsync(EpisodeForCreationDto episode)
        {
            EpisodeValidator validator = new EpisodeValidator();
            ValidationResult results = validator.Validate(episode);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    return BadRequest(failure);
                }
            }

            var newEpisode = _mapper.Map<Episode>(episode);
            await _episodeAsyncRepository.InsertAsyncEpisode(newEpisode);
            var episodeToReturn = _mapper.Map<EpisodeDto>(newEpisode);
            return Ok(episodeToReturn.DoctorId);
        }
    }
}