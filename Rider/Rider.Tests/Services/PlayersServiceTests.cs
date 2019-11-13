using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Rider.Tests.Services
{
    public class PlayersServiceTests
    {
        private DbContextOptions<RiderDBContext> GetDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<RiderDBContext>()
                        .UseInMemoryDatabase(databaseName: dbName)
                        .Options;
        }

        private Mock<UserManager<Player>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<Player>>();
            return new Mock<UserManager<Player>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }


        [Fact]
        public void GetPlayerByUsername_ShouldReturnCorrectRole()
        {
            var options = GetDbOptions("GetPlayerByUsername_Database");

            var dummyAttempts = new List<Attempt>();
            var dummyPlayerParts = new List<PlayerParts>();
            var dummyBikes = new List<Bike>();

            var dummyPlayer = new Player() { UserName = "Dummy", Attempts = dummyAttempts, Inventory = dummyPlayerParts, Bikes = dummyBikes };

            using (var context = new RiderDBContext(options))
            {
                context.Users.Add(dummyPlayer);
                context.SaveChanges();
            }

            Player actual = null;
            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);

                actual = playersService.GetPlayerByUsername("Dummy");
            }

            Assert.False(string.IsNullOrEmpty(actual.Id));
            Assert.Equal(dummyPlayer.Id, actual.Id);
        }

        [Fact]
        public void GetAllPlayers_ShouldReturnAllPlayers()
        {
            var options = GetDbOptions("GetAllPlayers_Database");

            var dummyPlayer1 = new Player() { UserName = "Dummy_1" };
            var dummyPlayer2 = new Player() { UserName = "Dummy_2" };
            var dummyPlayer3 = new Player() { UserName = "Dummy_3" };

            var actual = new List<Player>();
            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);
                actual = playersService.GetAllPlayers().ToList();
            }

            Assert.Empty(actual);

            using (var context = new RiderDBContext(options))
            {
                context.Users.Add(dummyPlayer1);
                context.Users.Add(dummyPlayer2);
                context.Users.Add(dummyPlayer3);
                context.SaveChanges();
            }

            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);
                actual = playersService.GetAllPlayers().ToList();
            }

            Assert.Equal(3, actual.Count());
            Assert.Contains(actual, x => x.UserName == dummyPlayer1.UserName);
            Assert.Contains(actual, x => x.UserName == dummyPlayer2.UserName);
            Assert.Contains(actual, x => x.UserName == dummyPlayer3.UserName);
        }

        [Fact]
        public void SetPartForSale_ShouldSetPartForSale()
        {
            var options = GetDbOptions("SetPartForSale_Database");

            var dummyPlayerPart1 = new PlayerParts() { Id = 10 };
            var dummyPlayerPart2 = new PlayerParts() { Id = 20 };
            var dummyPlayerPart3 = new PlayerParts() { Id = 30 };

            using (var context = new RiderDBContext(options))
            {
                context.PlayerParts.Add(dummyPlayerPart1);
                context.PlayerParts.Add(dummyPlayerPart2);
                context.PlayerParts.Add(dummyPlayerPart3);
                context.SaveChanges();
            }

            Assert.False(dummyPlayerPart1.IsForSale);
            Assert.False(dummyPlayerPart2.IsForSale);

            PlayerParts actual1;
            PlayerParts actual2;
            bool isSet1;
            bool isSet2;
            bool isSet3;

            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);
                isSet1 = playersService.SetPartForSale(10);
                isSet2 = playersService.SetPartForSale(20);
                isSet3 = playersService.SetPartForSale(221);

                actual1 = context.PlayerParts.FirstOrDefault(x => x.Id == 10);
                actual2 = context.PlayerParts.FirstOrDefault(x => x.Id == 20);
            }

            Assert.True(isSet1);
            Assert.True(isSet2);
            Assert.False(isSet3);

            Assert.True(actual1.IsForSale);
            Assert.True(actual2.IsForSale);
        }

        [Fact]
        public void RemovePartFromSale_ShouldRemovePartFromSale()
        {
            var options = GetDbOptions("RemovePartFromSale_Database");

            var dummyPlayerPart1 = new PlayerParts() { Id = 10, IsForSale = true };
            var dummyPlayerPart2 = new PlayerParts() { Id = 20, IsForSale = true };
            var dummyPlayerPart3 = new PlayerParts() { Id = 30, IsForSale = true };

            using (var context = new RiderDBContext(options))
            {
                context.PlayerParts.Add(dummyPlayerPart1);
                context.PlayerParts.Add(dummyPlayerPart2);
                context.PlayerParts.Add(dummyPlayerPart3);
                context.SaveChanges();
            }

            Assert.True(dummyPlayerPart1.IsForSale);
            Assert.True(dummyPlayerPart2.IsForSale);

            PlayerParts actual1;
            PlayerParts actual2;
            bool isRemoved1;
            bool isRemoved2;
            bool isRemoved3;

            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);
                isRemoved1 = playersService.RemovePartFromSale(10);
                isRemoved2 = playersService.RemovePartFromSale(20);
                isRemoved3 = playersService.RemovePartFromSale(221);

                actual1 = context.PlayerParts.FirstOrDefault(x => x.Id == 10);
                actual2 = context.PlayerParts.FirstOrDefault(x => x.Id == 20);
            }

            Assert.True(isRemoved1);
            Assert.True(isRemoved2);
            Assert.False(isRemoved3);

            Assert.False(actual1.IsForSale);
            Assert.False(actual2.IsForSale);
        }

        [Fact]
        public void UsePart_ShouldSetIsUsedTrueAndChangeBikePartId()
        {
            var options = GetDbOptions("UsePart_Database");

            var dummyPlayerPart1 = new PlayerParts() { Id = 10 };
            var dummyPlayerPart2 = new PlayerParts() { Id = 20 };
            var dummyPlayerPart3 = new PlayerParts() { Id = 30 };

            var dummyBikePart = new BikeParts() { Id = 1020 };

            using (var context = new RiderDBContext(options))
            {
                context.PlayerParts.Add(dummyPlayerPart1);
                context.PlayerParts.Add(dummyPlayerPart2);
                context.PlayerParts.Add(dummyPlayerPart3);
                context.SaveChanges();
            }

            Assert.False(dummyPlayerPart1.IsUsed);
            Assert.False(dummyPlayerPart2.IsUsed);
            Assert.NotEqual(dummyPlayerPart1.BikePartId, dummyBikePart.Id);
            Assert.NotEqual(dummyPlayerPart2.BikePartId, dummyBikePart.Id);

            PlayerParts actual1;
            PlayerParts actual2;
            bool isUsed1;
            bool isUsed2;
            bool isUsed3;

            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);
                isUsed1 = playersService.UsePart(10, 1020);
                isUsed2 = playersService.UsePart(20, 1020);
                isUsed3 = playersService.UsePart(221, 1020);

                actual1 = context.PlayerParts.FirstOrDefault(x => x.Id == 10);
                actual2 = context.PlayerParts.FirstOrDefault(x => x.Id == 20);
            }

            Assert.True(isUsed1);
            Assert.True(isUsed2);
            Assert.False(isUsed3);

            Assert.True(actual1.IsUsed);
            Assert.True(actual2.IsUsed);
            Assert.Equal(dummyBikePart.Id, actual1.BikePartId);
            Assert.Equal(dummyBikePart.Id, actual2.BikePartId);
        }

        [Fact]
        public void DisusePart_ShouldSetIsUsedFalseAndRemoveBikePartId()
        {
            var options = GetDbOptions("DisusePart_Database");

            var dummyBikePart = new BikeParts();

            var dummyPlayerPart1 = new PlayerParts() { Id = 10, IsUsed = true, BikePart = dummyBikePart };
            var dummyPlayerPart2 = new PlayerParts() { Id = 20, IsUsed = true, BikePart = dummyBikePart };
            var dummyPlayerPart3 = new PlayerParts() { Id = 30, IsUsed = true, BikePart = dummyBikePart };

            using (var context = new RiderDBContext(options))
            {
                context.PlayerParts.Add(dummyPlayerPart1);
                context.PlayerParts.Add(dummyPlayerPart2);
                context.PlayerParts.Add(dummyPlayerPart3);
                context.SaveChanges();
            }

            Assert.True(dummyPlayerPart1.IsUsed);
            Assert.True(dummyPlayerPart2.IsUsed);
            Assert.NotNull(dummyPlayerPart1.BikePart);
            Assert.NotNull(dummyPlayerPart2.BikePart);

            PlayerParts actual1;
            PlayerParts actual2;
            bool isDisused1;
            bool isDisused2;
            bool isDisused3;

            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);
                isDisused1 = playersService.DisusePart(10);
                isDisused2 = playersService.DisusePart(20);
                isDisused3 = playersService.DisusePart(221);

                actual1 = context.PlayerParts.FirstOrDefault(x => x.Id == 10);
                actual2 = context.PlayerParts.FirstOrDefault(x => x.Id == 20);
            }

            Assert.True(isDisused1);
            Assert.True(isDisused2);
            Assert.False(isDisused3);

            Assert.False(actual1.IsUsed);
            Assert.False(actual2.IsUsed);
            Assert.Null(actual1.BikePart);
            Assert.Null(actual2.BikePart);
        }

        [Fact]
        public void GetPlayerPartById_ShouldReturnCorrectPlayerPart()
        {
            var options = GetDbOptions("GetPlayerPartById_Database");

            var dummyPlayerPart1 = new PlayerParts() { Id = 123 };
            var dummyPlayerPart2 = new PlayerParts() { Id = 321 };

            using (var context = new RiderDBContext(options))
            {
                context.PlayerParts.Add(dummyPlayerPart1);
                context.PlayerParts.Add(dummyPlayerPart2);
                context.SaveChanges();
            }

            PlayerParts actual;
            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);


                actual = playersService.GetPlayerPartById(123);
                Assert.Equal(123, actual.Id);

                actual = playersService.GetPlayerPartById(321);
                Assert.Equal(321, actual.Id);
            }
        }

        [Fact]
        public void AddPartToInventory_ShouldAddPlayerPart()
        {
            var options = GetDbOptions("AddPartToInventory_Database");

            var dummyPlayer = new Player();
            var dummyPart = new Part();

            using (var context = new RiderDBContext(options))
            {
                context.Users.Add(dummyPlayer);
                context.Parts.Add(dummyPart);
                context.SaveChanges();
            }

            PlayerParts actual;
            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);
                actual = playersService.AddPartToInventory(dummyPart.Id, dummyPlayer.Id);
            }

            Assert.NotNull(actual);
            Assert.Equal(dummyPlayer.Id, actual.PlayerId);
            Assert.Equal(dummyPart.Id, actual.PartId);
        }

        [Fact]
        public void RemovePartFromInventory_ShouldRemoveCorrectPlayerPart()
        {
            var options = GetDbOptions("_Database");

            var dummyPlayerPart1 = new PlayerParts() { Id = 1 };
            var dummyPlayerPart2 = new PlayerParts() { Id = 2 };

            using (var context = new RiderDBContext(options))
            {
                context.PlayerParts.Add(dummyPlayerPart1);
                context.PlayerParts.Add(dummyPlayerPart2);
                context.SaveChanges();
            }

            var actual = new List<PlayerParts>();
            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);
                playersService.RemovePartFromInventory(2);
                actual = context.PlayerParts.ToList();
            }

            Assert.Single(actual);
            Assert.Contains(actual, x => x.Id == 1);
            Assert.DoesNotContain(actual, x => x.Id == 2);
        }

        [Theory]
        [InlineData("dummyId", 10, true)]
        [InlineData("dummyId", 10.10, true)]
        [InlineData("dsummyId", 10, false)]
        public void AddTokens_ShouldAddTokenWhenCorrectDataGiven(string playerId, decimal amount, bool expected)
        {
            var options = GetDbOptions(string.Format("AddTokens_{0}_{1}_Database", playerId, amount));

            var dummyPlayer = new Player() { Id = "dummyId", Balance = 0 };

            using (var context = new RiderDBContext(options))
            {
                context.Users.Add(dummyPlayer);
                context.SaveChanges();
            }

            Player actual;
            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);
                playersService.AddTokens(playerId, amount);
                actual = context.Users.FirstOrDefault(x => x.Id == dummyPlayer.Id);
            }

            Assert.Equal(expected, actual.Balance == amount);
        }

        [Fact]
        public void RemoveTokens_ShouldRemoveCorrectAmounOfTokens()
        {
            var options = GetDbOptions("_Database");

            var dummyPlayer = new Player() { Id = "dummyId", Balance = 100 };

            using (var context = new RiderDBContext(options))
            {
                context.Users.Add(dummyPlayer);
                context.SaveChanges();
            }

            Player actual;
            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);
                playersService.RemoveTokens(dummyPlayer.Id, 50);
                actual = context.Users.FirstOrDefault(x => x.Id == dummyPlayer.Id);
            }

            Assert.NotNull(actual);
            Assert.Equal(50, actual.Balance);
        }

        [Fact]
        public void GetLatestPlayer_ShouldReturnLastAddedPlayer()
        {
            var options = GetDbOptions("GetLatestPlayer_Database");

            var dummyPlayer1 = new Player() { UserName = "First", CreatedOn = DateTime.UtcNow};
            var dummyPlayer2 = new Player() { UserName = "Second", CreatedOn = DateTime.UtcNow };
            var dummyPlayer3 = new Player() { UserName = "Last", CreatedOn = DateTime.UtcNow };

            using (var context = new RiderDBContext(options))
            {
                context.Users.Add(dummyPlayer1);
                context.Users.Add(dummyPlayer2);
                context.Users.Add(dummyPlayer3);
                context.SaveChanges();
            }

            Player actual;
            using (var context = new RiderDBContext(options))
            {
                var userManager = GetMockUserManager();
                var playersService = new PlayersService(context, userManager.Object);
                actual = playersService.GetLatestPlayer();
            }

            Assert.Equal("Last", actual.UserName);
        }
    }
}