using Rider.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rider.Services.Contracts
{
    public interface IBikesService
    {

        IEnumerable<Bike> GetUserBikesByUsername(string username);

        Player AddBikeToPlayer(string username, Bike bike);

        bool NicknameTaken(string nickname, string username);

        Bike GetBikeById(int bikeId);

        BikeParts AddBikePart(BikeParts part);

        bool RemoveBikePart(int bikePartId);

        BikeParts GetBikePartById(int bikePartId);

        bool RemoveBikeById(int bikeId);
    }
}