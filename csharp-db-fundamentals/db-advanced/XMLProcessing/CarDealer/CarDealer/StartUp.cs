using AutoMapper;
using CarDealer.Data;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(x => x.AddProfile<CarDealerProfile>());

            var suppliersXml = File.ReadAllText("suppliers.xml");
            var partsXml = File.ReadAllText("parts.xml");
            var carsXml = File.ReadAllText("cars.xml");
            var customersXml = File.ReadAllText("customers.xml");
            var salesXml = File.ReadAllText("sales.xml");

            using (CarDealerContext db = new CarDealerContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();
                //ImportSuppliers(db, suppliersXml);
                //ImportParts(db, partsXml);
                //ImportCars(db, carsXml);
                //ImportCustomers(db, customersXml);
                //ImportSales(db, salesXml);

                var result = GetSalesWithAppliedDiscount(db);

                Console.WriteLine(result);
            }

        }

        /// <summary>
        /// Query 9. Import Suppliers
        /// </summary>
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var att = new XmlRootAttribute("Suppliers");
            var serializer = new XmlSerializer(typeof(ImportSupplierDTO[]), att);

            var suppliersDTO = (ImportSupplierDTO[])serializer.Deserialize(new StringReader(inputXml));

            var suppliers = new List<Supplier>();
            foreach (var supplierDTO in suppliersDTO)
            {
                var supplier = Mapper.Map<Supplier>(supplierDTO);
                suppliers.Add(supplier);
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            var result = $"Successfully imported {suppliers.Count}";
            return result;
        }

        /// <summary>
        /// Query 10. Import Parts
        /// </summary>
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var att = new XmlRootAttribute("Parts");
            var serializer = new XmlSerializer(typeof(ImportPartDTO[]), att);

            var partsDTO = (ImportPartDTO[])serializer.Deserialize(new StringReader(inputXml));

            var parts = new List<Part>();
            var suppliers = context.Suppliers.ToHashSet();
            foreach (var partDTO in partsDTO)
            {
                if (suppliers.Any(x => x.Id == partDTO.SupplierId))
                {
                    var part = Mapper.Map<Part>(partDTO);
                    parts.Add(part);
                }
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();

            var result = $"Successfully imported {parts.Count}";
            return result;
        }

        /// <summary>
        /// Query 11. Import Cars
        /// </summary>
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var att = new XmlRootAttribute("Cars");
            var serializer = new XmlSerializer(typeof(ImportCarDTO[]), att);

            var carsDTO = (ImportCarDTO[])serializer.Deserialize(new StringReader(inputXml));

            var cars = new List<Car>();
            var partCars = new List<PartCar>();
            ;
            var parts = context.Parts.ToHashSet();

            foreach (var carDTO in carsDTO)
            {
                var car = Mapper.Map<Car>(carDTO);
                cars.Add(car);

                foreach (var partId in carDTO.PartIds.Select(x => x.Id).Distinct())
                {
                    if (parts.Any(x => x.Id == partId))
                    {
                        var partCar = new PartCar()
                        {
                            Car = car,
                            PartId = partId
                        };
                        partCars.Add(partCar);
                    }
                }
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partCars);
            context.SaveChanges();

            var result = $"Successfully imported {cars.Count}";
            return result;
        }

        /// <summary>
        /// Query 12. Import Customers
        /// </summary>
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var att = new XmlRootAttribute("Customers");
            var serializer = new XmlSerializer(typeof(ImportCustomerDTO[]), att);

            var customersDTO = (ImportCustomerDTO[])serializer.Deserialize(new StringReader(inputXml));

            var customers = new List<Customer>();
            foreach (var customerDTO in customersDTO)
            {
                var customer = Mapper.Map<Customer>(customerDTO);
                customers.Add(customer);
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();

            var result = $"Successfully imported {customers.Count}";
            return result;
        }


        /// <summary>
        /// Query 13. Import Sales
        /// </summary>
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var att = new XmlRootAttribute("Sales");
            var serializer = new XmlSerializer(typeof(ImportSaleDTO[]), att);

            var salesDTO = (ImportSaleDTO[])serializer.Deserialize(new StringReader(inputXml));

            var sales = new List<Sale>();
            var cars = context.Cars.ToHashSet();
            foreach (var saleDTO in salesDTO)
            {
                if (cars.Any(x => x.Id == saleDTO.CarId))
                {
                    var sale = Mapper.Map<Sale>(saleDTO);
                    sales.Add(sale);
                }
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();

            var result = $"Successfully imported {sales.Count}";
            return result;
        }

        /// <summary>
        /// Query 14. Cars With Distance
        /// </summary>
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var att = new XmlRootAttribute("cars");
            var serializer = new XmlSerializer(typeof(CarsWithDistanceDTO[]), att);

            var carsWithDistance = context
                .Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .Select(c => new CarsWithDistanceDTO
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToArray();

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), carsWithDistance, namespaces);
            return sb.ToString();
        }

        /// <summary>
        /// Query 15. Cars from make BMW
        /// </summary>
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var att = new XmlRootAttribute("cars");
            var serializer = new XmlSerializer(typeof(BmwDTO), att);

            var bmwCars = new BmwDTO
            {
                Car = context
                .Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new CarSpecDTO
                {
                    Id = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList()
            };

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), bmwCars, namespaces);
            return sb.ToString();
        }

        /// <summary>
        /// Query 16. Local Suppliers
        /// </summary>
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var att = new XmlRootAttribute("suppliers");
            var serializer = new XmlSerializer(typeof(LocalSupplierDTO), att);

            var localSuppliers = new LocalSupplierDTO
            {
                Suppliers = context
                .Suppliers
                .Where(s => s.IsImporter == false)
                .Select(c => new SupplierDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    PartsCount = c.Parts.Count()
                })
                .ToList()
            };

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), localSuppliers, namespaces);
            return sb.ToString();
        }

        /// <summary>
        /// Query 17. Cars with Their List of Parts
        /// </summary>
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var att = new XmlRootAttribute("cars");
            var serializer = new XmlSerializer(typeof(CarWithPartsDTO[]), att);

            var carParts = context.Cars
                 .Select(c => new CarWithPartsDTO
                 {
                     Make = c.Make,
                     Model = c.Model,
                     TravelledDistance = c.TravelledDistance,
                     Parts = c.PartCars.Select(pc => new CarPartDTO
                     {
                         Name = pc.Part.Name,
                         Price = pc.Part.Price
                     })
                     .OrderByDescending(pc => pc.Price)
                     .ToList()
                 })
                 .OrderByDescending(c => c.TravelledDistance)
                 .ThenBy(c => c.Model)
                 .Take(5)
                 .ToArray();

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), carParts, namespaces);
            return sb.ToString();
        }

        /// <summary>
        /// Query 18. Total Sales by Customer
        /// </summary>
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var att = new XmlRootAttribute("customers");
            var serializer = new XmlSerializer(typeof(CustomersTotalSalesDTO), att);

            var customersTotalSales = new CustomersTotalSalesDTO
            {
                Customers = context.Customers
                    .Select(c => new CustomerDTO
                    {
                        FullName = c.Name,
                        BoughtCars = c.Sales.Count(),
                        SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
                    })
                    .Where(c => c.BoughtCars > 0)
                    .OrderByDescending(c => c.SpentMoney)
                    .ToList()
            };


            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), customersTotalSales, namespaces);
            return sb.ToString();
        }

        /// <summary>
        /// Query 19. Sales with Applied Discount
        /// </summary>
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var att = new XmlRootAttribute("sales");
            var serializer = new XmlSerializer(typeof(SaleWithDiscountDTO[]), att);

            var sales = context.Sales
                .Select(s => new SaleWithDiscountDTO
                {
                    Car = new CarDTO
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Discount = $"{s.Discount:f0}",
                    CustomerName = s.Customer.Name,
                    Price = $"{s.Car.PartCars.Sum(pc => pc.Part.Price):f2}",
                    PriceWithDiscount =
                    $"{s.Car.PartCars.Sum(pc => pc.Part.Price) * (1 - (s.Discount / 100m)):0.####}"

                })
                .ToArray();


            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), sales, namespaces);
            return sb.ToString();
        }
    }
}