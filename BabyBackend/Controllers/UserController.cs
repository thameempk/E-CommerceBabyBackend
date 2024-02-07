using BabyBackend.Models;
using BabyBackend.Models.Dto;
using BabyBackend.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BabyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserServices _userServices;

        public UserController(IConfiguration configuration, IUserServices userServices)
        {
            _configuration = configuration;
            _userServices = userServices;
        }

        [HttpGet]

        public async Task<ActionResult> GetUsers()
        {
            return Ok(await _userServices.GetUsers());
        }


        [HttpGet("{id}")]

        public async Task<ActionResult> GetUserById(int id)
        {
            return Ok(await _userServices.GetUserById(id));
        }

        [HttpPost("register")]

        public async Task<ActionResult> RegisterUser([FromBody] UserRegisterDto userRegister)
        {
            try
            {
                var isExist = await _userServices.RegisterUser(userRegister);
                if (!isExist)
                {
                    return BadRequest("user already exist");
                }
                return Ok("succss");
            }catch (Exception ex)
            {
                return StatusCode(500, $"an error occured, {ex.Message}");
            }
            
        }

        [HttpPost("login")]

        public async Task<ActionResult> Login([FromBody] LoginDto login)
        {
            
            var existingUser = await _userServices.Login(login);

            if(existingUser == null)
            {
                return NotFound("user name or password incorrect");
            }
            bool validatePassword = BCrypt.Net.BCrypt.Verify(login.Password, existingUser.Password);
            if(!validatePassword)
            {
                return BadRequest("password doesn't match");
            }
            string token = GenerateToken(existingUser);

            return Ok(new { Token = token });

        }


        private string GenerateToken(Users users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
            new Claim(ClaimTypes.Name, users.Name),
            new Claim(ClaimTypes.Role, users.Role),
            new Claim(ClaimTypes.Email, users.Email),
        };

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1)

            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


    }
}
