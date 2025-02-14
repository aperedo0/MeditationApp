using AutoMapper;
using MeditationApp.Models;
using MeditationApp.Dtos;

namespace MeditationApp.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateSessionDto, MeditationSession>();
        }
    }
}
