using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Web.Data.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Middlewares
{
    public class SeedRolesAndAdminMiddleware
    {
        private readonly RequestDelegate _next;

        public SeedRolesAndAdminMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<Player> userManager,
                                      RoleManager<IdentityRole> roleManager, RiderDBContext db)
        {
            SeedRoles(roleManager).GetAwaiter().GetResult();

            SeedUserInRoles(userManager).GetAwaiter().GetResult();

            await _next(context);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Role.Admin.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Role.Admin.ToString()));
            }

            if (!await roleManager.RoleExistsAsync(Role.Moderator.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Role.Moderator.ToString()));
            }

            if (!await roleManager.RoleExistsAsync(Role.Premium.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Role.Premium.ToString()));
            }

            if (!await roleManager.RoleExistsAsync(Role.User.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Role.User.ToString()));
            }
        }

        private static async Task SeedUserInRoles(UserManager<Player> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new Player
                {
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@rider.com",
                    CreatedOn = DateTime.UtcNow,
                    Balance = 1000000,
                };

                var password = "RiderSuperAdminPassworD";

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role.Admin.ToString());
                }
            }

            if (!userManager.Users.Any(x => x.UserName == "OfficialStore"))
            {
                var user = new Player
                {
                    UserName = "OfficialStore",
                    Email = "OfficialStore@rider.com",
                    CreatedOn = DateTime.UtcNow,
                    Balance = 1000000,
                };

                var password = "RiderOfficialStorePassworD";

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role.Moderator.ToString());
                }
            }
        }
    }
}
