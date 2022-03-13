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
    public class EnemyController : ControllerBase
    {
        private readonly EpisodeRepository _episodeRepository;
        private readonly EnemyRepository _enemyRepository;
        private readonly IMapper _mapper;

        public EnemyController(IMapper mapper)
        {
            _episodeRepository = new EpisodeRepository();
            _enemyRepository = new EnemyRepository();
            _mapper = mapper;
        }

        [HttpGet("api/enemies")]
        public ActionResult<IEnumerable<Enemy>> GetEnemies()
        {
            var enemiesFromRepo = _enemyRepository.GetEnemies();
            return Ok(_mapper.Map<List<EnemyDto>>(enemiesFromRepo));
        }

        [HttpPost("api/episodes/{episodeId}/enemies")]
        public ActionResult<EpisodeDto> AddEnemyToEpisode(EnemyForCreationDto enemy, int episodeId)
        {
            var newEnemy = _mapper.Map<Enemy>(enemy);
            _enemyRepository.InsertEnemy(newEnemy);
            _episodeRepository.AddEnemy(_episodeRepository.GetEpisodes().Where(e => e.EpisodeId == episodeId).FirstOrDefault(), newEnemy.EnemyId);
            return Ok("Enemy Added To Episode");
        }
    }
}
