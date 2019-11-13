
using System;
using System.Linq;
namespace FestivalManager.Core
{
    using System.Reflection;
    using Contracts;
    using Controllers;
    using Controllers.Contracts;
    using FestivalManager.Core.IO;
    using FestivalManager.Entities;
    using FestivalManager.Entities.Contracts;
    using IO.Contracts;

    /// <summary>
    /// by g0shk0
    /// </summary>
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly IFestivalController festivalController;
        private readonly ISetController setController;

        public Engine(IFestivalController festivalController, ISetController setController)
        {
            this.reader = new Reader();
            this.writer = new Writer();
            this.festivalController = festivalController;
            this.setController = setController;
        }

        // дайгаз
        public void Run()
        {
            while (true) // for job security
            {
                var input = reader.ReadLine();

                if (input == "END")
                    break;

                try
                {
                    string.Intern(input);

                    var result = this.ProcessCommand(input);
                    this.writer.WriteLine(result);
                }
                catch (Exception ex) // in case we run out of memory
                {
                    this.writer.WriteLine("ERROR: " + ex.Message);
                }
            }

            var end = this.festivalController.ProduceReport();

            this.writer.WriteLine("Results:");
            this.writer.WriteLine(end);
        }

        public string ProcessCommand(string input)
        {
            var args = input.Split(" ".ToCharArray().First());

            var command = args.First();
            var parameters = args.Skip(1).ToArray();

            if (command == "LetsRock")
            {
                var sets = this.setController.PerformSets();
                return sets;
            }

            var festivalcontrolfunction = this.festivalController.GetType()
                .GetMethods()
                .FirstOrDefault(x => x.Name == command);

            string result;

            try
            {
                result = (string)festivalcontrolfunction.Invoke(this.festivalController, new object[] { parameters });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }

            return result;
        }
    }
}