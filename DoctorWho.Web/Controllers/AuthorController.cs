using AutoMapper;
using DoctorWho.Db.Repositories;
using DoctorWho.Web.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorAsyncRepository _authorAsyncRepository;
        private readonly IMapper _mapper;

        public AuthorController(IMapper mapper)
        {
            _authorAsyncRepository = new AuthorAsyncRepository();
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult> GetAuthors()
        {
            var AuthorsFromRepo = await _authorAsyncRepository.GetAsyncAuthors();
            return Ok(_mapper.Map<List<AuthorDto>>(AuthorsFromRepo));
        }

        [HttpGet("{authorId}")]
        public async Task<ActionResult> GetAuthor(int authorId)
        {
            var item = await _authorAsyncRepository.GetAsyncAuthor(authorId);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AuthorDto>(item));
        }

        [HttpPatch("{authorId}")]
        public async Task<IActionResult> UpdateAuthor(int authorId, JsonPatchDocument<AuthorForUpdateDto> patchDocument)
        {
            var authorFromRepo = await _authorAsyncRepository.GetAsyncAuthor(authorId);
            if (authorFromRepo == null)
            {
                return NotFound();
            }
            else
            {
                var authorToPatch = _mapper.Map<AuthorForUpdateDto>(authorFromRepo);
                patchDocument.ApplyTo(authorToPatch);
                var finalauhtor = _mapper.Map(authorToPatch, authorFromRepo);
                await _authorAsyncRepository.UpdateAsyncAuthor(finalauhtor);
                return Ok("Author Updated");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorAsyncRepository.DeleteAsyncAuthor(id);
            return NoContent();
        }
    }
}
