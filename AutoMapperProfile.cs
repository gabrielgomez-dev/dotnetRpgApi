using AutoMapper;
using dotnetRpgApi.Dtos.Character;
using dotnetRpgApi.Dtos.Skill;
using dotnetRpgApi.Dtos.Weapon;
using dotnetRpgApi.Models;

namespace dotnetRpgApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, CharacterResponseDto>(); 
            CreateMap<CharacterRequestDto, Character>(); 
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}