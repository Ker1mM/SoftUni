namespace TheTankGame.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    //using TheTankGame.Entities.Miscellaneous;
    //using TheTankGame.Entities.Miscellaneous.Contracts;
    //using TheTankGame.Entities.Parts;
    //using TheTankGame.Entities.Parts.Contracts;
    //using TheTankGame.Entities.Vehicles;

    [TestFixture]
    public class BaseVehicleTests
    {
        private Type baseVehicle;


        [SetUp]
        public void TestInit()
        {
            this.baseVehicle = typeof(BaseVehicle);
        }

        [Test]
        public void ValidateBaseVehicleConstructor()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var constructor = this.baseVehicle.GetConstructors(flags).FirstOrDefault();

            Assert.That(constructor, Is.Not.Null, "Constructor doesn't exists");

            var constructorsParams = constructor.GetParameters();

            var expectedConstructorParams = new Type[]
            {
                typeof(string),
                typeof(double),
                typeof(decimal),
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(IAssembler)
            };

            for (int i = 0; i < 7; i++)
            {
                Assert.That(constructorsParams[i].ParameterType, Is.EqualTo(expectedConstructorParams[i]));
            }
        }

        [Test]
        public void ValidateBaseVehicleIsAbstract()
        {
            Assert.That(baseVehicle.IsAbstract, $"BaseVehicle class must be abstract!");
        }

        [Test]
        public void ValidateBaseVehicleProperties()
        {
            var actualProperties = this.baseVehicle.GetProperties();

            var expectedProperties = new Dictionary<string, Type>
            {
                { "Model", typeof(string) },
                { "Weight", typeof(double) },
                { "Price", typeof(decimal)},
                { "Attack", typeof(int)},
                { "Defense", typeof(int)},
                { "HitPoints", typeof(int)},
                { "TotalWeight", typeof(double)},
                { "TotalPrice", typeof(decimal)},
                { "TotalAttack", typeof(long)},
                { "TotalDefense", typeof(long)},
                { "TotalHitPoints", typeof(long)},
                { "Parts", typeof(IEnumerable<IPart>)}
            };

            foreach (var actualProperty in actualProperties)
            {
                var isValidProperty = expectedProperties.Any(x => x.Key == actualProperty.Name && actualProperty.PropertyType == x.Value);

                Assert.That(isValidProperty, $"{actualProperty.Name} doesn't exists!");
            }
        }

        [Test]
        public void ValidateBaseVehicleFields()
        {
            var expectedFields = new Dictionary<string, Type>
               {
                { "weight",typeof(double)},
                { "price",typeof(decimal) },
                { "attack",typeof(int)},
                { "defense",typeof(int)},
                { "hitPoints",typeof(int)},
                { "model",typeof(string)},
                { "assembler",typeof(IAssembler)},
                { "orderedParts",typeof(IList<string>)}
               };

            foreach (var expectedfield in expectedFields)
            {
                var field = baseVehicle.GetField(expectedfield.Key, BindingFlags.NonPublic | BindingFlags.Instance);
                Assert.That(field, Is.Not.Null, $"Invalid field");
                Assert.That(field.FieldType, Is.EqualTo(expectedfield.Value), $"Invalid field");
            }
        }

        [Test]
        public void ValidateReturnString()
        {
            var vehicle = new Revenger("Model1", 100, 100, 10, 10, 10, new VehicleAssembler());
            StringBuilder result = new StringBuilder();

            result.AppendLine($"Revenger - Model1");
            result.AppendLine($"Total Weight: {100:F3}");
            result.AppendLine($"Total Price: {100:F3}");
            result.AppendLine($"Attack: {10}");
            result.AppendLine($"Defense: {10}");
            result.AppendLine($"HitPoints: {10}");

            result.Append("Parts: None");

            Assert.That(vehicle.ToString(), Is.EqualTo(result.ToString()));

            var attackPart = new ArsenalPart("ArsenalModel", 1.5, 2.5m, 10);
            var shellPart = new ShellPart("ShellModel", 1.5, 2.5m, 15);
            var endurancePart = new EndurancePart("EnduranceModel", 1.5, 2.5m, 20);

            vehicle.AddEndurancePart(endurancePart);
            vehicle.AddArsenalPart(attackPart);
            vehicle.AddShellPart(shellPart);

            result.Clear();
            result.AppendLine($"Revenger - Model1");
            result.AppendLine($"Total Weight: {104.5:F3}");
            result.AppendLine($"Total Price: {107.5:F3}");
            result.AppendLine($"Attack: {20}");
            result.AppendLine($"Defense: {25}");
            result.AppendLine($"HitPoints: {30}");

            result.Append("Parts: EnduranceModel, ArsenalModel, ShellModel");

            Assert.That(vehicle.ToString(), Is.EqualTo(result.ToString()));
        }
    }
}