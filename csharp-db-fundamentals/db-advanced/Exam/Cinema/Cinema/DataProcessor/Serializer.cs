namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context
                .Movies
                .Where(m => m.Rating >= rating && m.Projections.SelectMany(x => x.Tickets).Any())
                .Select(m => new
                {
                    MovieName = m.Title,
                    Rating = $"{m.Rating:f2}",
                    TotalIncomes = $"{ m.Projections.Sum(x => x.Tickets.Sum(y => y.Price)):f2}",
                    Customers = m.Projections
                    .SelectMany(p => p.Tickets)
                    .Select(t => new
                    {
                        FirstName = t.Customer.FirstName,
                        LastName = t.Customer.LastName,
                        Balance = $"{t.Customer.Balance:f2}"
                    })
                    .OrderByDescending(c => c.Balance)
                    .ThenBy(c => c.FirstName)
                    .ThenBy(c => c.LastName)
                    .ToList()
                })
                .OrderByDescending(m => double.Parse(m.Rating))
                .ThenByDescending(m => double.Parse(m.TotalIncomes))
                .Take(10)
                .ToArray();

            ;
            var result = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);

            return result;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var att = new XmlRootAttribute("Customers");
            var serializer = new XmlSerializer(typeof(ExportCustomersDTO[]), att);

            var customers = context.Customers
                .Where(c => c.Age >= age)
                .Select(c => new ExportCustomersDTO
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SpentMoney = $"{c.Tickets.Sum(t => t.Price):f2}",
                    SpentTime = GetTimeSpent(c.Tickets
                    .Select(x => x.Projection.Movie.Duration).ToList())
                })
                .OrderByDescending(c => double.Parse(c.SpentMoney))
                .Take(10)
                .ToArray();

            var namespaces = new XmlSerializerNamespaces(new[] {
                XmlQualifiedName.Empty
            });

            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), customers, namespaces);
            return sb.ToString();
        }

        private static string GetTimeSpent(List<TimeSpan> durations)
        {
            var timeSpent = TimeSpan.Zero;

            foreach (var duration in durations)
            {
                timeSpent = timeSpent.Add(duration);
            }

            return timeSpent.ToString();
        }
    }
}