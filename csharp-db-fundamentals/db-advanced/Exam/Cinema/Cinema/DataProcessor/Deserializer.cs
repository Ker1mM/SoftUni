namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public static class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var moviesDTO = JsonConvert.DeserializeObject<ImportMovieDTO[]>(jsonString);

            var movies = new List<Movie>();

            foreach (var movieDTO in moviesDTO)
            {
                if (!IsValid(movieDTO) || movies.Any(x => x.Title == movieDTO.Title))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = Mapper.Map<Movie>(movieDTO);
                movies.Add(movie);
                sb.AppendLine(string.Format(SuccessfulImportMovie, movieDTO.Title, movieDTO.Genre, $"{movieDTO.Rating:f2}"));
            }


            context.Movies.AddRange(movies);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var hallsDTO = JsonConvert.DeserializeObject<ImportHallDTO[]>(jsonString);

            var halls = new List<Hall>();
            var seats = new List<Seat>();

            foreach (var hallDTO in hallsDTO)
            {
                if (!IsValid(hallDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hall = new Hall
                {
                    Name = hallDTO.Name,
                    Is3D = hallDTO.Is3D,
                    Is4Dx = hallDTO.Is4Dx,
                };


                var currentHallSeats = new List<Seat>();
                for (int i = 1; i <= hallDTO.Seats; i++)
                {
                    var seat = new Seat
                    {
                        Hall = hall
                    };
                    currentHallSeats.Add(seat);
                }

                hall.Seats = currentHallSeats;
                seats.AddRange(currentHallSeats);

                var projectionType = "Normal";
                if (hall.Is3D && !hall.Is4Dx)
                {
                    projectionType = "3D";
                }
                else if (!hall.Is3D && hall.Is4Dx)
                {
                    projectionType = "4Dx";
                }
                else if (hall.Is3D && hall.Is4Dx)
                {
                    projectionType = "4Dx/3D";
                }

                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, projectionType, hallDTO.Seats));
                halls.Add(hall);
            }


            context.Halls.AddRange(halls);
            context.Seats.AddRange(seats);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ImportProjectionDTO[]), new XmlRootAttribute("Projections"));

            var projectionsDTO = (ImportProjectionDTO[])serializer.Deserialize(new StringReader(xmlString));

            var projections = new List<Projection>();

            var movies = context.Movies;
            var halls = context.Halls;

            foreach (var projectionDTO in projectionsDTO)
            {
                var movie = movies.FirstOrDefault(x => x.Id == projectionDTO.MovieId);
                var hall = halls.FirstOrDefault(x => x.Id == projectionDTO.HallId);

                if (!IsValid(projectionDTO) || movie == null || hall == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection
                {
                    Movie = movie,
                    Hall = hall,
                    DateTime = DateTime.ParseExact(projectionDTO.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                };

                projections.Add(projection);
                sb.AppendLine(string.Format(SuccessfulImportProjection, movie.Title, $"{projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}"));
            }

            context.Projections.AddRange(projections);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ImportCustomerDTO[]), new XmlRootAttribute("Customers"));

            var customersDTO = (ImportCustomerDTO[])serializer.Deserialize(new StringReader(xmlString));

            var customers = new List<Customer>();
            var tickets = new List<Ticket>();

            var projections = context.Projections;

            foreach (var customerDTO in customersDTO)
            {

                if (!IsValid(customerDTO) || !customerDTO.Tickets.All(IsValid) || !customerDTO.Tickets.All(x => projections.Any(t => t.Id == x.ProjectionId)))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer
                {
                    FirstName = customerDTO.FirstName,
                    LastName = customerDTO.LastName,
                    Age = customerDTO.Age,
                    Balance = customerDTO.Balance
                };

                var currentCustomerTickets = new List<Ticket>();

                foreach (var ticketDTO in customerDTO.Tickets)
                {
                    var ticket = new Ticket
                    {
                        Customer = customer,
                        Projection = projections.FirstOrDefault(x => x.Id == ticketDTO.ProjectionId),
                        Price = ticketDTO.Price
                    };

                    currentCustomerTickets.Add(ticket);
                }

                customer.Tickets = currentCustomerTickets;
                tickets.AddRange(currentCustomerTickets);
                customers.Add(customer);
                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customerDTO.FirstName, customerDTO.LastName, currentCustomerTickets.Count));
            }

            context.Customers.AddRange(customers);
            context.Tickets.AddRange(tickets);

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(this object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}