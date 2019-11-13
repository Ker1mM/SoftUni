using System;
using System.Collections.Generic;

namespace IteratorsAndComparators
{
    public class Book : IComparable<Book>
    {
        public string Title { get; private set; }
        public int Year { get; private set; }
        public IReadOnlyList<string> Authors { get; private set; }

        public Book(string title, int year, params string[] author)
        {
            this.Authors = new List<string>();
            this.Title = title;
            this.Year = year;
            this.Authors = author;
        }

        public int CompareTo(Book other)
        {
            if (this.Year == other.Year)
            {
                return this.Title.CompareTo(other.Title);
            }
            else
            {
                return this.Year.CompareTo(other.Year);
            }
        }

        public override string ToString()
        {
            return $"{this.Title} - {this.Year}";
        }
    }
}
