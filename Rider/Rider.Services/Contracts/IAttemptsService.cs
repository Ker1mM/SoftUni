using Rider.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rider.Services.Contracts
{
    public interface IAttemptsService
    {
        IEnumerable<Attempt> GetAllAttemptsByTrackId(string trackId);

        Attempt AddAttempt(Attempt attempt);

        bool RemoveAllAtemptsByBikeId(int bikeId);

        Attempt GetLatestAttempt();

        int GetAllAttemptsCount();
    }
}
