using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserInterface _userrepo;
        public UserApiController(IUserInterface userrepo)
        {
            _userrepo = userrepo;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel User)
        {
            IConfiguration myConfig = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

                Register user = _userrepo.ApiLogin(User);
                if(user != null)
                {
                    var claims = new[]{
                        new Claim(JwtRegisteredClaimNames.Sub, myConfig["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Userid", user.c_userid.ToString()),
                        new Claim("UserName", user.c_username),
                        new Claim("Email", user.c_email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(myConfig["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    myConfig["Jwt:Issuer"],
                    myConfig["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: signIn
                );
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }
    }
}