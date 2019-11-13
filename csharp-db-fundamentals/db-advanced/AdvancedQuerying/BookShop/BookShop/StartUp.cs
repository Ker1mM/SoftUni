namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            //using (var db = new BookShopContext())
            //{
            //    DbInitializer.ResetDatabase(db);
            //}
        }

        /// <summary>
        /// Problem 15.
        /// </summary>
        /// <returns></returns>
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200);

            context.Books.RemoveRange(books);
            int result = books.Count();
            context.SaveChanges();

            return result;
        }

        /// <summary>
        /// Problem 14.
        /// </summary>
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010);

            foreach (var book in books)
            {
                book.Price = book.Price + 5;
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Problem 13.
        /// </summary>
        /// <returns></returns>
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Select(b => new
                {
                    Category = b.Name,
                    Books = b.CategoryBooks
                        .Select(cb => new
                        {
                            Title = cb.Book.Title,
                            ReleaseDate = cb.Book.ReleaseDate
                        })
                        .OrderByDescending(cb => cb.ReleaseDate)
                        .Take(3)
                })
                .OrderBy(b => b.Category);

            var sb = new StringBuilder();
            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Category}");
                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Problem 12.
        /// </summary>
        /// <returns></returns>
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(b => new
                {
                    Category = b.Name,
                    Profit = b.CategoryBooks.Sum(s => s.Book.Price * s.Book.Copies)
                })
                .OrderByDescending(b => b.Profit);

            var sb = new StringBuilder();
            foreach (var category in categories)
            {
                sb.AppendLine($"{category.Category} ${category.Profit}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Problem 11.
        /// </summary>
        /// <returns></returns>
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    Name = a.FirstName + " " + a.LastName,
                    CopiesCount = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.CopiesCount);

            var sb = new StringBuilder();
            foreach (var author in authors)
            {
                sb.AppendLine($"{author.Name} - {author.CopiesCount}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Problem 10.
        /// </summary>
        /// <returns></returns>
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int result = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return result;
        }

        /// <summary>
        /// Problem 9.
        /// </summary>
        /// <returns></returns>
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(b => new
                {
                    b.BookId,
                    b.Title,
                    AuthorFullName = b.Author.FirstName + " " + b.Author.LastName
                })
                .OrderBy(b => b.BookId);

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.AuthorFullName})");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Problem 8.
        /// </summary>
        /// <returns></returns>
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b.Title);

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Problem 7.
        /// </summary>
        /// <returns></returns>
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(x => x.FullName);

            var sb = new StringBuilder();
            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FullName}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Problem 6.
        /// </summary>
        /// <returns></returns>
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime givenDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(d => d.ReleaseDate < givenDate)
                .OrderByDescending(x => x.ReleaseDate);

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Problem 5.
        /// </summary>
        /// <returns></returns>
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var allowedCategories = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            var books = context.Books
                .Where(bc => bc.BookCategories
                             .Any(s => allowedCategories.Contains(s.Category.Name.ToLower())))
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToArray();

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Problem 4.
        /// </summary>
        /// <returns></returns>
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(y => y.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId);

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Problem 3.
        /// </summary>
        /// <returns></returns>
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(p => p.Price > 40)
                .OrderByDescending(p => p.Price);

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Problem 2.
        /// </summary>
        /// <returns></returns>
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.EditionType == EditionType.Gold && x.Copies < 5000)
                .OrderBy(x => x.BookId);

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Problem 1.
        /// </summary>
        /// <returns></returns>
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            command = command.ToLower();
            command = Char.ToUpper(command[0]) + command.Substring(1);

            var restriction = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command);

            var books = context.Books
                .Where(x => x.AgeRestriction == restriction)
                .OrderBy(x => x.Title);

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString();
        }
    }
}
