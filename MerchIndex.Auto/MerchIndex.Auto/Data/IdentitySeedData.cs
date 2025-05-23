﻿using Microsoft.AspNetCore.Identity;
using MerchIndex.Auto.Client.Utilities;
using System.Security.Principal;

namespace MerchIndex.Auto.Data
{
    public class IdentitySeedData
    {
        public static void EnsurePopulated(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            //serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            //UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //one admin
            CreateAdminAccountAsync(serviceProvider, configuration).Wait();

            //10 company accounts
            for (int i = 1; i <= 10; i++)
            {
                CreateCompanyAccountAsync(serviceProvider, configuration, $"{i.ToString()}@{i.ToString()}.{i.ToString()}").Wait();
            }
        }
        public static async Task CreateAdminAccountAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (await roleManager.FindByNameAsync(Constants.Role_Admins) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.Role_Admins));
                await roleManager.CreateAsync(new IdentityRole(Constants.Role_CompanyAdmins));
                await roleManager.CreateAsync(new IdentityRole(Constants.Role_Users));
            }

            string username = configuration["Data:AdminUser:Name"] ?? "admin";
            string email = configuration["Data:AdminUser:Email"] ?? "admin@example.com";
            string password = configuration["Data:AdminUser:Password"] ?? "1";
            //string role = configuration["Data:AdminUser:Role"] ?? Constants.Role_Admins;
            if (await userManager.FindByNameAsync(username) == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = username,
                    Email = email
                };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Constants.Role_Admins);
                }
            }
        }
        public static async Task CreateCompanyAccountAsync(IServiceProvider serviceProvider, IConfiguration configuration, string email)
        {
            serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //var email = "kourosh23@hotmail.com";
            var password = "1";

            if (await userManager.FindByNameAsync(email) == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = email,
                    Email = email
                };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Constants.Role_CompanyAdmins);
                }
            }
        }
    }
}