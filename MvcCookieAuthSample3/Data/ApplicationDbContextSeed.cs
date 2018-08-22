using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MvcCookieAuthSample2.Models;

namespace MvcCookieAuthSample2.Data
{
    public class ApplicationDbContextSeed
    {
        private UserManager<ApplicationUser> _userManager;

        public async Task SeedAsync(ApplicationDbContext context, IServiceProvider services)
        {
            if (!context.Users.Any())
            {
                _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                var defaultUser = new ApplicationUser()
                {
                    UserName = "Administrator",
                    Email = "243310820@qq.com",
                    NormalizedUserName = "admin"
                };
                var result = await _userManager.CreateAsync(defaultUser, "Password$123");
                if (!result.Succeeded)
                {
                    throw new Exception("初始用户创建失败");
                }
            }
        }
    }
}
