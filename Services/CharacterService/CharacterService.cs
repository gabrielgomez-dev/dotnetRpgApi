using AutoMapper;
using dotnetRpgApi.Data;
using dotnetRpgApi.Dtos.Character;
using dotnetRpgApi.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetRpgApi.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            var characters = await _context.Characters.ToListAsync();
            serviceResponse.Data = characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> GetOneById(int id)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            var findCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

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

            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> Update(CharacterRequestDto character)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();

            var findCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == character.Id);

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

            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<CharacterResponseDto>(character);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();

            var findCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

            if (findCharacter is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
                return serviceResponse;
            }

            _context.Characters.Remove(findCharacter);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToListAsync();
            
            return serviceResponse;
        }
    }
}