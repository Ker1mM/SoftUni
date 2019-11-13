using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTrainer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var trainers = new Dictionary<string, Trainer>();
            string input;
            while ((input = Console.ReadLine()) != "Tournament")
            {
                string[] tokens =
                    input
                    .Trim()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string trainerName = tokens[0];
                string pokemonName = tokens[1];
                string element = tokens[2];
                int health = int.Parse(tokens[3]);

                Pokemon current = new Pokemon(pokemonName, element, health);
                if (!trainers.ContainsKey(trainerName))
                {
                    trainers.Add(trainerName, new Trainer(trainerName));
                }

                trainers[trainerName].AddPokemon(current);
            }

            while ((input = Console.ReadLine()) != "End")
            {
                foreach (var trainer in trainers)
                {
                    trainer.Value.Fight(input);
                }
            }

            trainers =
                trainers
                .OrderByDescending(x => x.Value.BadgesCount)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var trainer in trainers)
            {
                Console.WriteLine(trainer.Value.ToString());
            }
        }
    }
}
