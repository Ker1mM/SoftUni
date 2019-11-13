using System;
using System.Collections.Generic;
using System.Text;

namespace Skeleton.Interfaces
{
    public interface IWeapon
    {
        int AttackPoints { get; }
        int DurabilityPoints { get; }
        void Attack(ITarget target);
    }
}
