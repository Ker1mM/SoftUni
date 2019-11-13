using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rider.Data;
using Rider.Domain.Models;
using Rider.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Services
{
    public class PlayersService : IPlayersService
    {
        private readonly RiderDBContext context;
        private readonly UserManager<Player> userManager;

        public PlayersService(RiderDBContext context, UserManager<Player> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public string GetUserRoles(string username)
        {
            var user = this.GetUserByUsername(username);
            var role = this.userManager.GetRolesAsync(user);
            var result = string.Join(", ", role.Result);

            return result;
        }

        public Player GetUserByUsername(string username)
        {
            return this.userManager.FindByNameAsync(username)
                .GetAwaiter().GetResult();
        }

        public Player GetPlayerByUsername(string username)
        {
            var player = this.context.Users
                .Include(x => x.Attempts)
                .Include(x => x.Bikes)
                .Include(x => x.Inventory)
                .ThenInclude(x => x.Part)
                .FirstOrDefault(x => x.UserName == username);

            return player;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return this.context.Users
                .OrderByDescending(x => x.CreatedOn)
                .ToList();
        }

        public Player UpdateRoleByUsername(string username, string oldRole, string newRole)
        {
            var player = this.GetUserByUsername(username);

            this.userManager.RemoveFromRoleAsync(player, oldRole).GetAwaiter().GetResult();

            this.userManager.AddToRoleAsync(player, newRole).GetAwaiter().GetResult();

            return player;
        }

        public bool SetPartForSale(int playerPartId)
        {
            var playerPart = this.GetPlayerPartById(playerPartId);

            if (playerPart == null)
            {
                return false;
            }

            playerPart.IsForSale = true;

            this.context.Entry(playerPart).State = EntityState.Modified;
            this.context.SaveChanges();

            return true;
        }

        public bool RemovePartFromSale(int playerPartId)
        {
            var playerPart = this.GetPlayerPartById(playerPartId);

            if (playerPart == null)
            {
                return false;
            }

            playerPart.IsForSale = false;

            this.context.Entry(playerPart).State = EntityState.Modified;
            this.context.SaveChanges();

            return true;
        }

        public bool UsePart(int playerPartId, int bikePartId)
        {
            var playerPart = this.GetPlayerPartById(playerPartId);

            if (playerPart == null)
            {
                return false;
            }

            playerPart.IsUsed = true;
            playerPart.BikePartId = bikePartId;

            this.context.Entry(playerPart).State = EntityState.Modified;
            this.context.SaveChanges();

            return true;
        }

        public bool DisusePart(int playerPartId)
        {
            var playerPart = this.GetPlayerPartById(playerPartId);

            if (playerPart == null)
            {
                return false;
            }

            playerPart.IsUsed = false;
            playerPart.BikePart = null;

            this.context.Entry(playerPart).State = EntityState.Modified;
            this.context.SaveChanges();

            return true;
        }

        public PlayerParts GetPlayerPartById(int playerPartId)
        {
            var playerPart = this.context.PlayerParts
                .FirstOrDefault(x => x.Id == playerPartId);

            return playerPart;
        }

        public PlayerParts AddPartToInventory(int partId, string playerId)
        {
            var part = this.context.Parts.FirstOrDefault(x => x.Id == partId);
            var player = this.context.Users.FirstOrDefault(x => x.Id == playerId);

            var playerParts = new PlayerParts();

            playerParts.Part = part;
            playerParts.Player = player;

            this.context.PlayerParts.Add(playerParts);
            this.context.SaveChanges();

            return playerParts;
        }

        public bool RemovePartFromInventory(int playerPartId)
        {
            var playerPart = this.GetPlayerPartById(playerPartId);

            this.context.PlayerParts.Remove(playerPart);
            this.context.SaveChanges();

            return true;
        }

        public bool AddTokens(string playerId, decimal amount)
        {
            var player = this.context.Users.FirstOrDefault(x => x.Id == playerId);

            if (player == null)
            {
                return false;
            }

            player.Balance += amount;
            this.context.Entry(player).State = EntityState.Modified;
            this.context.SaveChanges();

            return true;
        }

        public bool RemoveTokens(string playerId, decimal amount)
        {
            var player = this.context.Users.FirstOrDefault(x => x.Id == playerId);

            if (player == null)
            {
                return false;
            }

            player.Balance -= amount;
            this.context.Entry(player).State = EntityState.Modified;
            this.context.SaveChanges();

            return true;
        }

        public Player GetLatestPlayer()
        {
            var player = this.context.Users
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefault();

            return player;
        }
    }
}
