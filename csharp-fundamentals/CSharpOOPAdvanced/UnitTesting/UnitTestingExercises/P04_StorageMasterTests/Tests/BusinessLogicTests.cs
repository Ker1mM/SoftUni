using NUnit.Framework;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Storage;
using StorageMaster.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StorageMaster.Tests
{
    [TestFixture]
    public class BusinessLogicTests
    {
        private Type storageMaster;

        [SetUp]
        public void TestInit()
        {
            this.storageMaster = this.GetType("StorageMaster");
        }

        [Test]
        public void ValidateAddProductMethod()
        {
            var addProductMethod = storageMaster.GetMethod("AddProduct");

            var instance = Activator.CreateInstance(storageMaster);

            string productType = "SolidStateDrive";
            double price = 230.43;

            var actualResult = addProductMethod.Invoke(instance, new object[] { productType, price });
            var expectedResult = $"Added SolidStateDrive to pool";

            Assert.AreEqual(expectedResult, actualResult);

            var productsPoolField = (IDictionary<string, Stack<Product>>)storageMaster.GetField("productsPool", (BindingFlags)62).GetValue(instance);

            Assert.That(productsPoolField[productType].Count, Is.EqualTo(1));
        }


        [Test]
        public void ValidateRegisterStorageMethod()
        {
            var registerStorageMethod = storageMaster.GetMethod("RegisterStorage");
            var instance = Activator.CreateInstance(storageMaster);

            string storageType = "DistributionCenter";
            string name = "Gosho";

            var actualResult = registerStorageMethod.Invoke(instance, new object[] { storageType, name });

            var expectedResult = $"Registered Gosho";

            Assert.AreEqual(expectedResult, actualResult);

            var storageRegistryField = (IDictionary<string, Storage>)storageMaster
                .GetField("storageRegistry", (BindingFlags)62)
                .GetValue(instance);

            Assert.That(storageRegistryField[name].Name, Is.EqualTo(name));
        }

        [Test]
        public void ValidateSelectVehicleMethod()
        {
            var selectVehicleMethod = storageMaster.GetMethod("SelectVehicle");
            var registerStorageMethod = storageMaster.GetMethod("RegisterStorage");
            var instance = Activator.CreateInstance(storageMaster);

            string storageType = "DistributionCenter";
            string name = "MyStorage";

            registerStorageMethod.Invoke(instance, new object[] { storageType, name });
            var actualResult = selectVehicleMethod.Invoke(instance, new object[] { name, 0 });

            var expectedResult = $"Selected Van";

            Assert.AreEqual(actualResult, expectedResult);

            var currentVehicleField = (Vehicle)storageMaster
                .GetField("currentVehicle", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(instance);

            Assert.AreEqual(currentVehicleField.GetType().Name, "Van");
        }

        [Test]
        public void ValidateLoadVehicleMethod()
        {
            var loadVehicleMethod = storageMaster.GetMethod("LoadVehicle");
            var addProductMethod = storageMaster.GetMethod("AddProduct");
            var selectVehicleMethod = storageMaster.GetMethod("SelectVehicle");
            var registerStorageMethod = storageMaster.GetMethod("RegisterStorage");
            var instance = Activator.CreateInstance(storageMaster);

            string storageType = "DistributionCenter";
            string name = "MyStorage";

            registerStorageMethod.Invoke(instance, new object[] { storageType, name });
            selectVehicleMethod.Invoke(instance, new object[] { name, 0 });

            List<string> products = new List<string> { "Ram", "Gpu", "HardDrive", "SolidStateDrive" };

            try
            {
                loadVehicleMethod.Invoke(instance, new object[] { products });
            }
            catch (TargetInvocationException ex)
            {
                Assert.AreEqual(ex.InnerException.Message, "Ram is out of stock!");
            }

            addProductMethod.Invoke(instance, new object[] { "HardDrive", 1.1 });
            addProductMethod.Invoke(instance, new object[] { "SolidStateDrive", 1.1 });
            addProductMethod.Invoke(instance, new object[] { "Ram", 1.1 });
            addProductMethod.Invoke(instance, new object[] { "Gpu", 1.1 });

            var expectedOutput = $"Loaded 4/4 products into Van";

            Assert.That(loadVehicleMethod.Invoke(instance, new object[] { products }), Is.EqualTo(expectedOutput));

            var currentVehicleField = (Vehicle)storageMaster
                .GetField("currentVehicle", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(instance);

            var vehicle = this.GetType("Vehicle");
            var vehicleCap = (List<Product>)vehicle
                .GetField("trunk", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(currentVehicleField);

            Assert.That(vehicleCap.Count, Is.EqualTo(4));
        }

        [Test]
        public void ValidateSendVehicleToMethod()
        {
            var registerStorageMethod = storageMaster.GetMethod("RegisterStorage");
            var instance = Activator.CreateInstance(storageMaster);

            string firstStorageType = "DistributionCenter";
            string firstName = "Gosho";

            string secondStorageType = "AutomatedWarehouse";
            string secondName = "Pesho";

            registerStorageMethod.Invoke(instance, new object[] { firstStorageType, firstName });
            registerStorageMethod.Invoke(instance, new object[] { secondStorageType, secondName });

            var actualResult = storageMaster.GetMethod("SendVehicleTo").Invoke(instance, new object[] { "Gosho", 0, "Pesho" });

            var expectedResult = $"Sent Van to Pesho (slot 1)";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ValidateUnloadVehicleMethod()
        {
            var unloadVehicleMethod = storageMaster.GetMethod("UnloadVehicle");
            var loadVehicleMethod = storageMaster.GetMethod("LoadVehicle");
            var addProductMethod = storageMaster.GetMethod("AddProduct");
            var selectVehicleMethod = storageMaster.GetMethod("SelectVehicle");
            var registerStorageMethod = storageMaster.GetMethod("RegisterStorage");
            var instance = Activator.CreateInstance(storageMaster);

            string storageType = "Warehouse";
            string name = "MyStorage";

            registerStorageMethod.Invoke(instance, new object[] { storageType, name });
            selectVehicleMethod.Invoke(instance, new object[] { name, 0 });

            addProductMethod.Invoke(instance, new object[] { "HardDrive", 1.1 });
            addProductMethod.Invoke(instance, new object[] { "SolidStateDrive", 1.1 });
            addProductMethod.Invoke(instance, new object[] { "Ram", 1.1 });
            addProductMethod.Invoke(instance, new object[] { "Gpu", 1.1 });

            List<string> products = new List<string> { "Ram", "Gpu", "HardDrive", "SolidStateDrive" };
            loadVehicleMethod.Invoke(instance, new object[] { products });

            var expectedResult = $"Unloaded 4/4 products at {name}";
            Assert.That(unloadVehicleMethod.Invoke(instance, new object[] { name, 0 }), Is.EqualTo(expectedResult));

            var storageRegistry = (Dictionary<string, Storage>)storageMaster
                .GetField("storageRegistry", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(instance);
            var currentVehicle = storageRegistry[name].GetVehicle(0);

            var vehicle = this.GetType("Vehicle");
            var vehicleCap = (List<Product>)vehicle
                .GetField("trunk", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(currentVehicle);

            Assert.That(vehicleCap.Count, Is.EqualTo(0));
        }

        [Test]
        public void ValidateGetStorageStatusMethod()
        {
            var getStorageStatusMethod = storageMaster.GetMethod("GetStorageStatus");
            var unloadVehicleMethod = storageMaster.GetMethod("UnloadVehicle");
            var loadVehicleMethod = storageMaster.GetMethod("LoadVehicle");
            var addProductMethod = storageMaster.GetMethod("AddProduct");
            var selectVehicleMethod = storageMaster.GetMethod("SelectVehicle");
            var registerStorageMethod = storageMaster.GetMethod("RegisterStorage");
            var instance = Activator.CreateInstance(storageMaster);

            string storageType = "DistributionCenter";
            string name = "FirstStorage";

            List<string> products = new List<string> { "Ram", "Gpu" };
            addProductMethod.Invoke(instance, new object[] { "Ram", 1.1 });
            addProductMethod.Invoke(instance, new object[] { "Gpu", 1.1 });
            registerStorageMethod.Invoke(instance, new object[] { storageType, name });
            selectVehicleMethod.Invoke(instance, new object[] { name, 0 });
            loadVehicleMethod.Invoke(instance, new object[] { products });
            unloadVehicleMethod.Invoke(instance, new object[] { name, 0 });

            string expectedResult = $"Stock (0,8/2): [Gpu (1), Ram (1)]" + Environment.NewLine + "Garage: [Van|Van|Van|empty|empty]";

            Assert.That(getStorageStatusMethod.Invoke(instance, new object[] { name }), Is.EqualTo(expectedResult));
        }

        [Test]
        public void ValidateGetSummaryMethod()
        {
            var getSummaryMethod = storageMaster.GetMethod("GetSummary");
            var unloadVehicleMethod = storageMaster.GetMethod("UnloadVehicle");
            var loadVehicleMethod = storageMaster.GetMethod("LoadVehicle");
            var addProductMethod = storageMaster.GetMethod("AddProduct");
            var selectVehicleMethod = storageMaster.GetMethod("SelectVehicle");
            var registerStorageMethod = storageMaster.GetMethod("RegisterStorage");
            var instance = Activator.CreateInstance(storageMaster);

            string storageType = "DistributionCenter";
            string name = "FirstStorage";

            List<string> products = new List<string> { "Ram", "Gpu" };
            addProductMethod.Invoke(instance, new object[] { "Ram", 1.1 });
            addProductMethod.Invoke(instance, new object[] { "Gpu", 1.1 });
            registerStorageMethod.Invoke(instance, new object[] { storageType, name });
            selectVehicleMethod.Invoke(instance, new object[] { name, 0 });
            loadVehicleMethod.Invoke(instance, new object[] { products });
            unloadVehicleMethod.Invoke(instance, new object[] { name, 0 });

            string expectedResult = $"FirstStorage:" + Environment.NewLine + "Storage worth: $2,20";

            Assert.That(getSummaryMethod.Invoke(instance, new object[] { }), Is.EqualTo(expectedResult));
        }

        private Type GetType(string type)
        {
            var targetType = typeof(StartUp)
                .Assembly
                .GetTypes()
                .FirstOrDefault(x => x.Name == type);

            return targetType;
        }
    }
}

