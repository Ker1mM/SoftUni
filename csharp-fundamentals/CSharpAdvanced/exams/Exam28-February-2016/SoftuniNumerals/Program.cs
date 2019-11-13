using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SoftuniNumerals
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            StringBuilder sb = new StringBuilder();
            string numeral = "";
            for (int i = 0; i < input.Length; i++)
            {
                sb.Append(input[i]);
                if (sb.ToString() == "aa")
                {
                    numeral += "0";
                    sb.Clear();
                }
                else if ((sb.ToString() == "cc"))
                {
                    numeral += "3";
                    sb.Clear();
                }
                else if (sb.ToString() == "aba")
                {
                    numeral += "1";
                    sb.Clear();
                }
                else if (sb.ToString() == "bcc")
                {
                    numeral += "2";
                    sb.Clear();
                }
                else if (sb.ToString() == "cdc")
                {
                    numeral += "4";
                    sb.Clear();
                }
            }

            Console.WriteLine(Convertor(numeral));
        }

        public static BigInteger Convertor(string numeral)
        {
            BigInteger result = 0;
            BigInteger number = BigInteger.Parse(numeral);
            int counter = 0;
            while (number > 0)
            {
                result += (number % 10) * BigInteger.Pow(5, counter);
                counter++;
                number /= 10;
            }
            return result;
        }
    }
}
