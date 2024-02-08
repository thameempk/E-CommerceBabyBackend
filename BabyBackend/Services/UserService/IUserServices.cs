using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.UserService
{
    public interface IUserServices
    {
        Task<bool> RegisterUser(UserRegisterDto userRegister);
        Task<List<UserViewDto>> GetUsers();
        Task<UserViewDto> GetUserById(int id);
        Task<Users> Login(LoginDto login);
        Task<bool> BlockUser(int userId);
        Task<bool> UnblockUser(int userId);


    }
}
