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
using Microsoft.AspNetCore.JsonPatch;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/authors")]

    public class AuthorController : ControllerBase
    {
        private readonly AuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IMapper mapper)
        {
            _authorRepository = new AuthorRepository();
            _mapper = mapper;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Author>> GetAuthors()
        {
            var AuthorsFromRepo = _authorRepository.GetAuthors();
            return Ok(_mapper.Map<List<AuthorDto>>(AuthorsFromRepo));
        }

        [HttpGet("{authorId}")]
        public ActionResult<IEnumerable<Author>> GetAuthor(int authorId)
        {
            if (_authorRepository.GetAuthors().Where(d => d.AuthorId == authorId).FirstOrDefault() == null)
            {
                return NotFound();
            }
            else
            {
                var AuthorFromRepo = _authorRepository.GetAuthors().Where(d => d.AuthorId == authorId).FirstOrDefault();
                return Ok(_mapper.Map<AuthorDto>(AuthorFromRepo));
            }
        }

        [HttpPatch("{authorId}")]
        public ActionResult<AuthorDto> UpdateAuthor(int authorId,JsonPatchDocument<AuthorForUpdateDto> patchDocument)
        {
            var authorFromRepo = _authorRepository.GetAuthors().Where(d => d.AuthorId == authorId).FirstOrDefault();
            if ( authorFromRepo== null)
            {
                return NotFound();
            }
            else
            {
                var authorToPatch = _mapper.Map<AuthorForUpdateDto>(authorFromRepo);
                patchDocument.ApplyTo(authorToPatch);
                _mapper.Map(authorToPatch, authorFromRepo);
                _authorRepository.UpdateAuthorName(authorId, authorFromRepo.AuthorName);
                return Ok("Author's Name Updated");
            }
        }
    }
}