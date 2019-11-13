using System;
using System.Collections.Generic;
using System.Linq;

namespace P04_Hospital
{
    public class StartUp
    {
        public static void Main()
        {
            var doctors = new Dictionary<string, List<string>>();
            var departments = new Dictionary<string, List<string>>();

            string command;
            while ((command = Console.ReadLine()) != "Output")
            {
                string[] tokens = command.Split();
                var department = tokens[0];
                var firstName = tokens[1];
                var surname = tokens[2];
                var patient = tokens[3];
                var fullName = firstName + surname;

                if (!doctors.ContainsKey(fullName))
                {
                    doctors.Add(fullName, new List<string>());
                }
                doctors[fullName].Add(patient);

                if (!departments.ContainsKey(department))
                {
                    departments.Add(department, new List<string>());
                }

                departments[department].Add(patient);
            }

            while ((command = Console.ReadLine()) != "End")
            {
                string[] args = command.Split();

                if (args.Length == 1)
                {
                    Console.WriteLine(string.Join("\n", departments[args[0]]));
                }
                else if (args.Length == 2 && int.TryParse(args[1], out int room))
                {
                    PrintRoom(departments, args[0], room);
                }
                else
                {
                    Console.WriteLine(string.Join("\n", doctors[args[0] + args[1]].OrderBy(x => x)));
                }
            }
        }

        public static void PrintRoom(Dictionary<string, List<string>> departments, string department, int room)
        {
            if (room >= 1 && room <= 20)
            {
                int startIndex = (room * 3) - 3;
                int patientCount = departments[department].Count;
                if (startIndex >= 0 && startIndex < patientCount)
                {
                    int endIndex =
                        room * 3 >= patientCount ?
                        patientCount : room * 3;
                    List<string> patients = new List<string>();
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        patients.Add(departments[department][i]);
                    }
                    Console.WriteLine(string.Join("\n", patients.OrderBy(x => x)));
                }
            }
        }
    }
}
