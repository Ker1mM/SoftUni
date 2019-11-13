using Microsoft.EntityFrameworkCore;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Rider.Tests.Services
{
    public class WaresServiceTests
    {
        private DbContextOptions<RiderDBContext> GetDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<RiderDBContext>()
                        .UseInMemoryDatabase(databaseName: dbName)
                        .Options;
        }

        [Fact]
        public void AddWare_ShouldAddWare()
        {
            var options = GetDbOptions("_Database");

            var dummyWare = new Ware();

            using (var context = new RiderDBContext(options))
            {
                var waresService = new WaresService(context);
                waresService.AddWare(dummyWare);
            }

            List<Ware> actual;
            using (var context = new RiderDBContext(options))
            {
                actual = context.Wares.ToList();
            }

            Assert.Single(actual);
            Assert.Contains(actual, x => x.Id == dummyWare.Id);
        }

        [Fact]
        public void GetAllWares_ShouldRetunAllWares()
        {
            var options = GetDbOptions("GetAllWares_Database");

            var dummyPlayer = new Player();
            var dummyPart = new Part();
            var dummyPlayerPart = new PlayerParts() { Player = dummyPlayer, Part = dummyPart };

            var dummyWares = new List<Ware>() { new Ware() { PlayerPart = dummyPlayerPart }, new Ware() { PlayerPart = dummyPlayerPart } };

            using (var context = new RiderDBContext(options))
            {
                context.Wares.AddRange(dummyWares);
                context.SaveChanges();
            }

            List<Ware> actual;
            using (var context = new RiderDBContext(options))
            {
                var waresService = new WaresService(context);
                actual = waresService.GetAllWares().ToList();
            }

            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void GetWareById_ShouldReturnCorrectWare()
        {
            var options = GetDbOptions("GetWareById_Database");

            var dummyPlayer = new Player();
            var dummyPart = new Part();
            var dummyPlayerPart = new PlayerParts() { Player = dummyPlayer, Part = dummyPart };

            var dummyWare1 = new Ware() { PlayerPart = dummyPlayerPart };
            var dummyWare2 = new Ware() { PlayerPart = dummyPlayerPart };

            using (var context = new RiderDBContext(options))
            {
                context.Wares.Add(dummyWare1);
                context.Wares.Add(dummyWare2);
                context.SaveChanges();
            }

            Ware actual;
            using (var context = new RiderDBContext(options))
            {
                var waresService = new WaresService(context);
                actual = waresService.GetWareById(dummyWare1.Id);
            }

            Assert.Equal(dummyWare1.Id, actual.Id);
        }

        [Fact]
        public void RemoveWareById_ShouldRemoveCorrectWare()
        {
            var options = GetDbOptions("RemoveWareById_Database");

            var dummyWare1 = new Ware() { Id = 12 };
            var dummyWare2 = new Ware() { Id = 23 };

            using (var context = new RiderDBContext(options))
            {
                context.Wares.Add(dummyWare1);
                context.Wares.Add(dummyWare2);
                context.SaveChanges();
            }

            List<Ware> actual;
            using (var context = new RiderDBContext(options))
            {
                var waresService = new WaresService(context);
                waresService.RemoveWareById(23);
                actual = context.Wares.ToList();
            }

            Assert.Single(actual);
            Assert.Contains(actual, x => x.Id == 12);
            Assert.DoesNotContain(actual, x => x.Id == 23);
        }

        [Fact]
        public void GetLatestWare_ShouldReturnLastWareByListedOn()
        {
            var options = GetDbOptions("GetLatestWare_Database");

            var dummyPlayer = new Player();
            var dummyPart = new Part();
            var dummyPlayerPart = new PlayerParts() { Player = dummyPlayer, Part = dummyPart };

            var dummyWare1 = new Ware() { PlayerPart = dummyPlayerPart, ListedOn = DateTime.UtcNow };
            var dummyWare2 = new Ware() { PlayerPart = dummyPlayerPart, ListedOn = DateTime.UtcNow };

            using (var context = new RiderDBContext(options))
            {
                context.Wares.Add(dummyWare2);
                context.Wares.Add(dummyWare1);
                context.SaveChanges();
            }

            Ware actual;
            using (var context = new RiderDBContext(options))
            {
                var waresService = new WaresService(context);
                actual = waresService.GetLatestWare();
            }

            Assert.Equal(dummyWare2.Id, actual.Id);
        }
    }
}