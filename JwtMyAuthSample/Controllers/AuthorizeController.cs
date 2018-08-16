using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtMyAuthSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtMyAuthSample.Controllers
{
    [Produces("application/json")]
    [Route("api/Authorize")]
    public class AuthorizeController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AuthorizeController(IOptions<JwtSettings> jwtSettingsAccesser)
        {
            _jwtSettings = jwtSettingsAccesser.Value;
        }

        [HttpPost]
        [Route("token")]
        public IActionResult Token([FromBody]LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!(viewModel.User == "jesse" && viewModel.Password == "123qwe"))
                {
                    return BadRequest();
                }

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,"jesse"),
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim("SuperAdminOnly", "true")
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _jwtSettings.Issuer,
                    _jwtSettings.Audience,
                    claims,
                    DateTime.Now, DateTime.Now.AddMinutes(30),
                    creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return BadRequest();
        }
    }
}