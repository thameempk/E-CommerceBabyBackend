using AutoMapper;
using BabyBackend.DbContexts;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BabyBackend.Services.UserService
{
    public class UserService : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly BabyDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UserService(BabyDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public void  RegisterUser(UserRegisterDto userRegister)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(salt, userRegister.Password);
            userRegister.Password = hashPassword;

            var user = _mapper.Map<Users>(userRegister);
            _dbContext.Users.Add(user);
        }
        public List<Users> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public Users Login(LoginDto login)
        {

            var existinguser = _dbContext.Users.FirstOrDefault(u => u.Email ==  login.Email && u.Password == login.Password);
            return existinguser;

        }

        

    }
}
