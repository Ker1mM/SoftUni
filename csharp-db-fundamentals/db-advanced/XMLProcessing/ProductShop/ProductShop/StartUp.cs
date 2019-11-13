using AutoMapper;
using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(x => x.AddProfile<ProductShopProfile>());

            var usersXml = File.ReadAllText("users.xml");
            var productsXml = File.ReadAllText("products.xml");
            var categoriesXml = File.ReadAllText("categories.xml");
            var categoriesProductsXml = File.ReadAllText("categories-products.xml");


            using (ProductShopContext db = new ProductShopContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                //ImportUsers(db, usersXml);
                //ImportProducts(db, productsXml);
                //ImportCategories(db, categoriesXml);
                //ImportCategoryProducts(db, categoriesProductsXml);

                var result = GetUsersWithProducts(db);

                System.Console.WriteLine(result);
            }
        }

        /// <summary>
        /// Query 1. Import Users
        /// </summary>
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(typeof(UserDTO[]), attr);

            var deserializedUsers = (UserDTO[])serializer.Deserialize(new StringReader(inputXml));

            var usersToBeAdded = new List<User>();
            foreach (var user in deserializedUsers)
            {
                var userToAdd = Mapper.Map<User>(user);
                usersToBeAdded.Add(userToAdd);
            }

            context.Users.AddRange(usersToBeAdded);
            context.SaveChanges();

            string result = $"Successfully imported {usersToBeAdded.Count}";
            return result;
        }

        /// <summary>
        /// Query 2. Import Products
        /// </summary>
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("Products");
            var serializer = new XmlSerializer(typeof(ProductDTO[]), attr);

            var productDTOs = (ProductDTO[])serializer.Deserialize(new StringReader(inputXml));

            var productsToBeAdded = new List<Product>();
            foreach (var product in productDTOs)
            {
                var productToAdd = Mapper.Map<Product>(product);
                productsToBeAdded.Add(productToAdd);
            }

            context.Products.AddRange(productsToBeAdded);
            context.SaveChanges();

            string result = $"Successfully imported {productsToBeAdded.Count}";
            return result;
        }

        /// <summary>
        /// Query 3. Import Categories
        /// </summary>
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("Categories");
            var serializer = new XmlSerializer(typeof(CategoryDTO[]), attr);

            var categoryDTOs = (CategoryDTO[])serializer.Deserialize(new StringReader(inputXml));

            var categoriesToBeAdded = new List<Category>();
            foreach (var category in categoryDTOs)
            {
                var categoryToAdd = Mapper.Map<Category>(category);
                categoriesToBeAdded.Add(categoryToAdd);
            }

            context.Categories.AddRange(categoriesToBeAdded);
            context.SaveChanges();

            string result = $"Successfully imported {categoriesToBeAdded.Count}";
            return result;
        }

        /// <summary>
        /// Query 4. Import Categories and Products
        /// </summary>
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("CategoryProducts");
            var serializer = new XmlSerializer(typeof(CategoryProductDTO[]), attr);

            var categoryProductDTOs = (CategoryProductDTO[])serializer.Deserialize(new StringReader(inputXml));

            var categoryProductsToBeAdded = new List<CategoryProduct>();
            foreach (var categoryProduct in categoryProductDTOs)
            {
                var categoryProductToAdd = Mapper.Map<CategoryProduct>(categoryProduct);
                categoryProductsToBeAdded.Add(categoryProductToAdd);
            }

            context.CategoryProducts.AddRange(categoryProductsToBeAdded);
            context.SaveChanges();

            string result = $"Successfully imported {categoryProductsToBeAdded.Count}";
            return result;
        }


        /// <summary>
        /// Query 5. Products In Range
        /// </summary>
        public static string GetProductsInRange(ProductShopContext context)
        {
            var attr = new XmlRootAttribute("Products");
            var serializer = new XmlSerializer(typeof(ProductsInRangeDTO[]), attr);

            var products = context
                .Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new ProductsInRangeDTO
                {
                    Name = x.Name,
                    Price = x.Price,
                    Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName
                })
                .OrderBy(x => x.Price)
                .Take(10)
                .ToArray();

            var sb = new StringBuilder();
            var xmlNamespace = new XmlSerializerNamespaces(new[]
            {
                new XmlQualifiedName("", "")
            });

            serializer.Serialize(new StringWriter(sb), products, xmlNamespace);
            return sb.ToString();
        }

        /// <summary>
        /// Query 6. Export Successfully Sold Products
        /// </summary>
        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts = context.Users
                .Where(u => u.ProductsSold.Any())
                .Select(u => new UserSoldProductsDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                                    .Select(p => new SoldProductsDTO
                                    {
                                        Name = p.Name,
                                        Price = p.Price,
                                    })
                                    .ToList()
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ToArray();

            var attr = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(typeof(UserSoldProductsDTO[]), attr);

            var sb = new StringBuilder();
            var xmlNamespace = new XmlSerializerNamespaces(new[]
            {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), soldProducts, xmlNamespace);
            return sb.ToString();
        }

        /// <summary>
        /// Query 7. Export Categories By Products Count
        /// </summary>
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new CategoryByProductDTO
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count(),
                    AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToArray();

            var attr = new XmlRootAttribute("Categories");
            var serializer = new XmlSerializer(typeof(CategoryByProductDTO[]), attr);

            var sb = new StringBuilder();
            var xmlNamespace = new XmlSerializerNamespaces(new[]
            {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), categories, xmlNamespace);
            return sb.ToString();
        }

        /// <summary>
        /// Query 8. Export Users and Products
        /// </summary>
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any());


            var usersInfo = new UserAndProductDTO
            {
                Count = users.Count(),
                Users = users.Select(u => new UserExportDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new UserAndProductSoldProductsDTO
                    {
                        Count = u.ProductsSold.Count(),
                        Products = u.ProductsSold
                       .Select(ps => new SoldProductsDTO
                       {
                           Name = ps.Name,
                           Price = ps.Price
                       })
                       .OrderByDescending(t => t.Price)
                       .ToList()
                    }
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .Take(10)
                .ToList()
            };

            var attr = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(typeof(UserAndProductDTO), attr);

            var sb = new StringBuilder();
            var xmlNamespace = new XmlSerializerNamespaces(new[]
            {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), usersInfo, xmlNamespace);
            return sb.ToString();
        }
    }
}