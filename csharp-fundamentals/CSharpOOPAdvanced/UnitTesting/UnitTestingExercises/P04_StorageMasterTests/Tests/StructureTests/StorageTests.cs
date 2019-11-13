using NUnit.Framework;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Storage;
using StorageMaster.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StorageMaster.Tests.StructureTests
{
    [TestFixture]
    class StorageTests
    {
        private Type storage;

        [SetUp]
        public void TestInit()
        {
            this.storage = GetType("Storage");
        }

        [Test]
        public void ValidateAllStorages()
        {
            var types = new[]
            {
                "AutomatedWarehouse",
                "DistributionCenter",
                "Warehouse"
            };

            foreach (var type in types)
            {
                var currentType = GetType(type);

                Assert.That(currentType, Is.Not.Null, $"{type} doesn't exists");
            }
        }

        [Test]
        public void ValidateStorageConstructor()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var constructor = this.storage.GetConstructors(flags).FirstOrDefault();

            Assert.That(constructor, Is.Not.Null, "Constructor doesn't exists");

            var constructorsParams = constructor.GetParameters();
            var expectedConstructorParamTypes = new Type[] { typeof(string), typeof(int), typeof(int), typeof(IEnumerable<Vehicle>) };

            for (int i = 0; i < 4; i++)
            {
                Assert.That(constructorsParams[i].ParameterType, Is.EqualTo(expectedConstructorParamTypes[i]));
            }
        }

        [Test]
        public void ValidateStorageChildClasses()
        {
            var derivedTypes = new[]
            {
                GetType("AutomatedWarehouse"),
                GetType("DistributionCenter"),
                GetType("Warehouse"),
            };

            foreach (var derivedType in derivedTypes)
            {
                Assert.That(derivedType.BaseType, Is.EqualTo(storage), $"{derivedType} doesn't inherit {storage}!");
            }
        }

        [Test]
        public void ValidateStorageProperties()
        {
            var actualProperties = storage.GetProperties();

            var expectedProperties = new Dictionary<string, Type>
            {
                { "Name", typeof(string) },
                { "Capacity", typeof(int) },
                { "GarageSlots", typeof(int) },
                { "IsFull", typeof(bool) },
                { "Garage", typeof(IReadOnlyCollection<Vehicle>) },
                { "Products", typeof(IReadOnlyCollection<Product>) }
            };

            foreach (var actualProperty in actualProperties)
            {
                var isValidProperty = expectedProperties.Any(x => x.Key == actualProperty.Name && actualProperty.PropertyType == x.Value);

                Assert.That(isValidProperty, $"{actualProperty.Name} doesn't exists!");
            }
        }

        [Test]
        public void ValidateStorageMethods()
        {
            var expectedMethods = new List<Method>
            {
                new Method(typeof(Vehicle), "GetVehicle", typeof(int)), //Vehicle GetVehicle(int garageSlot)
                new Method(typeof(int), "SendVehicleTo", typeof(int), typeof(Storage)), //int SendVehicleTo(int garageSlot, Storage deliveryLocation)
                new Method(typeof(int), "UnloadVehicle", typeof(int)), //int UnloadVehicle(int garageSlot)
                new Method(typeof(void), "InitializeGarage", typeof(IEnumerable<Vehicle>)), //void InitializeGarage(IEnumerable<Vehicle> vehicles)
                new Method(typeof(int), "AddVehicle", typeof(Vehicle)) //int AddVehicle(Vehicle vehicle)
            };

            foreach (var expectedMethod in expectedMethods)
            {
                var currentMethod = storage.GetMethod(expectedMethod.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                Assert.That(currentMethod, Is.Not.Null, $"{expectedMethod.Name} method doesn't exists");

                var currentMethodReturnType = currentMethod.ReturnType == expectedMethod.ReturnType;

                Assert.That(currentMethodReturnType, $"{expectedMethod.Name} invalid return type");

                var expectedMethodParms = expectedMethod.InputParamateres;
                var actualParms = currentMethod.GetParameters();

                for (int i = 0; i < expectedMethodParms.Length; i++)
                {
                    var actualParam = actualParms[i].ParameterType;
                    var expectedParam = expectedMethodParms[i];

                    Assert.AreEqual(expectedParam, actualParam);
                }
            }
        }

        [Test]
        public void ValidateStorageIsAbstract()
        {
            Assert.That(storage.IsAbstract, $"Storage class must be abstract!");
        }

        [Test]
        [TestCase("garage")]
        [TestCase("products")]
        public void ValidateStorageFields(string fieldName)
        {
            var trunkField = storage.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.That(trunkField, Is.Not.Null, $"Invalid field");
        }

        //Method Validation
        [Test]
        public void ValidateGetVehicleMethod()
        {
            var garage = new AutomatedWarehouse("MyStorage");

            Assert.That(() => garage.GetVehicle(2),
                Throws.InvalidOperationException.With.Message.EqualTo("Invalid garage slot!"));

            Assert.That(() => garage.GetVehicle(1),
                Throws.InvalidOperationException.With.Message.EqualTo("No vehicle in this garage slot!"));

            Assert.That(garage.GetVehicle(0).GetType().Name, Is.EqualTo("Truck"));
        }

        [Test]
        public void ValidateSendVehicleToMethod()
        {
            var originStorage = new DistributionCenter("Origin");
            var targetStorage = new AutomatedWarehouse("Target");

            originStorage.SendVehicleTo(0, targetStorage);

            var originGarage = (Vehicle[])this.GetType("Storage")
                .GetField("garage", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(originStorage);

            var targetGarage = (Vehicle[])this.GetType("Storage")
                .GetField("garage", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(targetStorage);

            Assert.That(originGarage[0], Is.EqualTo(null));
            Assert.That(targetGarage[1].GetType().Name, Is.EqualTo("Van"));

            Assert.That(() => originStorage.SendVehicleTo(1, targetStorage),
                Throws.InvalidOperationException.With.Message.EqualTo("No room in garage!"));
        }

        [Test]
        public void ValidateUnloadVehicleMethod()
        {
            var storage = new DistributionCenter("MyStorage");
            var vehicle = new Van();
            var product = new HardDrive(1.1);

            vehicle.LoadProduct(product);
            vehicle.LoadProduct(product);

            var garage = (Vehicle[])this.GetType("Storage")
                .GetField("garage", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(storage);

            garage[1] = vehicle;

            Assert.That(storage.UnloadVehicle(1), Is.EqualTo(2));

            var products = (List<Product>)this.GetType("Storage")
                .GetField("products", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(storage);

            Assert.That(products.Count, Is.EqualTo(2));
        }

        private Type GetType(string type)
        {
            var targetType = typeof(StartUp)
                .Assembly
                .GetTypes()
                .FirstOrDefault(x => x.Name == type);

            return targetType;
        }

        private class Method
        {
            public Method(Type returnType, string name, params Type[] inputParams)
            {
                this.ReturnType = returnType;
                this.Name = name;
                this.InputParamateres = inputParams;
            }

            public Type ReturnType { get; set; }

            public string Name { get; set; }

            public Type[] InputParamateres { get; set; }
        }
    }
}
