using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OlxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityControllerBase<T> : ControllerBase where T:IdentityUser
    {
        protected readonly UserManager<T> _userManager;
        public IdentityControllerBase(UserManager<T> userManager)
        {
            _userManager = userManager;
        }
        protected async Task<T> GetIdentityUserAsync()
        {
            return await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
