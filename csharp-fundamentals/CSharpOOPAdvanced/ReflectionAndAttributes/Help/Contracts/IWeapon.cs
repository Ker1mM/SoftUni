using InfernoInfinity.Enums;
using InfernoInfinity.Models.OtherClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfernoInfinity.Contracts
{
    public interface IWeapon
    {
        string Name { get; }
        IWeaponState WeaponState { get; }
        Rarities Rarities { get; }
        MagicalState MagicalStats { get; }
        void AddingGem(int socketIndex,string gemType);
        void RemovingGem(int socketIndex);
        string Print();
    }
}
