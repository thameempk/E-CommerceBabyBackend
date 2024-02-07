using AutoMapper;
using BabyBackend.DbContexts;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks; 

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

        public async Task<List<UserViewDto>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            var userMap = _mapper.Map<List<UserViewDto>>(users);
            return userMap;
        }

        public async Task<UserViewDto> GetUserById(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            var mapUser = _mapper.Map<UserViewDto>(user);
            return mapUser;
        }

        public async Task<bool> RegisterUser(UserRegisterDto userRegister)
        {
            var isUserExist = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userRegister.Email);
            if (isUserExist != null)
            {
                return false;
            }

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(userRegister.Password, salt);
            userRegister.Password = hashPassword;

            var user = _mapper.Map<Users>(userRegister);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Users> Login(LoginDto login)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            return existingUser;
        }
    }
}
