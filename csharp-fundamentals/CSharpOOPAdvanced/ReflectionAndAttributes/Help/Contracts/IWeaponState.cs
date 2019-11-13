using System;
using System.Collections.Generic;
using System.Text;

namespace InfernoInfinity.Contracts
{
    public interface IWeaponState
    {
         int MinDamage { get; }
        int MaxDamage { get;  }
        IGem[] NumberOfSocket { get;  }
        void AddGem(IGem gem, int socketIndex);

    }
}
