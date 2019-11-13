using System.Collections.Generic;
using System.Linq;

namespace P05_GreedyTimes
{
    public class Methods
    {
        public static string GetType(string item)
        {
            string result = string.Empty;

            if (item.Length == 3)
            {
                result = "Cash";
            }
            else if (item.ToLower().EndsWith("gem"))
            {
                result = "Gem";
            }
            else if (item.ToLower() == "gold")
            {
                result = "Gold";
            }

            return result;
        }

        public static bool CheckCase(string type, Dictionary<string, Dictionary<string, long>> bag, long count)
        {
            bool result = true;
            switch (type)
            {
                case "Gem":
                    if (!bag.ContainsKey(type))
                    {
                        if (bag.ContainsKey("Gold"))
                        {
                            if (count > bag["Gold"].Values.Sum())
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else if (bag[type].Values.Sum() + count > bag["Gold"].Values.Sum())
                    {
                        result = false;
                    }
                    break;
                case "Cash":
                    if (!bag.ContainsKey(type))
                    {
                        if (bag.ContainsKey("Gem"))
                        {
                            if (count > bag["Gem"].Values.Sum())
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else if (bag[type].Values.Sum() + count > bag["Gem"].Values.Sum())
                    {
                        result = false;
                    }
                    break;
            }

            return result;
        }
    }
}
