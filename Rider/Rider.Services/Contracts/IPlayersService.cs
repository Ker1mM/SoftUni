using Microsoft.AspNetCore.Mvc;
using Rider.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rider.Services.Contracts
{
    public interface IPlayersService
    {
        IEnumerable<Player> GetAllPlayers();

        string GetUserRoles(string username);

        Player GetUserByUsername(string username);

        Player GetPlayerByUsername(string username);

        Player UpdateRoleByUsername(string username, string oldRole, string newRole);

        bool SetPartForSale(int playerPartId);

        bool RemovePartFromSale(int playerPartId);

        bool UsePart(int playerPartId, int bikePartId);

        bool DisusePart(int playerPartId);

        PlayerParts GetPlayerPartById(int playerPartId);

        PlayerParts AddPartToInventory(int partId, string playerId);

        bool RemovePartFromInventory(int playerPartId);

        bool AddTokens(string playerId, decimal amount);

        bool RemoveTokens(string playerId, decimal amount);

        Player GetLatestPlayer();
    }
}
