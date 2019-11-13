using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CarDealer.Data;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (CarDealerContext db = new CarDealerContext())
            {
                Console.WriteLine(GetLocalSuppliers(db));
            }
        }

        /// <summary>
        /// Query 9. Import Suppliers
        /// </summary>
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            string result = $"Successfully imported {suppliers.Count}.";
            return result;
        }

        /// <summary>
        /// Query 10. Import Parts
        /// </summary>
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<List<Part>>(inputJson)
                .OrderBy(x => x.SupplierId).ToList();

            var suppliers = context.Suppliers.Select(x => x.Id).ToHashSet<int>();

            parts = parts.Where(p => suppliers.Contains(p.SupplierId)).ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            string result = $"Successfully imported {parts.Count}.";
            return result;
        }

        /// <summary>
        /// Query 11. Import Cars
        /// </summary>
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carParts = JsonConvert.DeserializeObject<List<Car>>(inputJson);

            var cars = new List<Car>();
            var partCars = new List<PartCar>();

            foreach (var carPart in carParts)
            {

                var car = new Car
                {
                    Make = carPart.Make,
                    Model = carPart.Model,
                    TravelledDistance = carPart.TravelledDistance
                };

                cars.Add(car);

                foreach (var partId in carPart.partsId.Distinct())
                {
                    partCars.Add(new PartCar
                    {
                        Car = car,
                        PartId = partId
                    });
                }
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partCars);
            context.SaveChanges();

            string result = $"Successfully imported {cars.Count}.";
            return result;
        }

        /// <summary>
        /// Query 12. Import Customers
        /// </summary>
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            string result = $"Successfully imported {customers.Count}.";
            return result;
        }


        /// <summary>
        /// Query 13. Import Sales
        /// </summary>
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            string result = $"Successfully imported {sales.Count}.";
            return result;
        }

        /// <summary>
        /// Query 14. Export Ordered Customers
        /// </summary>
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return json;
        }


        /// <summary>
        /// Query 15. Export Cars from make Toyota
        /// </summary>
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(x => new
                {
                    x.Id,
                    x.Make,
                    x.Model,
                    x.TravelledDistance
                })
                .ToList();

            var json = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return json;
        }

        /// <summary>
        /// Query 16. Export Local Suppliers
        /// </summary>
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var localSuppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count()
                })
                .ToList();

            var json = JsonConvert.SerializeObject(localSuppliers, Formatting.Indented);
            return json;
        }

        /// <summary>
        /// Query 17. Export Cars with Their List of Parts
        /// </summary>
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    },
                    parts = c.PartCars.Select(p => new
                    {
                        Name = p.Part.Name,
                        Price = $"{p.Part.Price:f2}"
                    })
                })
                .ToList();

            var json = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return json;
        }


        /// <summary>
        /// Query 18. Export Total Sales by Customer
        /// </summary>
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count(),
                    spentMoney = (c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price)))
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars)
                .ToList();

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            return json;
        }

        /// <summary>
        /// Query 19. Export Sales with Applied Discount
        /// </summary>
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales.Select(c => new
            {
                car = new
                {
                    c.Car.Make,
                    c.Car.Model,
                    c.Car.TravelledDistance
                },
                customerName = c.Customer.Name,
                Discount = $"{c.Discount:f2}",
                price = $"{c.Car.PartCars.Sum(pc => pc.Part.Price):f2}",
                priceWithDiscount = $"{(c.Car.PartCars.Sum(pc => pc.Part.Price)) * (1 - (c.Discount / 100m)):f2}"
            })
            .Take(10)
            .ToList();

            var json = JsonConvert.SerializeObject(sales, Formatting.Indented);
            return json;
        }
    }

}