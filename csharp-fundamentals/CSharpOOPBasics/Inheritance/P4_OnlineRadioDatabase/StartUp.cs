using System;

namespace P4_OnlineRadioDatabase
{
    public class StartUp
    {
        static void Main()
        {
            DateTime totalSongDuration = new DateTime();
            int songCount = 0;

            int count = int.Parse(Console.ReadLine());
            while (count-- > 0)
            {
                try
                {
                    string input = Console.ReadLine();
                    string[] args = input.Split(";");
                    string artistName = args[0];
                    string songName = args[1];
                    string[] durationArgs = args[2].Split(":");
                    if (!int.TryParse(durationArgs[0], out int minutes))
                    {
                        throw new InvalidSongLengthException();
                    }
                    if (!int.TryParse(durationArgs[1], out int seconds))
                    {
                        throw new InvalidSongLengthException();
                    }


                    Song song = new Song(artistName, songName, minutes, seconds);
                    Console.WriteLine("Song added.");
                    songCount++;
                    totalSongDuration = totalSongDuration.AddMinutes(minutes);
                    totalSongDuration = totalSongDuration.AddSeconds(seconds);
                }
                catch (InvalidSongException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException) { }
            }

            Console.WriteLine($"Songs added: {songCount}");
            Console.WriteLine("Playlist length: {0}h {1}m {2}s", totalSongDuration.Hour, totalSongDuration.Minute, totalSongDuration.Second);
        }
    }
}
