namespace TupleExercise
{
    public class MyTuple<T, Y>
    {
        private T item1;
        private Y item2;

        public T Item1 { get; private set; }
        public Y Item2 { get; private set; }

        public MyTuple(T item1, Y item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public override string ToString()
        {
            return $"{this.item1} -> {this.item2}";
        }
    }
}
