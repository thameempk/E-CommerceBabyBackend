﻿using AutoMapper;
using BabyBackend.DbContexts;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using BabyBackend.Services.EmailServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IEmailService _emailService;

        public UserService(BabyDbContext dbContext, IMapper mapper, IConfiguration configuration, IEmailService emailService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
            _emailService = emailService;
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

       

        public async Task<string> RegisterUser(UserRegisterDto userRegister)
        {
            var isUserExist = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userRegister.Email);
            if (isUserExist != null)
            {
                return "user already exist";
            }
            bool verifyOtp =  _emailService.verifyOtp(userRegister.Email, userRegister.Otp);
            if(verifyOtp)
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(userRegister.Password, salt);
                userRegister.Password = hashPassword;

                var user = _mapper.Map<Users>(userRegister);
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                return "register successfully";
            }
            return "wrong otp";
            
        }

        public async Task<Users> Login(LoginDto login)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            return existingUser;
        }

        public async Task<bool> BlockUser(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync( u=>u.Id ==  userId);
            if(user == null)
            {
                return false;
            }
            user.isBlocked = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UnblockUser(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if(user == null )
            {
                return false;
            }
            user.isBlocked = false;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
