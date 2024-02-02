using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.UserService
{
    public interface IUserServices
    {
        void RegisterUser(UserRegisterDto userRegister);
        List<UserViewDto> GetUsers();
        UserViewDto GetUserById(int id);
        Users Login(LoginDto login);


    }
}
