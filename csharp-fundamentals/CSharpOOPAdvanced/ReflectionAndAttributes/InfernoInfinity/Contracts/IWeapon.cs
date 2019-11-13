using InfernoInfinity.Data;
using InfernoInfinity.Models.Gems;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfernoInfinity.Contracts
{
    public interface IWeapon
    {
        string Name { get; }
        int SocketCount { get; }
        int GetStrength();
        int GetAgility();
        int GetVitality();
        int GetMinDamage();
        int GetMaxDamage();
        void AddGem(int index, Gem gem);
        void RemoveGem(int index);
    }
}
