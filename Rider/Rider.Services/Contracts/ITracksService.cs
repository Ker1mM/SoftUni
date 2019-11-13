using Rider.Domain.Models;
using System.Collections.Generic;

namespace Rider.Services.Contracts
{
    public interface ITracksService
    {
        IEnumerable<Track> GetAllTracks();

        Track AddTrack(Track track);

        Track GetTrackById(string id);

        Track EditTrack(Track track);

        Track ArchiveTrackById(string trackId);

        Track GetMostPopular();
    }
}