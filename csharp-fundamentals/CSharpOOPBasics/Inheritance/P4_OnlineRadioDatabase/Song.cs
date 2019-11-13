namespace P4_OnlineRadioDatabase
{
    public class Song
    {
        private string name;
        private string artist;
        private int minutes;
        private int seconds;

        public int Seconds
        {
            get { return seconds; }
            set
            {
                if (value < 0 || value > 59)
                {
                    throw new InvalidSongSecondsException();
                }
                seconds = value;
            }
        }


        public int Minutes
        {
            get { return minutes; }
            set
            {
                if (value < 0 || value > 14)
                {
                    throw new InvalidSongMinutesException();
                }
                minutes = value;
            }
        }


        public string Artist
        {
            get { return artist; }
            set
            {
                if (value.Length < 3 || value.Length > 30)
                {
                    throw new InvalidArtistNameException();
                }
                artist = value;
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                if (value.Length < 3 || value.Length > 30)
                {
                    throw new InvalidSongNameException();
                }
                name = value;
            }
        }

        public Song(string artistName, string songName, int minutes, int seconds)
        {
            Artist = artistName;
            Name = songName;
            Minutes = minutes;
            Seconds = seconds;
        }
    }
}
