using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.UserService
{
    public interface IUserServices
    {
        void RegisterUser(UserRegisterDto userRegister);
        List<Users> GetUsers();
        Users Login(LoginDto login);


    }
}
