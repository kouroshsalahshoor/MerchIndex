using Microsoft.EntityFrameworkCore;
using MerchIndex.Auto.Client.Models;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace MerchIndex.Auto.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(ApplicationDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            var email = "kourosh23@hotmail.com";

            if (!context.Companies.Any())
            {
                var admin = context.Users.FirstOrDefault(x => x.Email == email);

                context.Companies.AddRange(
                new Company
                {
                    Name = "1",
                    ActiveOn = DateTime.Now,
                    Address = "1",
                    BusinessArea = "1",
                    City = "1",
                    District = "1",
                    Email = "kourosh23@hotmail.com",
                    Tel = "1",
                    IsActive = true,
                    HasBeenActivated = true,
                    AdminId = admin!.Id
                });
            }

            if (!context.Products.Any())
            {
                var company = context.Companies.FirstOrDefault(x => x.Email == email);

                Random rnd = new Random();
                int nbr = rnd.Next(1, 11);  // creates a number between 1 and 10
                double randomDouble = rnd.NextDouble() * (100 - 1) + 1; // Generates a random double between 1.0 and 100.0

                //for (int i = 1; i <= 10000; i++)
                for (int i = 1; i <= 1000; i++)
                {
                    nbr = rnd.Next(1, 11);
                    randomDouble = rnd.NextDouble() * (100 - 1) + 1;

                    context.Products.Add(
                    new Product
                    {
                        Name = "Product " + i.ToString(),
                        Description = "Description " + i.ToString(),
                        Category = "Category " + nbr,
                        Price = (decimal)randomDouble,
                        CompanyId = 1,
                        Company = company!,
                        Tag = "Tag " + nbr,
                        ImageUrl = $"images/demo/{nbr}.jpg"
                    });
                }
                context.SaveChanges();
            }
        }
    }
}