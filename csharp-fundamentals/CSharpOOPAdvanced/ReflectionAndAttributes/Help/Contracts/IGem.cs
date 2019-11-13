using InfernoInfinity.Models.OtherClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfernoInfinity.Contracts
{
    public interface IGem 
    {
        int Strenght { get; }
        int Agility { get; }
        int Vitality { get;  }
    }
}
