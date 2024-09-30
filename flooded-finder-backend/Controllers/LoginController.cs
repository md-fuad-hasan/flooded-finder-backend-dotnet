using flooded_finder_backend.Data;
using flooded_finder_backend.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace flooded_finder_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public LoginController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult LoginUser(LoginDto loginDto)
        {
            var appUser = _context.AppUsers.Where(a => a.Email == loginDto.Email).FirstOrDefault();
            if (appUser == null)
            {
                return BadRequest();
            }
            if (BCrypt.Net.BCrypt.Verify(loginDto.Password, appUser.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                    new Claim(JwtRegisteredClaimNames.GivenName, appUser.UserName),
                    new Claim(ClaimTypes.Role, appUser.Role),
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    expires: DateTime.UtcNow.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])), SecurityAlgorithms.HmacSha256)

                    );

                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) , UserId = appUser.Id, UserType=appUser.Role});

            }

            return Unauthorized();

        }



    }
}
