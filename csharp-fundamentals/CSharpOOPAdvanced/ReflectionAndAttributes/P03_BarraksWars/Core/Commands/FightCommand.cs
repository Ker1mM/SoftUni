using _03BarracksFactory.Contracts;
using _03BarracksFactory.Core.Commands;
using System;

namespace P03_BarraksWars.Core.Commands
{
    public class FightCommand : Command
    {
        [Inject]
        private IRepository repository;

        [Inject]
        private IUnitFactory unitFactory;

        public IUnitFactory UnitFactory
        {
            get { return unitFactory; }
            set { unitFactory = value; }
        }

        public IRepository Repository
        {
            get { return repository; }
            set { repository = value; }
        }

        public FightCommand(string[] data) : base(data)
        {

        }

        public override string Execute()
        {
            Environment.Exit(0);
            return string.Empty;
        }
    }
}
