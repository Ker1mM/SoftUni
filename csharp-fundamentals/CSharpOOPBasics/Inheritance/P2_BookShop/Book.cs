using System;
using System.Text;

namespace P2_BookShop
{
    public class Book
    {

        private string title;
        private string author;
        private decimal price;
        protected string type;

        public virtual decimal Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price not valid!");
                }
                this.price = value;
            }
        }

        public string Author
        {
            get { return author; }
            set
            {
                SetAuthor(value);
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException("Title not valid!");
                }
                this.title = value;
            }
        }

        public Book(string author, string title, decimal price)
        {
            Title = title;
            Author = author;
            Price = price;
            type = "Book";
        }

        private void SetAuthor(string author)
        {
            string[] tokens = author.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length > 1 && char.IsDigit(tokens[1][0]))
            {
                throw new ArgumentException("Author not valid!");
            }
            this.author = author;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Type: {type}\n");
            sb.Append($"Title: {Title}\n");
            sb.Append($"Author: {Author}\n");
            sb.Append($"Price: {Price:F2}");

            return sb.ToString();
        }
    }
}
