using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<CharacterResponseDto>>> GetAll()
        {
            var response = new ServiceResponse<List<CharacterResponseDto>>();

            var characters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.User!.Id == GetUserId())
                .ToListAsync();

            response.Data = characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> GetOneById(int id)
        {
            var response = new ServiceResponse<CharacterResponseDto>();
            var findCharacter = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());

            if (findCharacter is null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            response.Data = _mapper.Map<CharacterResponseDto>(findCharacter);
            return response;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> Save(CharacterRequestDto character)
        {
            var response = new ServiceResponse<List<CharacterResponseDto>>();
            var newCharacter = _mapper.Map<Character>(character);

            newCharacter.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            response.Data = await _context.Characters
                .Where(c => c.User!.Id == GetUserId())
                .Select(c => _mapper.Map<CharacterResponseDto>(c))
                .ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> Update(CharacterRequestDto character)
        {
            var response = new ServiceResponse<CharacterResponseDto>();

            var findCharacter = await _context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == character.Id);

            if (findCharacter is null || findCharacter.User!.Id != GetUserId())
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            findCharacter.Name = character.Name;
            findCharacter.HitPoints = character.HitPoints;
            findCharacter.Strength = character.Strength;
            findCharacter.Defense = character.Defense;
            findCharacter.Class = character.Class;

            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<CharacterResponseDto>(character);

            return response;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> Delete(int id)
        {
            var response = new ServiceResponse<List<CharacterResponseDto>>();

            var findCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());

            if (findCharacter is null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            _context.Characters.Remove(findCharacter);
            await _context.SaveChangesAsync();

            response.Data = await _context.Characters
                .Where(c => c.User!.Id == GetUserId())
                .Select(c => _mapper.Map<CharacterResponseDto>(c))
                .ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> AddCharacterSkill(AddCharacterSkillDto skill)
        {
            var response = new ServiceResponse<CharacterResponseDto>();

            var findCharacter = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(
                    c => c.Id == skill.CharacterId && c.User!.Id == GetUserId()
                );

            if (findCharacter is null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            var findSkill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == skill.SkillId);

            if (findSkill is null)
            {
                response.Success = false;
                response.Message = "Skill not found";
                return response;
            }

            findCharacter.Skills!.Add(findSkill);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<CharacterResponseDto>(findCharacter);

            return response;
        }
    }
}