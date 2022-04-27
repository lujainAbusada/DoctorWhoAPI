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
    public class EnemyController : ControllerBase
    {
        private readonly EpisodeAsyncRepository _episodeAsyncRepository;
        private readonly EnemyAsyncRepository _enemyAsyncRepository;
        private readonly IMapper _mapper;

        public EnemyController(IMapper mapper)
        {
            _episodeAsyncRepository = new EpisodeAsyncRepository();
            _enemyAsyncRepository = new EnemyAsyncRepository();
            _mapper = mapper;
        }

        [HttpGet("api/enemies")]
        public async Task<ActionResult<IEnumerable<Enemy>>> GetEnemiesAsync()
        {
            var enemiesFromRepo = await _enemyAsyncRepository.GetAsyncEnemies();
            return Ok(_mapper.Map<List<EnemyDto>>(enemiesFromRepo));
        }

        [HttpPost("api/episodes/{episodeId}/enemies")]
        public async Task<ActionResult<EpisodeDto>> AddEnemyToEpisodeAsync(EnemyForCreationDto enemy, int episodeId)
        {
            var newEnemy = _mapper.Map<Enemy>(enemy);
            await _enemyAsyncRepository.InsertAsyncEnemy(newEnemy);
            await _episodeAsyncRepository.AddAsyncEnemy(await _episodeAsyncRepository.GetAsyncEpisode(episodeId), newEnemy.EnemyId);
            return Ok("Enemy Added To Episode");
        }
    }
}
