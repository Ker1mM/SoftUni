using P9_CollectionHierarchy.Interfaces;
using System.Collections.Generic;

namespace P9_CollectionHierarchy
{
    public class AddCollection : IAddCollection
    {
        protected List<string> Items;

        public AddCollection()
        {
            Items = new List<string>();
        }

        public virtual int Add(string item)
        {
            Items.Add(item);
            return Items.Count - 1;
        }
    }
}
