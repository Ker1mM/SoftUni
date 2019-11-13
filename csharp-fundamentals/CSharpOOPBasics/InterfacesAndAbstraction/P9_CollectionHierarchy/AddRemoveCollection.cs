using P9_CollectionHierarchy.Interfaces;
using System;

namespace P9_CollectionHierarchy
{
    public class AddRemoveCollection : AddCollection, IAddRemoveCollection
    {
        public AddRemoveCollection() : base() { }

        public override int Add(string item)
        {
            Items.Insert(0, item);
            return 0;
        }

        public virtual string Remove()
        {
            if (Items.Count == 0)
            {
                throw new InvalidOperationException("Empty collection!");
            }

            string lastElement = Items[Items.Count - 1];
            Items.RemoveAt(Items.Count - 1);
            return lastElement;
        }
    }
}
