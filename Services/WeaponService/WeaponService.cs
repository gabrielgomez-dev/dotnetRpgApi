using System.Security.Claims;
using AutoMapper;
using dotnetRpgApi.Data;
using dotnetRpgApi.Dtos.Character;
using dotnetRpgApi.Dtos.Weapon;
using dotnetRpgApi.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetRpgApi.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;

        }

        public async Task<ServiceResponse<CharacterResponseDto>> Save(WeaponAddDto weapon)
        {
            var response = new ServiceResponse<CharacterResponseDto>();

            var findCharacter = await _context.Characters.FirstOrDefaultAsync(
                c => c.Id == int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            );

            if (findCharacter is null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            var newWeapon = new Weapon
            {
                Name = weapon.Name,
                Damage = weapon.Damage,
                Character = findCharacter
            };

            _context.Weapons.Add(newWeapon);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<CharacterResponseDto>(findCharacter);

            return response;
        }
    }
}