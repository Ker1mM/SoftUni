using Microsoft.EntityFrameworkCore;
using Moq;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services;
using Rider.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Rider.Tests.Services
{
    public class BikesServiceTests
    {
        private DbContextOptions<RiderDBContext> GetDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<RiderDBContext>()
                        .UseInMemoryDatabase(databaseName: dbName)
                        .Options;
        }

        [Fact]
        public void GetBikePartById_ShouldReturnCorrectBikePart()
        {
            var options = GetDbOptions("GetBikePartById_Database");

            var testId = 128;
            var dummyBikePart = new BikeParts() { Id = testId };
            var dummyBikePartFake1 = new BikeParts() { Id = 1 };
            var dummyBikePartFake2 = new BikeParts() { Id = 2 };

            using (var context = new RiderDBContext(options))
            {
                context.BikeParts.Add(dummyBikePart);
                context.BikeParts.Add(dummyBikePartFake1);
                context.BikeParts.Add(dummyBikePartFake2);
                context.SaveChanges();
            }

            BikeParts actual = null;
            using (var context = new RiderDBContext(options))
            {
                var playersService = new Mock<IPlayersService>();
                var bikesService = new BikesService(context, playersService.Object);
                actual = bikesService.GetBikePartById(testId);
            }

            Assert.Equal(testId, actual.Id);
        }

        [Fact]
        public void AddBikeToPlayer_ShouldAddBikeToDatabase()
        {
            var options = GetDbOptions("AddBikeToPlayer_Database");

            var dummyPlayer = new Player() { UserName = "dummyPlayer" };

            var dummyBike1 = new Bike();
            var dummyBike2 = new Bike();

            using (var context = new RiderDBContext(options))
            {
                context.Users.Add(dummyPlayer);
                context.SaveChanges();
            }

            using (var context = new RiderDBContext(options))
            {
                var playersService = new Mock<IPlayersService>();
                playersService.Setup(u => u.GetPlayerByUsername(dummyPlayer.UserName))
                            .Returns(context.Users.FirstOrDefault(x => x.UserName == dummyPlayer.UserName));

                var bikesService = new BikesService(context, playersService.Object);

                 bikesService.AddBikeToPlayer(dummyPlayer.UserName, dummyBike1);
                 bikesService.AddBikeToPlayer(dummyPlayer.UserName, dummyBike2);
            }

            Player actual = null;
            using (var context = new RiderDBContext(options))
            {
                actual = context.Users
                     .Include(x => x.Bikes)
                     .FirstOrDefault(x => x.UserName == dummyPlayer.UserName);
            }

            Assert.Equal(2, actual.Bikes.Count());
            Assert.Contains(actual.Bikes, x => x.Id == dummyBike1.Id);
            Assert.Contains(actual.Bikes, x => x.Id == dummyBike2.Id);
        }

        [Fact]
        public  void AddBikePart_ShouldAddPartToBike()
        {
            var options = GetDbOptions("AddBikePart_Database");

            var dummyBikePart = new BikeParts();

            using (var context = new RiderDBContext(options))
            {
                var playersService = new Mock<IPlayersService>();
                var bikesService = new BikesService(context, playersService.Object);

                 bikesService.AddBikePart(dummyBikePart);
            }

            List<BikeParts> actual = null;
            using (var context = new RiderDBContext(options))
            {
                actual = context.BikeParts.ToList();
            }

            Assert.Single(actual);
            Assert.Contains(actual, x => x.Id == dummyBikePart.Id);
        }

        [Theory]
        [InlineData(231, true)]
        [InlineData(92, true)]
        [InlineData(91, false)]
        public void GetBikeById_ShouldReturnCorrectBike(int input, bool expected)
        {
            var options = GetDbOptions(string.Format("GetBikeById_{0}_Database", input));

            var dummyAttempts = new List<Attempt>();
            var dummyPlayer = new Player();
            var dummyBikeParts = new List<BikeParts>();

            var dummyBikes = new List<Bike>() {
                    new Bike { Id = 231, Attempts = dummyAttempts, Player = dummyPlayer, BikeParts = dummyBikeParts },
                    new Bike { Id = 92, Attempts = dummyAttempts, Player = dummyPlayer, BikeParts = dummyBikeParts },
                    new Bike { Id = 233321, Attempts = dummyAttempts, Player = dummyPlayer, BikeParts = dummyBikeParts },
            };

            using (var context = new RiderDBContext(options))
            {
                context.Bikes.AddRange(dummyBikes);
                context.SaveChanges();
            }

            Bike actual = null;
            using (var context = new RiderDBContext(options))
            {
                var playersService = new Mock<IPlayersService>();
                var bikesService = new BikesService(context, playersService.Object);
                actual = bikesService.GetBikeById(input);
            }

            Assert.Equal(expected, actual != null && dummyBikes.Any(x => x.Id == actual.Id));
        }

        [Fact]
        public void GetUserBikesByUsername_ShouldReturnCorrectUserBikes()
        {
            var options = GetDbOptions("GetUserBikesByUsername_Database");

            var dummyPlayer1 = new Player() { UserName = "Dummy1" };
            var dummyPlayer2 = new Player() { UserName = "Dummy2" };

            var dummyBikeParts = new List<BikeParts>();

            var bikes = new List<Bike>
            {
                new Bike{Id = 2 ,Player = dummyPlayer1, BikeParts = dummyBikeParts},
                new Bike{Id = 3, Player = dummyPlayer1, BikeParts = dummyBikeParts},
                new Bike{Id = 4, Player = dummyPlayer1, BikeParts = dummyBikeParts},
                new Bike{Id = 12, Player = dummyPlayer2, BikeParts = dummyBikeParts},
            };

            using (var context = new RiderDBContext(options))
            {
                context.Bikes.AddRange(bikes);
                context.SaveChanges();
            }

            var actualBikes = new List<Bike>();
            using (var context = new RiderDBContext(options))
            {
                var playersService = new Mock<IPlayersService>();
                var bikesService = new BikesService(context, playersService.Object);

                actualBikes = bikesService.GetUserBikesByUsername(dummyPlayer1.UserName).ToList();
            }

            Assert.Equal(3, actualBikes.Count());
            Assert.Contains(actualBikes, x => x.Id == 2);
            Assert.Contains(actualBikes, x => x.Id == 3);
            Assert.Contains(actualBikes, x => x.Id == 4);
            Assert.DoesNotContain(actualBikes, x => x.Id == 12);
        }

        [Theory]
        [InlineData("Taken1", true)]
        [InlineData("Taken12", false)]
        [InlineData("TaKen3", true)]
        [InlineData("taken4", true)]
        [InlineData("Taken_4", false)]
        public void NicknameTaken_ShouldReturnIfNicknameIsTaken(string nickname, bool expected)
        {
            var options = GetDbOptions(string.Format("NicknameTaken_{0}_Database", nickname));

            var dummyPlayer = new Player() { UserName = "Dummy" };

            var dummyBikeParts = new List<BikeParts>();

            var bikes = new List<Bike>
            {
                new Bike{Nickname = "Taken1" ,Player = dummyPlayer, BikeParts = dummyBikeParts},
                new Bike{Nickname = "Taken4", Player = dummyPlayer, BikeParts = dummyBikeParts},
                new Bike{Nickname = "Taken2", Player = dummyPlayer, BikeParts = dummyBikeParts},
                new Bike{Nickname = "Taken3", Player = dummyPlayer, BikeParts = dummyBikeParts},
            };

            using (var context = new RiderDBContext(options))
            {
                context.AddRange(bikes);
                context.SaveChanges();
            }

            bool actual;
            using (var context = new RiderDBContext(options))
            {
                var playersService = new Mock<IPlayersService>();
                var bikesService = new BikesService(context, playersService.Object);
                actual = bikesService.NicknameTaken(nickname, dummyPlayer.UserName);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public  void RemoveBikePart_ShouldRemoveCorrectBikePart()
        {
            var options = GetDbOptions("RemovePartFromBike_Database");

            var dummyBikeParts = new List<BikeParts>()
            {
                new BikeParts() { Id = 23  },
                new BikeParts() { Id =  12 },
                new BikeParts() { Id =  44 },
                new BikeParts() { Id =  11 },
            };

            using (var context = new RiderDBContext(options))
            {
                context.BikeParts.AddRange(dummyBikeParts);
                context.SaveChanges();
            }

            bool actual;
            var actualBikes = new List<BikeParts>();
            using (var context = new RiderDBContext(options))
            {
                var playersService = new Mock<IPlayersService>();
                var bikesService = new BikesService(context, playersService.Object);
                actual =  bikesService.RemoveBikePart(11);
                actualBikes = context.BikeParts.ToList();
            }

            Assert.True(actual);
            Assert.DoesNotContain(actualBikes, x => x.Id == 11);
        }

        [Fact]
        public  void RemoveBikeById_ShouldRemoveCorrectBike()
        {
            var options = GetDbOptions("_Database");
            const int BikeToRemoveId = 55;

            var dummyAttempts = new List<Attempt>();
            var dummyPlayer = new Player();
            var dummyBikeParts = new List<BikeParts>();

            var dummyBikes = new List<Bike>
            {
                new Bike { Id= 32,Attempts = dummyAttempts , Player = dummyPlayer, BikeParts = dummyBikeParts},
                new Bike { Id= BikeToRemoveId, Attempts = dummyAttempts, Player = dummyPlayer, BikeParts = dummyBikeParts},
                new Bike { Id= 66, Attempts = dummyAttempts, Player = dummyPlayer, BikeParts = dummyBikeParts},
            };

            using (var context = new RiderDBContext(options))
            {
                context.Bikes.AddRange(dummyBikes);
                context.SaveChanges();
            }

            bool actual;
            var actualBikes = new List<Bike>();
            using (var context = new RiderDBContext(options))
            {
                var playersService = new Mock<IPlayersService>();
                var bikesService = new BikesService(context, playersService.Object);
                actual = bikesService.RemoveBikeById(BikeToRemoveId);
                actualBikes = context.Bikes.ToList();
            }

            Assert.True(actual);
            Assert.Equal(2, actualBikes.Count());
            Assert.DoesNotContain(actualBikes, x => x.Id == BikeToRemoveId);
        }
    }
}