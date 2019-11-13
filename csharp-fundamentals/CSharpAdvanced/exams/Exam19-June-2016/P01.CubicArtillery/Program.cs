using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace P01.CubicArtillery
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxCapacity = int.Parse(Console.ReadLine());

            Stopwatch timer = new Stopwatch();
            timer.Start();

            Queue<char> bunkers = new Queue<char>();
            Queue<int> bunkerContent = new Queue<int>();


            string input;
            int currenBunkerCap = maxCapacity;
            while ((input = Console.ReadLine()) != "Bunker Revision")
            {
                string[] tokens = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var nextElement in tokens)
                {
                    if (int.TryParse(nextElement, out int weaponCap))
                    {
                        if (bunkers.Count == 1 && maxCapacity >= weaponCap)
                        {
                            if (currenBunkerCap >= weaponCap)
                            {
                                currenBunkerCap -= weaponCap;
                                bunkerContent.Enqueue(weaponCap);
                            }
                            else
                            {
                                while (true)
                                {
                                    currenBunkerCap += bunkerContent.Dequeue();
                                    if (currenBunkerCap >= weaponCap)
                                    {
                                        currenBunkerCap -= weaponCap;
                                        bunkerContent.Enqueue(weaponCap);
                                        break;
                                    }
                                }

                            }
                        }
                        else if (bunkers.Count > 1)
                        {
                            if (currenBunkerCap >= weaponCap)
                            {
                                currenBunkerCap -= weaponCap;
                                bunkerContent.Enqueue(weaponCap);
                            }
                            else
                            {
                                while (currenBunkerCap < weaponCap && bunkers.Count > 1)
                                {
                                    Console.Write("{0} -> ", bunkers.Dequeue());
                                    if (bunkerContent.Count > 0)
                                    {
                                        Console.WriteLine(String.Join(", ", bunkerContent));
                                    }
                                    else
                                    {
                                        Console.WriteLine("Empty");
                                    }
                                    bunkerContent.Clear();
                                    currenBunkerCap = maxCapacity;
                                    if (currenBunkerCap >= weaponCap)
                                    {
                                        currenBunkerCap -= weaponCap;
                                        bunkerContent.Enqueue(weaponCap);
                                        break;
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        bunkers.Enqueue(nextElement[0]);
                    }
                }
            }
        }
    }

}
