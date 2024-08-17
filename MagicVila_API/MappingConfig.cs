using AutoMapper;
using MagicVila_API.Models;
using MagicVila_API.Models.Dto;
namespace MagicVila_API
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Vila, VilaDto>();
            CreateMap<VilaDto, VilaDto>();

            CreateMap<Vila,VilaCreateDto>().ReverseMap();
            CreateMap<Vila, VilaUpdateDto>().ReverseMap();
        }
    }
}
