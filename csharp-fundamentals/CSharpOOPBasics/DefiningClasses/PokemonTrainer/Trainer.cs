using System.Collections.Generic;
using System.Linq;

namespace PokemonTrainer
{
    public class Trainer
    {
        public string Name { get; set; }
        public int BadgesCount { get; set; }
        public List<Pokemon> Pokemons { get; set; }

        public Trainer(string name)
        {
            this.Name = name;
            this.BadgesCount = 0;
            this.Pokemons = new List<Pokemon>();
        }

        public void AddPokemon(Pokemon pokemon)
        {
            this.Pokemons.Add(pokemon);
        }

        public void Fight(string element)
        {
            if (this.Pokemons.Any(x => x.Element == element))
            {
                this.BadgesCount++;
            }
            else
            {
                this.Pokemons =
                    this.Pokemons
                    .Select(x => { x.Health -= 10; return x; })
                    .ToList();

                this.Pokemons =
                    this.Pokemons
                    .Where(x => x.Health > 0)
                    .ToList();
            }
        }

        public override string ToString()
        {
            return $"{this.Name} {this.BadgesCount} {this.Pokemons.Count}";
        }
    }
}
