using AutoMapper;
using DoctorWho.Db.DataModels;
using DoctorWho.Web.Models;

namespace DoctorWho.Web.Profiles
{
    public class EpisodeProfile : Profile
    {
        public EpisodeProfile()
        {
            CreateMap<Episode, EpisodeDto>();
        }
    }
}