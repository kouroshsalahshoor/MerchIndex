using Microsoft.EntityFrameworkCore;
using MerchIndex.Auto.Client.Models;
using static System.Net.Mime.MediaTypeNames;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MerchIndex.Auto.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(ApplicationDbContext context)
        {
            Random rnd = new Random();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Companies.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    var email = $"{i.ToString()}@{i.ToString()}.{i.ToString()}";
                    var companyUser = context.Users.FirstOrDefault(x => x.Email == email);

                    context.Companies.AddRange(
                    new Company
                    {
                        Name = $"Company {i.ToString()}",
                        ActiveOn = DateTime.Now,
                        Address = $"Address Company {i.ToString()}",
                        BusinessArea = $"Business Area {rnd.Next(1, 3 + 1)}",
                        City = $"City {rnd.Next(1, 3 + 1)}",
                        District = $"District {rnd.Next(1, 3 + 1)}",
                        Email = email,
                        Tel = $"Tel Company {i.ToString()}",
                        IsActive = true,
                        HasBeenActivated = true,
                        AdminId = companyUser!.Id
                    });
                }
            }

            if (!context.Products.Any())
            {
                //double randomDouble = rnd.NextDouble() * (100 - 1) + 1; // Generates a random double between 1.0 and 100.0

                for (int c = 1; c <= 10; c++)
                {
                    var company = context.Companies.FirstOrDefault(x => x.Email == $"{c.ToString()}@{c.ToString()}.{c.ToString()}");

                    for (int i = 1; i <= 1000; i++)
                    {
                        context.Products.Add(
                        new Product
                        {
                            Name = $"Company {c.ToString()} Product {i.ToString()}",
                            Description = $"Description for Product {i.ToString()}",
                            Category = $"Category {rnd.Next(1, 10 + 1).ToString()}",
                            Manifacturer = $"Manifacturer {rnd.Next(1, 3 + 1).ToString()}",
                            Price = (decimal)((rnd.NextDouble() * (10000 - 1)) + 1),
                            CompanyId = c,
                            Company = company!,
                            Tag = $"Tag {rnd.Next(1, 10 + 1).ToString()}",
                            ImageUrl = $"images/demo/{rnd.Next(1, 12 + 1).ToString()}.jpg"
                        });
                    }
                }

                context.SaveChanges();
            }
        }
    }
}