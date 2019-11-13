using BillsPaymentSystem.App.Core;
using BillsPaymentSystem.Data;
using System;

namespace BillsPaymentSystem.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var commandInterpreter = new CommandInterpreter();
            var engine = new Engine(commandInterpreter);
            engine.Run();

            //using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
            //{
            //    DbInitializer.Seed(context);
            //}
        }
    }
}
