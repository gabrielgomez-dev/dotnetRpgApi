using dotnetRpgApi.Dtos.Character;

namespace dotnetRpgApi.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<CharacterResponseDto>>> GetAll();
        Task<ServiceResponse<CharacterResponseDto>> GetOneById(int id);
        Task<ServiceResponse<List<CharacterResponseDto>>> Save(CharacterRequestDto character);
        Task<ServiceResponse<CharacterResponseDto>> Update(CharacterRequestDto character);
        Task<ServiceResponse<List<CharacterResponseDto>>> Delete(int id);
    }
}