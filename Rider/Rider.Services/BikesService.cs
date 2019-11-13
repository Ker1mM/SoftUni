using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services.Contracts;

namespace Rider.Services
{
    public class BikesService : IBikesService
    {
        private readonly RiderDBContext context;
        private readonly IPlayersService playersService;

        public BikesService(RiderDBContext context, IPlayersService playersService)
        {
            this.context = context;
            this.playersService = playersService;
        }

        public BikeParts GetBikePartById(int bikePartId)
        {
            var result = this.context.BikeParts
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == bikePartId);

            return result;
        }

        public Player AddBikeToPlayer(string username, Bike bike)
        {
            var player = playersService
                .GetPlayerByUsername(username);

            bike.Player = player;

            this.context.Bikes.Add(bike);
            this.context.SaveChanges();

            return player;
        }

        public BikeParts AddBikePart(BikeParts part)
        {
            this.context.BikeParts.Add(part);
            this.context.SaveChanges();

            return part;
        }

        public Bike GetBikeById(int bikeId)
        {
            var bike = this.context.Bikes
                .Include(x => x.Attempts)
                .Include(x => x.Player)
                .Include(x => x.BikeParts)
                .ThenInclude(x => x.PlayerPart)
                .ThenInclude(x => x.Part)
                .FirstOrDefault(x => x.Id == bikeId);

            return bike;
        }

        public IEnumerable<Bike> GetUserBikesByUsername(string username)
        {
            var bikes = this.context.Bikes
                .Include(x => x.Player)
                .Include(x => x.BikeParts)
                .ThenInclude(x => x.PlayerPart)
                .ThenInclude(x => x.Part)
                .Where(x => x.Player.UserName == username);

            return bikes;
        }

        public bool NicknameTaken(string nickname, string username)
        {
            var playerBikes = this.GetUserBikesByUsername(username);

            return playerBikes.Any(x => x.Nickname.ToLower() == nickname.ToLower());
        }

        public bool RemoveBikePart(int bikePartId)
        {
            var part = GetBikePartById(bikePartId);
            this.context.BikeParts.Remove(part);
            this.context.SaveChanges();

            return true;
        }

        public bool RemoveBikeById(int bikeId)
        {
            var bike = this.GetBikeById(bikeId);
            this.context.Bikes.Remove(bike);
            this.context.SaveChanges();

            return true;
        }
    }
}