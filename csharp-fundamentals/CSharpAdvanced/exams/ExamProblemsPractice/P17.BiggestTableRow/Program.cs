using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace P17.BiggestTableRow
{
    class Program
    {
        static void Main(string[] args)
        {

            double maxSum = double.MinValue;
            string[] stores = new string[] { "", "", "" };

            string pattern = @"(?<=<td>)(-?[0-9.]*)(?=<\/td>)";
            string input;
            while ((input = Console.ReadLine()) != "</table>")
            {
                MatchCollection matches = Regex.Matches(input, pattern);

                if (matches.Count == 3)
                {
                    double currentMaxSum = double.MinValue;
                    foreach (Match number in matches)
                    {
                        if (number.ToString() != "-")
                        {
                            if (currentMaxSum == double.MinValue)
                            {
                                currentMaxSum = 0;
                            }
                            currentMaxSum += double.Parse(number.ToString());
                        }
                    }
                    if (currentMaxSum > maxSum)
                    {
                        maxSum = currentMaxSum;
                        stores[0] = matches[0].ToString();
                        stores[1] = matches[1].ToString();
                        stores[2] = matches[2].ToString();
                    }
                }
            }

            if (stores.Where(x => x == "-" || x == "").Count() == 3)
            {
                Console.WriteLine("no data");
            }
            else
            {
                Console.Write("{0} = ", maxSum);
                Console.WriteLine(String.Join(" + ", stores.Where(x => x != "-")));
            }
        }
    }
}
