using System;
using System.Linq;
using System.Text;

namespace StorageMaster.Logic
{
    public class Engine
    {
        private StorageMaster sm;

        public Engine()
        {
            this.sm = new StorageMaster();
        }

        public void Run()
        {
            string input;
            StringBuilder sb = new StringBuilder();
            while ((input = System.Console.ReadLine()) != "END")
            {
                string[] args = input.Split();
                string command = args[0];
                string arg1 = args[1];
                string result;
                try
                {
                    switch (command)
                    {
                        case "AddProduct":
                            result = this.sm.AddProduct(arg1, double.Parse(args[2]));
                            break;
                        case "RegisterStorage":
                            result = this.sm.RegisterStorage(arg1, args[2]);
                            break;
                        case "SelectVehicle":
                            result = this.sm.SelectVehicle(arg1, int.Parse(args[2]));
                            break;
                        case "LoadVehicle":
                            result = this.sm.LoadVehicle(args.Skip(1));
                            break;
                        case "SendVehicleTo":
                            result = this.sm.SendVehicleTo(arg1, int.Parse(args[2]), args[3]);
                            break;
                        case "UnloadVehicle":
                            result = this.sm.UnloadVehicle(arg1, int.Parse(args[2]));
                            break;
                        case "GetStorageStatus":
                            result = this.sm.GetStorageStatus(arg1);
                            break;
                        default:
                            throw new ArgumentException("Invalid command!");
                    }
                    sb.AppendLine(result);
                }
                catch (InvalidOperationException ioe)
                {
                    sb.AppendLine($"Error: {ioe.Message}");
                }
            }
            sb.AppendLine(this.sm.GetSummary());
            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}
