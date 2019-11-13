using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackMessup
{
    class Program
    {
        static Dictionary<string, Atom> atoms;
        static Dictionary<string, List<string>> graph;
        static List<SortedSet<Atom>> molecules;

        static void Main(string[] args)
        {
            int atomCount = int.Parse(Console.ReadLine());
            int connectionCount = int.Parse(Console.ReadLine());

            atoms = new Dictionary<string, Atom>();
            graph = new Dictionary<string, List<string>>();

            for (int i = 0; i < atomCount; i++)
            {
                var inputArgs = Console.ReadLine().Split().ToArray();

                var atom = new Atom(inputArgs[0], int.Parse(inputArgs[1]), int.Parse(inputArgs[2]));
                atoms.Add(inputArgs[0], atom);
                graph.Add(inputArgs[0], new List<string>());
            }

            for (int i = 0; i < connectionCount; i++)
            {
                var inputArgs = Console.ReadLine().Split().ToArray();

                graph[inputArgs[0]].Add(inputArgs[1]);
                graph[inputArgs[1]].Add(inputArgs[0]);
            }

            GetAllMolecules();

            int maxMass = 0;
            foreach (var molecule in molecules)
            {
                int mass = GetMaxMass(molecule);
                if (mass > maxMass)
                {
                    maxMass = mass;
                }
            }

            Console.WriteLine(maxMass);
        }

        private static int GetMaxMass(SortedSet<Atom> molecule)
        {
            int mass = 0;
            int maxDecay = 1;
            int takenCount = 0;

            foreach (var atom in molecule)
            {
                if (atom.Decay > maxDecay)
                {
                    takenCount++;
                    mass += atom.Mass;
                    maxDecay = atom.Decay;
                }
                else if (maxDecay > takenCount)
                {
                    takenCount++;
                    mass += atom.Mass;
                }
            }

            return mass;
        }
        private static void GetAllMolecules()
        {
            HashSet<string> visited = new HashSet<string>();
            molecules = new List<SortedSet<Atom>>();

            foreach (var atom in atoms)
            {
                if (!visited.Contains(atom.Key))
                {
                    var molecule = new SortedSet<Atom>();
                    GetMolecule(atom.Key, molecule, visited);
                    molecules.Add(molecule);
                }

            }
        }

        private static void GetMolecule(string atom, SortedSet<Atom> component, HashSet<string> visited)
        {
            component.Add(atoms[atom]);
            visited.Add(atom);

            foreach (var childAtom in graph[atom])
            {
                if (!visited.Contains(childAtom))
                {
                    GetMolecule(childAtom, component, visited);
                }
            }
        }
    }

    class Atom : IComparable<Atom>
    {
        public string Name { get; set; }
        public int Mass { get; set; }
        public int Decay { get; set; }

        public Atom(string name, int mass, int decay)
        {
            this.Name = name;
            this.Mass = mass;
            this.Decay = decay;
        }

        public int CompareTo(Atom other)
        {
            return -this.Mass.CompareTo(other.Mass);
        }
    }


}
