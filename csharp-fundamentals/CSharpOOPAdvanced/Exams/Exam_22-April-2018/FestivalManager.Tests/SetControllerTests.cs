// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to your project (entities/controllers/etc)
namespace FestivalManager.Tests
{
    using FestivalManager.Core.Controllers;
    using FestivalManager.Entities;
    using FestivalManager.Entities.Contracts;
    using FestivalManager.Entities.Instruments;
    using FestivalManager.Entities.Sets;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class SetControllerTests
    {
        private IStage stage;

        [SetUp]
        public void InitTest()
        {
            this.stage = new Stage();
        }

        [Test]
        public void ValidatePerformSetsMethod()
        {
            var set1 = new Short("Set1");
            var set2 = new Medium("Set2");
            var set3 = new Medium("Set3");
            var song1 = new Song("Song1", new TimeSpan(0, 10, 0));
            var song2 = new Song("Song2", new TimeSpan(0, 15, 0));
            var performer1 = new Performer("Performer1", 22);
            var performer2 = new Performer("Performer2", 22);
            var instrument = new Microphone();

            performer1.AddInstrument(instrument);
            performer2.AddInstrument(instrument);
            set1.AddSong(song1);
            set1.AddPerformer(performer1);
            set2.AddSong(song2);
            set2.AddPerformer(performer1);
            set3.AddSong(song2);
            set3.AddPerformer(performer1);
            set3.AddPerformer(performer2);
            stage.AddSet(set1);
            stage.AddSet(set2);
            stage.AddSet(set3);

            var setController = new SetController(stage);
            string result = setController.PerformSets();
            string expectedResult = "1. Set3:\r\n-- 1. Song2 (15:00)\r\n-- Set Successful\r\n2. Set2:\r\n-- Did not perform\r\n3. Set1:\r\n-- Did not perform";

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}