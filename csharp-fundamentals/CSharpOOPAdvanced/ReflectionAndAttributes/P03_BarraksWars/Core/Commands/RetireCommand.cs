using _03BarracksFactory.Contracts;
using _03BarracksFactory.Core.Commands;

namespace P03_BarraksWars.Core.Commands
{
    public class RetireCommand : Command
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

        public RetireCommand(string[] data) : base(data)
        {

        }

        public override string Execute()
        {
            string unitType = this.Data[1];
            this.Repository.RemoveUnit(unitType);

            string result = $"{unitType} retired!";
            return result;
        }
    }
}
