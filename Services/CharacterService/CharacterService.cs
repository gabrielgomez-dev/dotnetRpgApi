using AutoMapper;
using dotnetRpgApi.Dtos.Character;
using dotnetRpgApi.Enums;
using dotnetRpgApi.Models;

namespace dotnetRpgApi.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private List<Character> characters = new List<Character>
        {
            new Character { Id = 1, Name = "Eldron", HitPoints = 100, Strength = 15, Defense = 10, Intelligence = 20, Class = RpgClass.Mage },
            new Character { Id = 2, Name = "Lyanna", HitPoints = 80, Strength = 10, Defense = 12, Intelligence = 18, Class = RpgClass.Warrior },
            new Character { Id = 3, Name = "Thorn", HitPoints = 120, Strength = 18, Defense = 8, Intelligence = 12, Class = RpgClass.Rogue },
            new Character { Id = 4, Name = "Sylvia", HitPoints = 90, Strength = 12, Defense = 15, Intelligence = 16, Class = RpgClass.Archer }
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>
            {
                Data = characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> GetOneById(int id)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            var findCharacter = characters.FirstOrDefault(c => c.Id == id);

            if (findCharacter is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
                return serviceResponse;
            }

            serviceResponse.Data = _mapper.Map<CharacterResponseDto>(findCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> Save(CharacterRequestDto character)
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            var newCharacter = _mapper.Map<Character>(character);
            newCharacter.Id = characters.Max(c => c.Id) + 1;

            characters.Add(newCharacter);
            serviceResponse.Data = characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> Update(CharacterRequestDto character)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();

            var findCharacter = characters.FirstOrDefault(c => c.Id == character.Id);

            if (findCharacter is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
                return serviceResponse;
            }

            findCharacter.Name = character.Name;
            findCharacter.HitPoints = character.HitPoints;
            findCharacter.Strength = character.Strength;
            findCharacter.Defense = character.Defense;
            findCharacter.Class = character.Class;

            serviceResponse.Data = _mapper.Map<CharacterResponseDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();

            var findCharacter = characters.First(c => c.Id == id);

            if (findCharacter is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
                return serviceResponse;
            }

            characters.Remove(findCharacter);
            serviceResponse.Data = characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            
            return serviceResponse;
        }
    }
}