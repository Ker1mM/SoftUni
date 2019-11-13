using P9_CollectionHierarchy.Interfaces;
using System;

namespace P9_CollectionHierarchy
{
    public class MyList : AddRemoveCollection, IMyList
    {

        public MyList() : base() { }

        public int Used { get { return Items.Count; } }

        public override string Remove()
        {
            if (Items.Count == 0)
            {
                throw new InvalidOperationException("Empty collection!");
            }

            string lastElement = Items[0];
            Items.RemoveAt(0);
            return lastElement;
        }
    }
}
