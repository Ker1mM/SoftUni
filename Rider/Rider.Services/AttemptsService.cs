using Microsoft.EntityFrameworkCore;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rider.Services
{
    public class AttemptsService : IAttemptsService
    {
        private readonly RiderDBContext context;

        public AttemptsService(RiderDBContext context)
        {
            this.context = context;
        }

        public int GetAllAttemptsCount()
        {
            var result = this.context.Attempts.Count();

            return result;
        }

        public Attempt AddAttempt(Attempt attempt)
        {
            this.context.Attempts.Add(attempt);
            this.context.SaveChanges();

            return attempt;
        }

        public IEnumerable<Attempt> GetAllAttemptsByTrackId(string trackId)
        {
            var attempts = this.context.Attempts
                .Where(x => x.TrackId == trackId)
                .Include(x => x.Player)
                .Include(x => x.Track)
                .OrderBy(x => x.Time)
                .AsNoTracking()
                .ToList();

            return attempts;
        }

        public bool RemoveAllAtemptsByBikeId(int bikeId)
        {
            var attempts = this.context.Attempts
                .Where(x => x.PlayerBikeId == bikeId)
                .ToList();

            this.context.Attempts.RemoveRange(attempts);
            this.context.SaveChanges();

            return true;
        }

        public Attempt GetLatestAttempt()
        {
            var attempt = this.context.Attempts
                .Include(x => x.Player)
                .Include(x => x.Track)
                .OrderByDescending(x => x.AttemptedOn)
                .AsNoTracking()
                .FirstOrDefault();

            return attempt;
        }
    }
}
