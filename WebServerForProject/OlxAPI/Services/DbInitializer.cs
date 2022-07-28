using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OlxAPI.Data.Entities;
using OlxAPI.Models;
using OlxAPI.Services.Abstractions;

namespace OlxAPI.Services
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            await CreateRoles();
            await CreateAdmin();
        }

        private async Task CreateAdmin()
        {
            var roleName = RolesEnum.Admin.GetEnumDescription();
            var roleFromDb = await _roleManager.FindByNameAsync(roleName);

            if (roleFromDb != null)
            {
                var admin = new User()
                {
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin228@gmail.com",
                    NormalizedEmail = "ADMIN@ADMIN.com",
                    EmailConfirmed = true
                };

                var password = "Admin228@gmail.com";

                var existingAdmin = await _userManager.FindByNameAsync(admin.UserName);
                if (existingAdmin == null)
                {
                    await _userManager.CreateAsync(admin, password);
                    await _userManager.AddToRoleAsync(admin, roleName);
                }
            }
        }

        private async Task CreateRoles()
        {
            string[] rolesArray = { RolesEnum.Admin.GetEnumDescription(), RolesEnum.User.GetEnumDescription() };

            foreach (var role in rolesArray)
            {
                var findRole = await _roleManager.FindByNameAsync(role);
                if (findRole == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

    }
}

