using AutoMapper;
using dotnetRpgApi.Dtos.Character;
using dotnetRpgApi.Models;

namespace dotnetRpgApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, CharacterResponseDto>(); // convert character to response
            CreateMap<CharacterRequestDto, Character>(); // convert request character to a character
        }
    }
}