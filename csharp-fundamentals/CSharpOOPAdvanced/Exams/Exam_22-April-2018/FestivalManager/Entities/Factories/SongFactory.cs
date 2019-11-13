namespace FestivalManager.Entities.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Entities.Contracts;

    public class SongFactory : ISongFactory
    {
        public ISong CreateSong(string name, TimeSpan duration)
        {
            var types = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .Where(x => typeof(ISong).IsAssignableFrom(x) && !x.IsAbstract)
                .ToArray();

            var songType = types.FirstOrDefault(x => x.Name == "Song");

            var song = (ISong)Activator.CreateInstance(songType, new object[] { name, duration });
            return song;
        }
    }
}