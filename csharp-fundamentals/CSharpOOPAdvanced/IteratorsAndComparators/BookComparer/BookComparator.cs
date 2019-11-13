using System.Collections.Generic;

namespace IteratorsAndComparators
{
    public class BookComparator : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            if (x.Title == y.Title)
            {
                return y.Year.CompareTo(x.Year);
            }
            else
            {
                return x.Title.CompareTo(y.Title);
            }
        }
    }
}
