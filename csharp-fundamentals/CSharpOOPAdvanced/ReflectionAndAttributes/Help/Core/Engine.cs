using InfernoInfinity.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfernoInfinity.Core
{
    public class Engine
    {
        public Engine()
        {
            this.infinityInferno = new InfinityInferno();
        }
        private IInfinityInferno infinityInferno;

        public void Act()
        {
            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                string[] args = input.Split(";");
                string command = args[0];

                Type type = typeof(InfinityInferno);

                var method = type.GetMethods().FirstOrDefault(x => x.Name == command);
                if (method != null)
                {

                    method.Invoke(this.infinityInferno, new object[] { args });

                }


            }
        }
    }
}
