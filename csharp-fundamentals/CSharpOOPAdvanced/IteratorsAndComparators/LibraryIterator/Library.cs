using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            return new LibraryIterator(this.books);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class LibraryIterator : IEnumerator<Book>
        {
            public List<Book> books;
            private int currentIndex = -1;

            public Book Current
            {
                get
                {
                    return this.books[currentIndex];
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public LibraryIterator(IEnumerable<Book> lib)
            {
                this.Reset();
                this.books = lib.ToList();
            }

            public bool MoveNext()
            {
                currentIndex++;
                return (currentIndex < this.books.Count);
            }

            public void Reset()
            {
                this.currentIndex = -1;
            }

            public void Dispose() { }
        }
    }

}
