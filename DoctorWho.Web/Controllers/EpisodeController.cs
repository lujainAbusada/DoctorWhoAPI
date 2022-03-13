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
    [Route("api/episodes")]
    public class EpisodeController : ControllerBase
    {
        private readonly EpisodeRepository _episodeRepository;
        private readonly IMapper _mapper;

        public EpisodeController(IMapper mapper)
        {
            _episodeRepository = new EpisodeRepository();
            _mapper = mapper;

        }

        [HttpGet()]
        public ActionResult<IEnumerable<Episode>> GetEpisodes()
        {
            var episodesFromRepo = _episodeRepository.GetEpisodes();
            return Ok(_mapper.Map<List<EpisodeDto>>(episodesFromRepo));
        }       
    }
}