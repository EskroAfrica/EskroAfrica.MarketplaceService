using EskroAfrica.MarketplaceService.Common;
using EskroAfrica.MarketplaceService.Common.Enums;
using EskroAfrica.MarketplaceService.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EskroAfrica.MarketplaceService.Infrastructure.Data
{
    public class Seeder
    {
        public static async Task SeedAsync(MarketplaceServiceDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            var userIds = new List<Guid> { Guid.Parse("f44aaef5-3ccc-488b-aa93-5d30488e1121"), Guid.Parse("500651bc-824e-438f-aa02-8fd3acfa554a") };
            List<Category> categories = context.Categories.ToList();
            List<SubCategory> subCategories = context.SubCategories.ToList();

            if (!categories.Any())
            {
                categories = CreateCategories();
                await context.Categories.AddRangeAsync(categories);
            }

            if (!subCategories.Any())
            {
                subCategories = CreateSubCategories(categories);
                await context.SubCategories.AddRangeAsync(subCategories);
            }

            if(!context.Products.Any())
            {
                var products = new List<Product>();

                for (int i = 0; i < 50; i++)
                {
                    var categoryId = categories[new Random().Next(0, categories.Count)].Id;
                    var availableSubCategories = subCategories.Where(s => s.CategoryId == categoryId).ToList();
                    Guid? subCategoryId = !availableSubCategories.Any() ? null : availableSubCategories[new Random().Next(0, availableSubCategories.Count)].Id;

                    var product = new Product
                    {
                        Name = MarketplaceServiceHelper.GenerateString(30),
                        Description = MarketplaceServiceHelper.GenerateString(100),
                        Price = new Random().Next(10000, 1500000),
                        SellerId = userIds[new Random().Next(0, userIds.Count)],
                        Condition = (ProductCondition)new Random().Next(0, 2),
                        State = MarketplaceServiceHelper.GenerateString(15),
                        City = MarketplaceServiceHelper.GenerateString(15),
                        Address = MarketplaceServiceHelper.GenerateString(30),
                        Status = (ProductStatus)new Random().Next(0, 4),
                        CategoryId = categoryId,
                        SubCategoryId = subCategoryId,
                        FeaturedImage = MarketplaceServiceHelper.GenerateString(30),
                        Images = JsonConvert.SerializeObject(new List<string> { MarketplaceServiceHelper.GenerateString(30), MarketplaceServiceHelper.GenerateString(30) }),
                    };

                    products.Add(product);
                }

                context.AddRange(products);
            }

            await context.SaveChangesAsync();
        }

        private static List<Category> CreateCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Name = "Electronics"
                },
                new Category
                {
                    Name = "Furniture"
                },
                new Category
                {
                    Name = "Phones"
                },
                new Category
                {
                    Name = "Fashion"
                },
                new Category
                {
                    Name = "Others"
                }
            };
        }

        private static List<SubCategory> CreateSubCategories(List<Category> categories)
        {
            return new List<SubCategory>
            {
                new SubCategory
                {
                    Name = "Air Conditioners",
                    CategoryId = categories[0].Id
                },
                new SubCategory
                {
                    Name = "TVs",
                    CategoryId = categories[0].Id
                },
                new SubCategory
                {
                    Name = "Tables",
                    CategoryId = categories[1].Id
                },
                new SubCategory
                {
                    Name = "Chairs",
                    CategoryId = categories[1].Id
                },
                new SubCategory
                {
                    Name = "Samsung",
                    CategoryId = categories[2].Id
                },
                new SubCategory
                {
                    Name = "Apple",
                    CategoryId = categories[2].Id
                },
                new SubCategory
                {
                    Name = "Shoes",
                    CategoryId = categories[3].Id
                }
            };
        }
    }
}
