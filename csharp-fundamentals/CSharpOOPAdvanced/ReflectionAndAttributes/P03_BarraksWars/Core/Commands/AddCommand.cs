using _03BarracksFactory.Contracts;
using _03BarracksFactory.Core.Commands;

namespace P03_BarraksWars.Core.Commands
{
    public class AddCommand : Command
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

        public AddCommand(string[] data) : base(data)
        {

        }

        public override string Execute()
        {
            string unitType = this.Data[1];
            var unitToAdd = this.UnitFactory.CreateUnit(unitType);
            this.Repository.AddUnit(unitToAdd);

            return $"{unitType} added!";
        }
    }
}
