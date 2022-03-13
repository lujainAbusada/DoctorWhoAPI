using AutoMapper;
using DoctorWho.Db.DataModels;
using DoctorWho.Web.Models;


namespace DoctorWho.Web.Profiles
{
    public class CompanionProfile : Profile
    {
        public CompanionProfile()
        {
            CreateMap<Companion, CompanionDto>();
            CreateMap<CompanionForCreationDto, Companion>();
        }
    }
}

