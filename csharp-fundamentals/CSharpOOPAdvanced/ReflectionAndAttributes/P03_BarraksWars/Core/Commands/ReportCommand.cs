using _03BarracksFactory.Contracts;
using _03BarracksFactory.Core.Commands;

namespace P03_BarraksWars.Core.Commands
{
    public class ReportCommand : Command
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

        public ReportCommand(string[] data) : base(data)
        {

        }

        public override string Execute()
        {
            string output = this.Repository.Statistics;
            return output;
        }
    }
}
