using Microsoft.EntityFrameworkCore;
using Moq;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services;
using Rider.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rider.Tests.Services
{
    public class AttemptsServiceTests
    {

        private DbContextOptions<RiderDBContext> GetDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<RiderDBContext>()
                        .UseInMemoryDatabase(databaseName: dbName)
                        .Options;
        }

        [Fact]
        public void GetAllAttemptsCount_ReturnsCorrectAttemptCount()
        {
            var options = GetDbOptions("GetAllAttemptsCoun_Database");

            var attempts = new List<Attempt>()
            {
                new Attempt(),
                new Attempt(),
            };

            using (var context = new RiderDBContext(options))
            {
                context.Attempts.AddRange(attempts);
                context.SaveChanges();
            }

            int result = 0;
            using (var context = new RiderDBContext(options))
            {
                var attemptsService = new AttemptsService(context);
                result = attemptsService.GetAllAttemptsCount();
            }

            Assert.Equal(2, result);
        }

        [Fact]
        public  void AddAttempt_ShouldAddAttempt()
        {
            var options = GetDbOptions("AddAttempt_Database");

            var attempt = new Attempt()
            {
                Id = 123,
            };

            using (var context = new RiderDBContext(options))
            {
                var attemptsService = new AttemptsService(context);
                 attemptsService.AddAttempt(attempt);
            }

            Attempt result = null;

            using (var context = new RiderDBContext(options))
            {
                result = context.Attempts.FirstOrDefault();
            }

            Assert.Equal(123, result.Id);
        }

        [Fact]

        public void GetAllAttemptsByTrackId_ShouldReturnCorrectAttempts()
        {
            var options = GetDbOptions("GetAllAttemptsByTrackId_Database");

            var guidOne = Guid.NewGuid().ToString();
            var guidTwo = Guid.NewGuid().ToString();

            var trackOne = new Track()
            {
                Id = guidOne,
            };

            var trackTwo = new Track()
            {
                Id = guidTwo,
            };

            var player = new Player()
            {
                Id = guidTwo
            };

            var attempts = new List<Attempt>()
            {
                new Attempt{ Id=1, Player = player, Track = trackOne },
                new Attempt{ Id=2, Player = player ,Track = trackOne },
                new Attempt{ Id=3, Player = player, Track = trackTwo },
            };

            using (var context = new RiderDBContext(options))
            {
                context.Attempts.AddRange(attempts);
                context.SaveChanges();
            }

            var resultAttempts = new List<Attempt>();

            using (var context = new RiderDBContext(options))
            {
                var attemptsService = new AttemptsService(context);
                resultAttempts = attemptsService.GetAllAttemptsByTrackId(guidOne).ToList();
            }

            Assert.Equal(2, resultAttempts.Count());
            foreach (var attempt in resultAttempts)
            {
                Assert.Equal(guidOne, attempt.TrackId);
            }
        }

        [Theory]
        [InlineData(5, 1)]
        [InlineData(10, 2)]
        [InlineData(15, 3)]
        public  void RemoveAllAtemptsByBikeId_ShouldRemoveCorrectEntries(int input, int expectedResult)
        {
            var options = GetDbOptions(string.Format("RemoveAllAtemptsByBikeId_{0}_{1}_Database", input, expectedResult));

            var attempts = new List<Attempt>()
            {
                new Attempt{ PlayerBikeId = 5 },
                new Attempt{ PlayerBikeId =  10},
                new Attempt{ PlayerBikeId = 5 },
            };

            using (var context = new RiderDBContext(options))
            {
                context.Attempts.AddRange(attempts);
                context.SaveChanges();
            }

            int result = 0;

            using (var context = new RiderDBContext(options))
            {
                var attemptsService = new AttemptsService(context);
                 attemptsService.RemoveAllAtemptsByBikeId(input);
                result = context.Attempts.Count();
            }

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GetLatestAttempt_ReturnsCorrectEntry()
        {
            var options = GetDbOptions("GetLatestAttempt_Database");

            var track = new Track();
            var player = new Player();

            var firstAttempt = new Attempt() { Player = player, Track = track, AttemptedOn = DateTime.UtcNow };
            var secondAttempt = new Attempt() { Player = player, Track = track, AttemptedOn = DateTime.UtcNow };
            var thirdAttempt = new Attempt() { Player = player, Track = track, AttemptedOn = DateTime.UtcNow };
            var lastAttempt = new Attempt() { Player = player, Track = track, AttemptedOn = DateTime.UtcNow };

            using (var context = new RiderDBContext(options))
            {
                context.Attempts.Add(lastAttempt);
                context.Attempts.Add(firstAttempt);
                context.Attempts.Add(secondAttempt);
                context.Attempts.Add(thirdAttempt);
                context.SaveChanges();
            }

            int result = 0;

            using (var context = new RiderDBContext(options))
            {
                var attemptsService = new AttemptsService(context);
                result = attemptsService.GetLatestAttempt().Id;
            }

            Assert.Equal(lastAttempt.Id, result);
        }
    }
}