using Microsoft.EntityFrameworkCore;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Rider.Tests.Services
{
    public class PartsServiceTests
    {
        private DbContextOptions<RiderDBContext> GetDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<RiderDBContext>()
                        .UseInMemoryDatabase(databaseName: dbName)
                        .Options;
        }

        [Fact]
        public void AddPart_ShouldAddPart()
        {
            var options = GetDbOptions("AddPart_Database");

            var dummyPart = new Part();

            Part actual;
            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.AddPart(dummyPart);
            }

            var partsActual = new List<Part>();
            using (var context = new RiderDBContext(options))
            {
                partsActual = context.Parts.ToList();
            }

            Assert.Equal(dummyPart, actual);
            Assert.Contains(partsActual, x => x.Id == dummyPart.Id);
        }

        [Fact]
        public void EditPart_ShouldSaveNewPartCorrectly()
        {
            var options = GetDbOptions("EditPart_Database");

            var dummyPart = new Part() { Make = "Dummy", Model = "Part" };

            using (var context = new RiderDBContext(options))
            {
                context.Parts.Add(dummyPart);
                context.SaveChanges();
            }

            Assert.Equal("Part", dummyPart.Model);
            Assert.Equal("Dummy", dummyPart.Make);

            dummyPart.Make = "Edited Make";
            dummyPart.Model = "Edited Model";

            Part actual;
            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.EditPart(dummyPart);
            }

            Assert.Equal("Edited Model", actual.Model);
            Assert.Equal("Edited Make", actual.Make);
        }

        [Fact]
        public void GetAllParts_ShoudlReturnAllParts()
        {
            var options = GetDbOptions("GetAllParts_Database");

            var actual = new List<Part>();
            using (var context = new RiderDBContext(options))
            {
                var bikesService = new PartsService(context);
                actual = bikesService.GetAllParts().ToList();
            }

            Assert.True(actual.Count() == 0);

            var dummyPart1 = new Part() { Id = 2 };
            var dummyPart2 = new Part() { Id = 4 };
            var dummyPart3 = new Part() { Id = 6 };

            using (var context = new RiderDBContext(options))
            {
                context.Parts.Add(dummyPart1);
                context.Parts.Add(dummyPart2);
                context.Parts.Add(dummyPart3);
                context.SaveChanges();
            }

            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.GetAllParts().ToList();
            }

            Assert.True(actual.Count() == 3);
            Assert.Contains(actual, x => x.Id == 2);
            Assert.Contains(actual, x => x.Id == 4);
            Assert.Contains(actual, x => x.Id == 6);
        }

        [Fact]
        public void GetPartById_ShouldReturnCorrectPart()
        {
            var options = GetDbOptions("GetPartById_Database");

            var dummyPart1 = new Part() { Id = 2, Make = "dummyMake2", Model = "DummyModel2" };
            var dummyPart2 = new Part() { Id = 4, Make = "dummyMake4", Model = "DummyModel4" };
            var dummyPart3 = new Part() { Id = 6, Make = "dummyMake6", Model = "DummyModel6" };

            using (var context = new RiderDBContext(options))
            {
                context.Parts.Add(dummyPart1);
                context.Parts.Add(dummyPart2);
                context.Parts.Add(dummyPart3);
                context.SaveChanges();
            }

            Part actual;
            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.GetPartById(2);
            }

            Assert.True(actual.Id == 2);
            Assert.True(actual.Make == "dummyMake2");
            Assert.True(actual.Model == "DummyModel2");

            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.GetPartById(6);
            }

            Assert.True(actual.Id == 6);
            Assert.True(actual.Make == "dummyMake6");
            Assert.True(actual.Model == "DummyModel6");

            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.GetPartById(22);
            }

            Assert.True(actual == null);
        }

        [Fact]
        public void GetAllPlayerPartsByUserName_ShoudlReturnCorrectPlayerParts()
        {
            var options = GetDbOptions("GetAllPlayerPartsByUserName_Database");

            var dummPlayer1 = new Player() { UserName = "Dummy1" };
            var dummPlayer2 = new Player() { UserName = "Dummy2" };

            var dummyPart1 = new Part() { Id = 1 };
            var dummyPart2 = new Part() { Id = 2 };
            var dummyPart3 = new Part() { Id = 3 };

            var dummyPlayerPart1 = new PlayerParts() { Player = dummPlayer1, Part = dummyPart1 };
            var dummyPlayerPart2 = new PlayerParts() { Player = dummPlayer1, Part = dummyPart2 };
            var dummyPlayerPart3 = new PlayerParts() { Player = dummPlayer2, Part = dummyPart3 };

            using (var context = new RiderDBContext(options))
            {
                context.PlayerParts.Add(dummyPlayerPart1);
                context.PlayerParts.Add(dummyPlayerPart2);
                context.PlayerParts.Add(dummyPlayerPart3);
                context.SaveChanges();
            }

            List<PlayerParts> actual;
            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.GetAllPlayerPartsByUserName("Dummy1").ToList();
            }

            Assert.True(actual.Count() == 2);
            Assert.Contains(actual, x => x.Id == dummyPlayerPart1.Id);
            Assert.Contains(actual, x => x.Id == dummyPlayerPart2.Id);

            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.GetAllPlayerPartsByUserName("Dummy2").ToList();
            }

            Assert.True(actual.Count() == 1);
            Assert.Contains(actual, x => x.Id == dummyPlayerPart3.Id);

            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.GetAllPlayerPartsByUserName("Dummy").ToList();
            }

            Assert.True(actual.Count() == 0);
        }

        [Fact]
        public void GetPlayerPartByPartId_ShoudlReturnCorrectPlayerParts()
        {
            var options = GetDbOptions("GetPlayerPartByPartId_Database");

            var dummyPlayer = new Player() { UserName = "Dummy" };

            var dummyPart1 = new Part() { Id = 1 };
            var dummyPart2 = new Part() { Id = 2 };

            var dummyPlayerPart1 = new PlayerParts() { Id = 12, Player = dummyPlayer, Part = dummyPart1 };
            var dummyPlayerPart2 = new PlayerParts() { Id = 21, Player = dummyPlayer, Part = dummyPart2 };

            using (var context = new RiderDBContext(options))
            {
                context.PlayerParts.Add(dummyPlayerPart1);
                context.PlayerParts.Add(dummyPlayerPart2);
                context.SaveChanges();
            }

            PlayerParts actual;
            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.GetPlayerPartByPartId("Dummy", 2);
            }

            Assert.Equal(21, actual.Id);

            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.GetPlayerPartByPartId("Dummy", 1);
            }

            Assert.Equal(12, actual.Id);

            using (var context = new RiderDBContext(options))
            {
                var partsService = new PartsService(context);
                actual = partsService.GetPlayerPartByPartId("Dummy", 3);
            }

            Assert.Null(actual);
        }
    }
}