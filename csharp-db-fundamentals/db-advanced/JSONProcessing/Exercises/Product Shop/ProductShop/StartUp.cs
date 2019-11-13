using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

        }

        /// <summary>
        /// Query 1. Import Users
        /// </summary>
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(inputJson);
            context.Users.AddRange(users);
            context.SaveChanges();

            string result = $"Successfully imported {users.Count}";
            return result;
        }

        /// <summary>
        /// Query 2. Import Products
        /// </summary>
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(inputJson);
            context.Products.AddRange(products);
            context.SaveChanges();

            string result = $"Successfully imported {products.Count}";
            return result;
        }

        /// <summary>
        /// Query 3. Import Categories
        /// </summary>
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson)
                .Where(c => c.Name != null)
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            string result = $"Successfully imported {categories.Count}";
            return result;
        }

        /// <summary>
        /// Query 4. Import Categories and Products
        /// </summary>
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            string result = $"Successfully imported {categoryProducts.Count}";
            return result;
        }

        /// <summary>
        /// Query 5. Export Products In Range
        /// </summary>
        public static string GetProductsInRange(ProductShopContext context)
        {
            var productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                })
                .OrderBy(p => p.price)
                .ToList();

            var json = JsonConvert.SerializeObject(productsInRange);

            return json;
        }

        /// <summary>
        /// Query 6. Export Successfully Sold Products
        /// </summary>
        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts = context.Users
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                                    .Where(p => p.BuyerId != null)
                                    .Select(p => new
                                    {
                                        name = p.Name,
                                        price = p.Price,
                                        buyerFirstName = p.Buyer.FirstName,
                                        buyerLastName = p.Buyer.LastName
                                    })
                })
                .Where(u => u.soldProducts.Count() > 0)
                .OrderBy(u => u.lastName)
                .ThenBy(u => u.firstName)
                .ToList();

            var json = JsonConvert.SerializeObject(soldProducts);

            return json;
        }

        /// <summary>
        /// Query 7. Export Categories By Products Count
        /// </summary>
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count(),
                    averagePrice = $"{c.CategoryProducts.Average(cp => cp.Product.Price):f2}",
                    totalRevenue = $"{c.CategoryProducts.Sum(cp => cp.Product.Price):f2}"
                })
                .OrderByDescending(c => c.productsCount)
                .ToList();

            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return json;
        }


        /// <summary>
        /// Query 8. Export Users and Products
        /// </summary>
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(ps => ps.BuyerId != null));


            var usersInfo = new
            {
                usersCount = users.Count(),
                users = users.Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold.Count(ps => ps.BuyerId != null),
                        products = u.ProductsSold
                        .Where(ps => ps.BuyerId != null)
                        .Select(ps => new
                        {
                            name = ps.Name,
                            price = ps.Price
                        })
                    }
                })
                .OrderByDescending(u => u.soldProducts.count)
            };

            var json = JsonConvert.SerializeObject(usersInfo, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return json;
        }
    }
}