using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
         private readonly ProductsContext _context;
        public LoginController(IConfiguration config, ProductsContext context) 
        {
             _context = context;
        }

        [HttpPost("auth")]

        async public Task<IActionResult> PostLoginAuth(LoginRequest loginRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Name == loginRequest.Name &&  user.Password == loginRequest.Password);

            if (user == null)
            {
                return NotFound();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(Environment.GetEnvironmentVariable("JWT_ISSUER"),
              Environment.GetEnvironmentVariable("JWT_ISSUER"),
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }

        [HttpPost("register")]

        async public Task<IActionResult> PostLoginRegister(User userData)
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Name == userData.Name &&  user.Password == userData.Password);

            if (user != null)
            {
                return BadRequest();
            }

            _context.Users.Add(userData);
            await _context.SaveChangesAsync();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(Environment.GetEnvironmentVariable("JWT_ISSSUER"),
              Environment.GetEnvironmentVariable("JWT_ISSUER"),
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }
    }
}