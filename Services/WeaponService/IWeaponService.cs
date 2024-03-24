using dotnetRpgApi.Dtos.Character;
using dotnetRpgApi.Dtos.Weapon;

namespace dotnetRpgApi.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<CharacterResponseDto>> Save(WeaponAddDto weapon);
    }
}