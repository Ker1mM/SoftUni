namespace FestivalManager.Core.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Entities.Contracts;
    using FestivalManager.Entities.Factories;
    using FestivalManager.Entities.Factories.Contracts;

    public class FestivalController : IFestivalController
    {
        private const string TimeFormat = "mm\\:ss";
        private const string TimeFormatLong = "{0:d2}:{1:d2}";
        private const string TimeFormatThreeDimensional = "{0:3D}:{1:3D}";

        private readonly IStage stage;
        private readonly ISongFactory songFactory;
        private readonly IPerformerFactory performerFactory;
        private readonly IInstrumentFactory instrumentFactory;
        private readonly ISetFactory setFactory;

        public FestivalController(IStage stage)
        {
            this.stage = stage;
            this.songFactory = new SongFactory();
            this.performerFactory = new PerformerFactory();
            this.instrumentFactory = new InstrumentFactory();
            this.setFactory = new SetFactory();
        }

        public string ProduceReport()
        {
            var result = string.Empty;

            var totalFestivalLength = new TimeSpan(this.stage.Sets.Sum(s => s.ActualDuration.Ticks));

            result += ($"Festival length: {String.Format(TimeFormatLong, (int)totalFestivalLength.TotalMinutes, totalFestivalLength.Seconds)}") + "\n";

            foreach (var set in this.stage.Sets)
            {
                var setDuration = set.ActualDuration;
                result += ($"--{set.Name} ({String.Format(TimeFormatLong, (int)setDuration.TotalMinutes, setDuration.Seconds)}):") + "\n";

                var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);
                foreach (var performer in performersOrderedDescendingByAge)
                {
                    var instruments = string.Join(", ", performer.Instruments
                        .OrderByDescending(i => i.Wear));

                    result += ($"---{performer.Name} ({instruments})") + "\n";
                }

                if (!set.Songs.Any())
                    result += ("--No songs played") + "\n";
                else
                {
                    result += ("--Songs played:") + "\n";
                    foreach (var song in set.Songs)
                    {
                        result += ($"----{song.Name} ({song.Duration.ToString(TimeFormat)})") + "\n";
                    }
                }
            }

            return result.ToString();
        }

        public string RegisterSet(string[] args)
        {
            var name = args[0];
            var type = args[1];
            var set = this.setFactory.CreateSet(name, type);

            this.stage.AddSet(set);
            return $"Registered {type} set";
        }

        public string SignUpPerformer(string[] args)
        {
            var name = args[0];
            var age = int.Parse(args[1]);

            var instrumentTypes = args.Skip(2).ToArray();

            var instruments = instrumentTypes
                .Select(i => this.instrumentFactory.CreateInstrument(i))
                .ToArray();

            var performer = this.performerFactory.CreatePerformer(name, age);

            foreach (var instrument in instruments)
            {
                performer.AddInstrument(instrument);
            }

            this.stage.AddPerformer(performer);

            return $"Registered performer {performer.Name}";
        }

        public string RegisterSong(string[] args)
        {
            var name = args[0];
            var timeSpan = TimeSpan.ParseExact(args[1], TimeFormat, CultureInfo.InvariantCulture);
            var song = this.songFactory.CreateSong(name, timeSpan);

            this.stage.AddSong(song);

            return $"Registered song {name} ({timeSpan:mm\\:ss})";
        }

        public string AddSongToSet(string[] args)
        {
            var songName = args[0];
            var setName = args[1];

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            if (!this.stage.HasSong(songName))
            {
                throw new InvalidOperationException("Invalid song provided");
            }

            var set = this.stage.GetSet(setName);
            var song = this.stage.GetSong(songName);

            set.AddSong(song);

            string result = $"Added {song.Name} ({song.Duration.ToString(TimeFormat)}) to {set.Name}";
            return result;
        }

        // Временно!!! Чтобы работало делаем срез на конец месяца
        public string AddPerformerToSet(string[] args)
        {
            var performerName = args[0];
            var setName = args[1];

            if (!this.stage.HasPerformer(performerName))
            {
                throw new InvalidOperationException("Invalid performer provided");
            }

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            var performer = this.stage.GetPerformer(performerName);
            var set = this.stage.GetSet(setName);

            set.AddPerformer(performer);

            return $"Added {performer.Name} to {set.Name}";
        }

        public string RepairInstruments(string[] args)
        {
            var instrumentsToRepair = this.stage.Performers
                .SelectMany(p => p.Instruments)
                .Where(i => i.Wear < 100)
                .ToArray();

            foreach (var instrument in instrumentsToRepair)
            {
                instrument.Repair();
            }

            return $"Repaired {instrumentsToRepair.Length} instruments";
        }
    }
}