using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services.Contracts;

namespace Rider.Services
{
    public class WaresService : IWaresService
    {
        private readonly RiderDBContext context;

        public WaresService(RiderDBContext riderDBContext)
        {
            this.context = riderDBContext;
        }

        public Ware AddWare(Ware ware)
        {
            this.context.Wares.Add(ware);
            this.context.SaveChanges();

            return ware;
        }

        public IEnumerable<Ware> GetAllWares()
        {
            var wares = this.context.Wares
                .Include(x => x.PlayerPart)
                .ThenInclude(x => x.Part)
                .Include(x => x.PlayerPart)
                .ThenInclude(x => x.Player)
                .ToList();

            return wares;
        }

        public Ware GetWareById(int wareId)
        {
            var ware = this.context.Wares
                .Include(x => x.PlayerPart)
                .ThenInclude(x => x.Player)
                .Include(x => x.PlayerPart)
                .ThenInclude(x => x.Part)
                .FirstOrDefault(x => x.Id == wareId);

            return ware;
        }

        public bool RemoveWareById(int id)
        {
            var ware = this.context.Wares.Find(id);

            if (ware == null)
            {
                return false;
            }

            this.context.Wares.Remove(ware);
            this.context.SaveChanges();

            return true;
        }

        public Ware GetLatestWare()
        {
            var ware = this.context.Wares
                .Include(x => x.PlayerPart)
                .ThenInclude(x => x.Part)
                .Include(x => x.PlayerPart)
                .ThenInclude(x => x.Player)
                .OrderByDescending(x => x.ListedOn)
                .FirstOrDefault();

            return ware;
        }
    }
}