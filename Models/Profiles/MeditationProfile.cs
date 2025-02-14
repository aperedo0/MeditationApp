// using AutoMapper;
// using MeditationApp.Models;
// using MeditationApp.Dtos;

// public class MeditationProfile : Profile
// {
//     public MeditationProfile()
//     {
//         CreateMap<CreateSessionDto, MeditationSession>();
//     }
// }
using AutoMapper;
using MeditationApp.Dtos;
using MeditationApp.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateSessionDto, MeditationSession>(); // DTO -> Entity
        CreateMap<MeditationSession, CreateSessionDto>(); // Entity -> DTO
    }
}
