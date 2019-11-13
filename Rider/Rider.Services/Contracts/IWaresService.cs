using Rider.Domain.Models;
using System.Collections.Generic;

namespace Rider.Services.Contracts
{
    public interface IWaresService
    {
        Ware AddWare(Ware ware);

        IEnumerable<Ware> GetAllWares();

        bool RemoveWareById(int id);

        Ware GetWareById(int wareId);

        Ware GetLatestWare();
    }
}