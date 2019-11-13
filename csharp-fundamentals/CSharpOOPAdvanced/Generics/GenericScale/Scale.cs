using System;

namespace GenericScale
{
    public class Scale<T> where T : IComparable<T>
    {
        private T leftItem;
        private T rightItem;

        public Scale(T left, T right)
        {
            this.leftItem = left;
            this.rightItem = right;
        }

        public T GetHeavier()
        {
            int result = leftItem.CompareTo(rightItem);
            if (result > 0)
            {
                return leftItem;
            }
            else if (result < 0)
            {
                return rightItem;
            }
            else
            {
                return default(T);
            }
        }
    }
}
