using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NumberWars
{
    class Program
    {
        static void Main(string[] args)
        {
            string line1 = Console.ReadLine();
            string line2 = Console.ReadLine();

            var firstDeck = FillDecks(line1);
            var secondDeck = FillDecks(line2);

            int turns = 0;
            bool over = false;
            while (turns < 1_000_000 && firstDeck.Any() && secondDeck.Any() & !over)
            {
                turns++;

                var p1card = firstDeck.Dequeue();
                var p2card = secondDeck.Dequeue();

                if (p1card.Key > p2card.Key)
                {
                    firstDeck.Enqueue(p1card);
                    firstDeck.Enqueue(p2card);
                }
                else if (p1card.Key < p2card.Key)
                {
                    secondDeck.Enqueue(p2card);
                    secondDeck.Enqueue(p1card);
                }
                else if (p1card.Key == p2card.Key)
                {
                    List<KeyValuePair<int, char>> cards = new List<KeyValuePair<int, char>>();
                    cards.Add(p1card);
                    cards.Add(p2card);

                    while (!over)
                    {
                        if (firstDeck.Count >= 3 && secondDeck.Count >= 3)
                        {

                            int p1Score = 0;
                            int p2Score = 0;

                            for (int i = 0; i < 3; i++)
                            {
                                var current = firstDeck.Dequeue();
                                p1Score += current.Value;
                                cards.Add(current);

                                current = secondDeck.Dequeue();
                                p2Score += current.Value;
                                cards.Add(current);
                            }

                            if (p1Score > p2Score)
                            {
                                FillDeck(firstDeck, cards);
                                break;
                            }
                            else if (p2Score > p1Score)
                            {
                                FillDeck(secondDeck, cards);
                                break;
                            }
                        }
                        else
                        {
                            over = true;
                        }
                    }
                }
            }
            string result = "";

            if (firstDeck.Count < secondDeck.Count)
            {
                result = "Second player wins";
            }
            else if (firstDeck.Count > secondDeck.Count)
            {
                result = "First player wins";
            }
            else
            {
                result = "Draw";
            }
            Console.WriteLine("{0} after {1} turns", result, turns);
        }

        public static void FillDeck(Queue<KeyValuePair<int, char>> deck, List<KeyValuePair<int, char>> cards)
        {
            foreach (var card in cards.OrderByDescending(x => x.Key).ThenByDescending(x => x.Value))
            {
                deck.Enqueue(card);
            }
        }

        public static Queue<KeyValuePair<int, char>> FillDecks(string cards)
        {
            string pattern = @"(\d+)(\w)";
            MatchCollection matches = Regex.Matches(cards, pattern);
            Queue<KeyValuePair<int, char>> result = new Queue<KeyValuePair<int, char>>();
            foreach (Match item in matches)
            {
                result.Enqueue(new KeyValuePair<int, char>(int.Parse(item.Groups[1].ToString()), item.Groups[2].ToString()[0]));
            }
            return result;
        }
    }
}// I hated this ONE!