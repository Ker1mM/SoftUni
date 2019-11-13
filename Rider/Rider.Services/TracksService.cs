using Microsoft.EntityFrameworkCore;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rider.Services
{
    public class TracksService : ITracksService
    {
        private readonly RiderDBContext context;

        public TracksService(RiderDBContext context)
        {
            this.context = context;
        }

        public Track AddTrack(Track track)
        {
            this.context.Tracks.Add(track);
            this.context.SaveChanges();

            return track;
        }

        public Track ArchiveTrackById(string trackId)
        {
            var track = this.context.Tracks.Find(trackId);
            track.IsArchived = true;
            this.context.Entry(track).State = EntityState.Modified;
            this.context.SaveChanges();

            return track;
        }

        public Track EditTrack(Track track)
        {
            this.context.Entry(track).State = EntityState.Modified;
            this.context.SaveChanges();

            return track;
        }

        public IEnumerable<Track> GetAllTracks()
        {
            var tracks = this.context.Tracks
                .ToList();

            return tracks;
        }

        public Track GetTrackById(string id)
        {
            var track = this.context.Tracks.Where(x => x.Id == id).FirstOrDefault();

            return track;
        }

        public Track GetMostPopular()
        {
            var track = this.context.Tracks
                .Include(x => x.Attempts)
                .OrderByDescending(x => x.Attempts.Count())
                .FirstOrDefault();

            return track;
        }
    }
}