using Heroes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command
{
    class AttackCommand : ICommand
    {
        public AttackCommand(IAttacker attack)
        {

        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
