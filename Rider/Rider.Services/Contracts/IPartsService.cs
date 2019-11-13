using Rider.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rider.Services.Contracts
{
    public interface IPartsService
    {
        Part AddPart(Part part);

        IEnumerable<Part> GetAllParts();

        Part GetPartById(int partId);

        Part EditPart(Part part);

        IEnumerable<PlayerParts> GetAllPlayerPartsByUserName(string username);

        PlayerParts GetPlayerPartByPartId(string username, int partId);
    }
}