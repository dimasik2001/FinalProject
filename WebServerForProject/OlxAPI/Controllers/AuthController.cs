using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OlxAPI.Configurations;
using OlxAPI.Data.Entities;
using OlxAPI.Models;
using OlxAPI.Models.Credentials;
using OlxAPI.Models.PostModels;

namespace OlxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : IdentityControllerBase<User>
    {
        private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;
        public AuthController(IOptions<JwtBearerTokenSettings> jwtTokenOptions,
            UserManager<User> userManager)
            :base(userManager)
        {
            _jwtBearerTokenSettings = jwtTokenOptions.Value;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserPostModel userDetails)
        {
            if (!ModelState.IsValid || userDetails == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }

            var identityUser = new User() { UserName = userDetails.Username, Email = userDetails.Email};
            await _userManager.CreateAsync(identityUser, userDetails.Password);
            var roleName = RolesEnum.User.GetEnumDescription();

            var result = await _userManager.AddToRoleAsync(identityUser, roleName);


            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (var error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }


            return Ok(new { Message = "User Reigstration Successful" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<object> Login([FromBody] LoginCredentials credentials)
        {
            User identityUser;

            if (!ModelState.IsValid
                || credentials == null
                || (identityUser = await ValidateUserAsync(credentials)) == null
                ||identityUser.IsBlocked)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }
            var roles = await _userManager.GetRolesAsync(identityUser);
            var token = GenerateToken(identityUser, roles);

            return Ok(
                new
                {
                    AccessToken = token,
                    UserId = identityUser.Id,
                    UserName = identityUser.UserName,
                    Email = identityUser.Email,
                    ImageUrl = identityUser.ImagePath,
                    Roles = roles,
                    isLogin = true
                });
        }
        private string GenerateToken(User identityUser, IList<string> roles)
        {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSettings.SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, identityUser.Email),
                    new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                    new Claim(ClaimTypes.Role, string.Join(',', roles))
                    }),

                    Expires = DateTime.Now.AddSeconds(_jwtBearerTokenSettings.ExpiryTimeInSeconds),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                    Audience = _jwtBearerTokenSettings.Audience,
                    Issuer = _jwtBearerTokenSettings.Issuer
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);

        }

        private async Task<User> ValidateUserAsync(LoginCredentials credentials)
        {
            var identityUser = await _userManager.FindByEmailAsync(credentials.Email);

            if (identityUser != null)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
                return result == PasswordVerificationResult.Failed ? null : identityUser;
            }

            return null;
        }


    }
}
