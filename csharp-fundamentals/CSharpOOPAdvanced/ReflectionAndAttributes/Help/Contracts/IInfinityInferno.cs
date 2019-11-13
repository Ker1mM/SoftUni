using System;
using System.Collections.Generic;
using System.Text;

namespace InfernoInfinity.Contracts
{
    public interface IInfinityInferno
    {
        IReadOnlyCollection<IWeapon> Weapons { get; }
        void Create(string[] args);
        void Add(string[] args);
        void Remove(string[] args);
        void Print(string[] args);
    }
}
