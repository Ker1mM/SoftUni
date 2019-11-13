using System;
using System.Numerics;

namespace ParticlesSelector
{
    class Program
    {
        static void Main(string[] args)
        {
            int particleCount = int.Parse(Console.ReadLine());
            int selectedParticlesCount = int.Parse(Console.ReadLine());

            if (particleCount == selectedParticlesCount)
            {
                Console.WriteLine(1);
            }
            else
            {
                BigInteger num = GetFactorial(selectedParticlesCount + 1, particleCount);
                BigInteger den = GetFactorial(1, particleCount - selectedParticlesCount);
                BigInteger result = num / den;
                Console.WriteLine(result);
            }
        }

        private static BigInteger GetFactorial(int start, int end)
        {

            if (start == end)
            {
                return start;
            }
            else
            {
                return end * GetFactorial(start, end - 1);
            }
        }
    }
}
