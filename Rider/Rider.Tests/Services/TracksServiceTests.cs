using Microsoft.EntityFrameworkCore;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Rider.Tests.Services
{
    public class TracksServiceTests
    {
        private DbContextOptions<RiderDBContext> GetDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<RiderDBContext>()
                        .UseInMemoryDatabase(databaseName: dbName)
                        .Options;
        }

        [Fact]
        public void AddTrack_ShouldAddTrack()
        {
            var options = GetDbOptions("AddTrack_Database");

            var dummyTrack = new Track();

            using (var context = new RiderDBContext(options))
            {
                var tracksService = new TracksService(context);
                tracksService.AddTrack(dummyTrack);
            }

            List<Track> actual;
            using (var context = new RiderDBContext(options))
            {
                actual = context.Tracks.ToList();
            }

            Assert.NotNull(actual);
            Assert.Contains(actual, x => x.Id == dummyTrack.Id);
        }

        [Fact]
        public void ArchiveTrackById_ShouldSetIsArchivedTrue()
        {
            var options = GetDbOptions("ArchiveTrackById_Database");

            var dummyTrack = new Track();

            using (var context = new RiderDBContext(options))
            {
                context.Tracks.Add(dummyTrack);
                context.SaveChanges();
            }

            Track actual;
            using (var context = new RiderDBContext(options))
            {
                var tracksService = new TracksService(context);
                actual = tracksService.ArchiveTrackById(dummyTrack.Id);
            }

            Assert.NotNull(actual);
            Assert.True(actual.IsArchived);
        }

        [Fact]
        public void EditTrack_ShouldSaveEditedTrack()
        {
            var options = GetDbOptions("EditTrack_Database");

            var dummyTrack = new Track();

            using (var context = new RiderDBContext(options))
            {
                context.Tracks.Add(dummyTrack);
                context.SaveChanges();
            }

            dummyTrack.Name = "Edited";
            dummyTrack.Description = "Edited";
            dummyTrack.Distance = 10;
            dummyTrack.Elevation = 100;

            Track actual;
            using (var context = new RiderDBContext(options))
            {
                var tracksService = new TracksService(context);
                actual = tracksService.EditTrack(dummyTrack);
            }

            Assert.Equal("Edited", actual.Name);
            Assert.Equal("Edited", actual.Description);
            Assert.Equal(10, dummyTrack.Distance);
            Assert.Equal(100, dummyTrack.Elevation);
        }

        [Fact]
        public void GetAllTracks_ShouldReturnAllTracks()
        {
            var options = GetDbOptions("GetAllTracks_Database");

            var dummyTrack1 = new Track();
            var dummyTrack2 = new Track();
            var dummyTrack3 = new Track();

            using (var context = new RiderDBContext(options))
            {
                context.Tracks.Add(dummyTrack1);
                context.Tracks.Add(dummyTrack2);
                context.Tracks.Add(dummyTrack3);
                context.SaveChanges();
            }

            List<Track> actual;
            using (var context = new RiderDBContext(options))
            {
                var tracksService = new TracksService(context);
                actual = tracksService.GetAllTracks().ToList();
            }

            Assert.Equal(3, actual.Count);
            Assert.Contains(actual, x => x.Id == dummyTrack1.Id);
            Assert.Contains(actual, x => x.Id == dummyTrack3.Id);
            Assert.Contains(actual, x => x.Id == dummyTrack2.Id);
        }

        [Fact]
        public void GetTrackById_ShouldReturnCorrectTrack()
        {
            var options = GetDbOptions("_Database");

            var dummyTrack1 = new Track();
            var dummyTrack2 = new Track();
            var dummyTrack3 = new Track();

            using (var context = new RiderDBContext(options))
            {
                context.Tracks.Add(dummyTrack1);
                context.Tracks.Add(dummyTrack2);
                context.Tracks.Add(dummyTrack3);
                context.SaveChanges();
            }

            Track actual;
            using (var context = new RiderDBContext(options))
            {
                var tracksService = new TracksService(context);
                actual = tracksService.GetTrackById(dummyTrack2.Id);
            }

            Assert.Equal(dummyTrack2.Id, actual.Id);
        }

        [Fact]
        public void GetMostPopular_ShouldReturnTrackWithMostAttempts()
        {
            var options = GetDbOptions("GetMostPopular_Database");

            var dummyAttempt1 = new Attempt();
            var dummyAttempt2 = new Attempt();
            var dummyAttempt3 = new Attempt();

            var dummyTrack1 = new Track();
            var dummyTrack2 = new Track();
            dummyTrack1.Attempts.Add(dummyAttempt1);
            dummyTrack1.Attempts.Add(dummyAttempt2);
            dummyTrack2.Attempts.Add(dummyAttempt3);

            using (var context = new RiderDBContext(options))
            {
                context.Tracks.Add(dummyTrack1);
                context.Tracks.Add(dummyTrack2);
                context.SaveChanges();
            }

            Track actual;
            using (var context = new RiderDBContext(options))
            {
                var tracksService = new TracksService(context);
                actual = tracksService.GetMostPopular();
            }

            Assert.Equal(2, actual.Attempts.Count());
            Assert.Contains(actual.Attempts, x => x.Id == dummyAttempt1.Id);
            Assert.Contains(actual.Attempts, x => x.Id == dummyAttempt2.Id);
        }
    }
}