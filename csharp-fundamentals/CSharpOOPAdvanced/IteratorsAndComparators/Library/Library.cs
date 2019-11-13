using System.Collections;
using System.Collections.Generic;

namespace IteratorsAndComparators
{
    public class Library : IEnumerable<Book>
    {
        private List<Book> books;

        public Library(params Book[] books)
        {
            this.books = new List<Book>();
            this.books.AddRange(books);
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return this.books.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.books.GetEnumerator();
        }
    }
}
