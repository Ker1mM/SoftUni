using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hospital
{
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<string, List<string>> departments = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> doctors = new Dictionary<string, List<string>>();
            string input;
            while ((input = Console.ReadLine()) != "Output")
            {
                string[] tokens = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string currentDepartment = tokens[0];
                string currentDoctor = tokens[1] + " " + tokens[2];
                string currentPatient = tokens[3];

                if (!departments.ContainsKey(currentDepartment))
                {
                    departments.Add(currentDepartment, new List<string>());
                }
                if (!doctors.ContainsKey(currentDoctor))
                {
                    doctors.Add(currentDoctor, new List<string>());
                }
                departments[currentDepartment].Add(currentPatient);
                doctors[currentDoctor].Add(currentPatient);

            }

            while ((input = Console.ReadLine().Trim()) != "End")
            {
                string[] tokens = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 2)
                {
                    if (int.TryParse(tokens[1], out int room))
                    {
                        if (departments.ContainsKey(tokens[0]) && room <= 20)
                        {
                            var patiens = departments[tokens[0]];
                            var patientsInRoom = new List<string>();
                            for (int i = (room * 3) - 3; i < room * 3; i++)
                            {
                                if (i < patiens.Count)
                                {
                                    patientsInRoom.Add(patiens[i]);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            Console.WriteLine(String.Join("\n", patientsInRoom.OrderBy(x => x)));
                        }
                    }
                    else
                    {
                        if (doctors.ContainsKey(input))
                        {
                            foreach (var patient in doctors[input].OrderBy(x => x))
                            {
                                Console.WriteLine(patient);
                            }
                        }
                    }
                }
                else if (tokens.Length == 1 && departments.ContainsKey(input))
                {
                    foreach (var patient in departments[input])
                    {
                        Console.WriteLine(patient);
                    }
                }
            }

        }
    }
}
