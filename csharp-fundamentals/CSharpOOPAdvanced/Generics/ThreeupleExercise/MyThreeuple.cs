using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeupleExercise
{
    public class MyThreeuple<X, Y, Z>
    {
        private X item1;
        private Y item2;
        private Z iteam3;

        public X Item1 { get; private set; }
        public Y Item2 { get; private set; }
        public Z Item3 { get; private set; }

        public MyThreeuple(X item1, Y item2, Z item3)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }

        public override string ToString()
        {
            return $"{this.Item1} -> {this.Item2} -> {this.Item3}";
        }
    }
}
