using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services.Contracts;

namespace Rider.Services
{
    public class PartsService : IPartsService
    {
        private readonly RiderDBContext context;

        public PartsService(RiderDBContext context)
        {
            this.context = context;
        }

        public Part AddPart(Part part)
        {
            this.context.Parts.Add(part);
            this.context.SaveChanges();

            return part;
        }

        public Part EditPart(Part part)
        {
            this.context.Entry(part).State = EntityState.Modified;
            this.context.SaveChanges();

            return part;
        }

        public IEnumerable<Part> GetAllParts()
        {
            var parts = this.context.Parts
                .OrderBy(x => x.Type)
                .ToList();

            return parts;
        }

        public Part GetPartById(int partId)
        {
            var part = this.context.Parts
                .FirstOrDefault(x => x.Id == partId);

            return part;
        }

        public IEnumerable<PlayerParts> GetAllPlayerPartsByUserName(string username)
        {
            var playerParts = this.context.PlayerParts
                .Include(x => x.Player)
                .Include(x => x.Part)
                .Where(x => x.Player.UserName == username)
                .ToList();

            return playerParts;
        }

        public PlayerParts GetPlayerPartByPartId(string username, int partId)
        {
            var playerPart = this.context.PlayerParts
                .Include(x => x.Player)
                .Include(x => x.Part)
                .Where(x => x.Player.UserName == username && x.PartId == partId)
                .FirstOrDefault();

            return playerPart;
        }
    }
}