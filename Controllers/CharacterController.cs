using System.Security.Claims;
using dotnetRpgApi.Dtos.Character;
using dotnetRpgApi.Services;
using dotnetRpgApi.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetRpgApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CharacterResponseDto>>>> GetAll()
        {
            return Ok(await _characterService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> GetOneById(int id)
        {
            return Ok(await _characterService.GetOneById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CharacterResponseDto>>>> Save(CharacterRequestDto character)
        {
            return Ok(await _characterService.Save(character));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> Update(CharacterRequestDto character)
        {
            var response = await _characterService.Update(character);

            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> Delete(int id)
        {
            var response = await _characterService.Delete(id);

            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> AddCharacterSkill(AddCharacterSkillDto skill)
        {
            var response = await _characterService.AddCharacterSkill(skill);

            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}