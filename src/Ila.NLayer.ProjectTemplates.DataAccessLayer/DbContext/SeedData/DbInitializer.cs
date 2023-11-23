using System;
using System.Collections.Generic;
using System.Linq;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext
{
    public static class DbInitializer
    {
        public static void Initialize(IlaDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Check if there are already categories and products in the database
            if (context.Categories.Any() || context.Products.Any())
            {
                return; // No need to seed if there is already data in the database
            }

            // Seed data
            var electronicsCategory = new Category { Name = "Electronics" };
            var clothingCategory = new Category { Name = "Clothing" };

            context.Categories.AddRange(electronicsCategory, clothingCategory);
            context.SaveChanges();

            var products = new List<Product>
        {
            new Product
            {
                Name = "Laptop",
                Price = 1200.00m,
                StockQuantity = 50,
                Description = "High-performance laptop",
                Category = electronicsCategory
            },
            new Product
            {
                Name = "Smartphone",
                Price = 699.99m,
                StockQuantity = 100,
                Description = "Latest smartphone model",
                Category = electronicsCategory
            },
            new Product
            {
                Name = "T-Shirt",
                Price = 19.99m,
                StockQuantity = 200,
                Description = "Comfortable cotton T-shirt",
                Category = clothingCategory
            },
            new Product
            {
                Name = "Jeans",
                Price = 49.99m,
                StockQuantity = 150,
                Description = "Slim-fit jeans",
                Category = clothingCategory
            }
            // You can add other products here.
        };

            context.Products.AddRange(products);
            context.SaveChanges();


            // Check if there are already users and roles in the database
            if (context.Users.Any() || context.Roles.Any())
            {
                return; // No need to seed if there is already data in the database
            }

            // Create roles
            var adminRole = new IdentityRole { Name = "Admin" };
            var testRole = new IdentityRole { Name = "Test" };

            roleManager.CreateAsync(adminRole).Wait();
            roleManager.CreateAsync(testRole).Wait();

            // Create admin user
            var adminUser = new User {  Fullname = "Admin", UserName = "admin", Email = "admin@example.com" };
            var adminPassword = "123"; // Change the password securely
            userManager.CreateAsync(adminUser, adminPassword).Wait();

            // Assign "Admin" role to admin user
            userManager.AddToRoleAsync(adminUser, "Admin").Wait();

            // Create test user
            var testUser = new User { Fullname = "Test", UserName = "test", Email = "test@example.com" };
            var testPassword = "123"; // Change the password securely
            userManager.CreateAsync(testUser, testPassword).Wait();

            // Assign "Test" role to test user
            userManager.AddToRoleAsync(testUser, "Test").Wait();
        }
    }
}