using AutoMapper;
using DoctorWho.Db.DataModels;
using DoctorWho.Web.Models;

namespace DoctorWho.Web.Profiles
{
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorForUpdateDto, Author>();
            CreateMap<Author, AuthorForUpdateDto>();
        }
    }
}
