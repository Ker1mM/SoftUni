using System;

class StartUp
{
    static void Main()
    {
        string[] inputArgs = Console.ReadLine().Split();
        Car car = new Car(double.Parse(inputArgs[1]), double.Parse(inputArgs[2]), double.Parse(inputArgs[3]));
        inputArgs = Console.ReadLine().Split();
        Truck truck = new Truck(double.Parse(inputArgs[1]), double.Parse(inputArgs[2]), double.Parse(inputArgs[3]));
        inputArgs = Console.ReadLine().Split();
        Bus bus = new Bus(double.Parse(inputArgs[1]), double.Parse(inputArgs[2]), double.Parse(inputArgs[3]));

        int commandCount = int.Parse(Console.ReadLine());
        while (commandCount-- > 0)
        {
            try
            {
                inputArgs = Console.ReadLine().Split();
                double argument = double.Parse(inputArgs[2]);
                if (inputArgs[1] == "Car")
                {
                    car.ExecuteCommand(inputArgs[0], argument);
                }
                else if (inputArgs[1] == "Truck")
                {
                    truck.ExecuteCommand(inputArgs[0], argument);
                }
                else if (inputArgs[1] == "Bus")
                {
                    bus.ExecuteCommand(inputArgs[0], argument);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Console.WriteLine(car.ToString());
        Console.WriteLine(truck.ToString());
        Console.WriteLine(bus.ToString());
    }
}


