using dotnetRpgApi.Models;
using dotnetRpgApi.Services;

namespace dotnetRpgApi.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string username);
    }
}