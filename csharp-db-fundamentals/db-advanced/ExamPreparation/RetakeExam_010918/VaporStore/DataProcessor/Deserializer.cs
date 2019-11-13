namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
    {
        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var gamesDTO = JsonConvert.DeserializeObject<List<ImportGamesDTO>>(jsonString);
            
            var sb = new StringBuilder();

            var developers = new List<Developer>();
            var genres = new List<Genre>();
            var tags = new List<Tag>();
            var games = new List<Game>();

            foreach (var gameDTO in gamesDTO)
            {
                if (!IsValid(gameDTO))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var developer = developers
                    .FirstOrDefault(d => d.Name == gameDTO.Developer);
                if (developer == null)
                {
                    developer = (new Developer
                    {
                        Name = gameDTO.Developer
                    });
                    developers.Add(developer);
                }

                var genre = genres
                    .FirstOrDefault(g => g.Name == gameDTO.Genre);
                if (genre == null)
                {
                    genre = (new Genre
                    {
                        Name = gameDTO.Genre
                    });
                    genres.Add(genre);
                }

                var currentGameTags = new List<Tag>();
                foreach (var tagDTO in gameDTO.Tags)
                {
                    var tag = tags.FirstOrDefault(t => t.Name == tagDTO);
                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Name = tagDTO
                        };
                        tags.Add(tag);
                    }
                    currentGameTags.Add(tag);
                }

                var game = new Game
                {
                    Name = gameDTO.Name,
                    Price = gameDTO.Price,
                    ReleaseDate = gameDTO.ReleaseDate,
                    Developer = developer,
                    Genre = genre,
                    GameTags = currentGameTags.Select(t => new GameTag { Tag = t }).ToHashSet()
                };

                games.Add(game);
                sb.AppendLine($"Added {gameDTO.Name} ({gameDTO.Genre}) with {gameDTO.Tags.Count()} tags");
            }

            context.Developers.AddRange(developers);
            context.Genres.AddRange(genres);
            context.Tags.AddRange(tags);
            context.Games.AddRange(games);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var usersDTO = JsonConvert.DeserializeObject<List<ImportUserDTO>>(jsonString);

            var sb = new StringBuilder();

            var users = new List<User>();
            var cards = new List<Card>();


            foreach (var userDTO in usersDTO)
            {
                if (!IsValid(userDTO) || !userDTO.Cards.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var currentUserCards = new List<Card>();

                foreach (var cardDTO in userDTO.Cards)
                {
                    var card = cards.FirstOrDefault(c => c.Number == cardDTO.Number);
                    if (card == null)
                    {
                        card = new Card
                        {
                            Number = cardDTO.Number,
                            Cvc = cardDTO.CVC,
                            Type = cardDTO.Type
                        };
                        cards.Add(card);
                    }
                    currentUserCards.Add(card);
                }

                var user = new User
                {
                    FullName = userDTO.FullName,
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Age = userDTO.Age,
                    Cards = currentUserCards
                };

                users.Add(user);
                sb.AppendLine($"Imported {userDTO.Username} with {userDTO.Cards.Count()} cards");
            }

            context.Cards.AddRange(cards);
            context.Users.AddRange(users);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var att = new XmlRootAttribute("Purchases");
            var serializer = new XmlSerializer(typeof(ImportPurchasesDTO[]), att);

            var purchasesDTO = (ImportPurchasesDTO[])serializer.Deserialize(new StringReader(xmlString));
            var games = context.Games.ToHashSet();
            var cards = context.Cards
                .Include(c => c.User)
                .ToHashSet();

            var sb = new StringBuilder();
            var purchases = new List<Purchase>();

            foreach (var purchaseDTO in purchasesDTO)
            {
                var game = games.FirstOrDefault(g => g.Name == purchaseDTO.Title);
                var card = cards.FirstOrDefault(c => c.Number == purchaseDTO.Card);

                bool isValidPurchase =
                    IsValid(purchaseDTO) &&
                    game != null &&
                    card != null;

                if (!isValidPurchase)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var purchase = new Purchase
                {
                    Type = purchaseDTO.Type,
                    ProductKey = purchaseDTO.Key,
                    Date = DateTime.ParseExact(purchaseDTO.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    Card = card,
                    Game = game
                };

                purchases.Add(purchase);
                sb.AppendLine($"Imported {purchaseDTO.Title} for {card.User.Username}");
            }

            context.AddRange(purchases);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(this object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}