using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OlxAPI.Data.Entities;
using OlxAPI.Models;
using OlxAPI.Services.Services.Abstractions;

namespace OlxAPI.Helpers
{
    public class UserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly IAdsService _adsService;

        public UserHelper(UserManager<User> userManager, IAdsService adsService)
        {
            _userManager = userManager;
            _adsService = adsService;
        }
        public async Task<bool> IsOwner(string userId, int adId)
        {
            var currentAd = await _adsService.GetAsync(adId);
            return currentAd?.UserId == userId;
        }
        public async Task<bool> IsAdmin(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var adminRole = RolesEnum.Admin.GetEnumDescription();
            return roles.Contains(adminRole);
        }
    }
}
