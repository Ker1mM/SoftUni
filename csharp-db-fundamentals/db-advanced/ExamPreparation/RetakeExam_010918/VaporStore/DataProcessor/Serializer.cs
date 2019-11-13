namespace VaporStore.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Enums;
    using VaporStore.DataProcessor.Dto.Export;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var games =
                context.Genres
                .Where(g => genreNames.Contains(g.Name))
                .Select(g => new GamesByGenreDTO
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games.Where(p => p.Purchases.Any())
                        .Select(p => new GameInfoDTO
                        {
                            Id = p.Id,
                            Title = p.Name,
                            Developer = p.Developer.Name,
                            Tags = string.Join(", ", p.GameTags.Select(gt => gt.Tag.Name)),
                            Players = p.Purchases.Count
                        })
                        .OrderByDescending(p => p.Players)
                        .ThenBy(p => p.Id)
                        .ToList(),
                    TotalPlayers = g.Games.Sum(p => p.Purchases.Count)
                })
                .OrderByDescending(g => g.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToList();

            var result = JsonConvert.SerializeObject(games, Newtonsoft.Json.Formatting.Indented);
            return result;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var att = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(typeof(UserPurchasesDTO[]), att);

            var type = (PurchaseType)Enum.Parse(typeof(PurchaseType), storeType);

            var purchases = context.Purchases
                .Where(p => p.Type == type)
                .Select(p => new PurchaseDTO
                {
                    Username = p.Card.User.Username,
                    Card = p.Card.Number,
                    Cvc = p.Card.Cvc,
                    Date = p.Date,
                    Game = new GameDTO
                    {
                        Title = p.Game.Name,
                        Genre = p.Game.Genre.Name,
                        Price = p.Game.Price
                    }
                })
                .ToList();

            var usernames = context.Users
                .Select(u => new UserPurchasesDTO
                {
                    Username = u.Username,
                    Purchases = purchases
                        .Where(p => p.Username == u.Username)
                        .OrderBy(p => p.Date)
                        .ToList(),
                    TotalSpent = purchases
                        .Where(p => p.Username == u.Username)
                        .Sum(p => p.Game.Price)
                })
                .Where(p => p.Purchases.Count > 0)
                .OrderByDescending(p => p.TotalSpent)
                .ThenBy(p => p.Username)
                .ToArray();

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), usernames, namespaces);
            return sb.ToString();
        }
    }
}