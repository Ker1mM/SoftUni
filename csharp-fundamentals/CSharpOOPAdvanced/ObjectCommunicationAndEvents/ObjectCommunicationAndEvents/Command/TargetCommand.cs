using Heroes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command
{
    public class TargetCommand : ICommand
    {
        public TargetCommand(IAttacker attacker, ITarget target)
        {

        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
