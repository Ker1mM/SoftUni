using System;

namespace P3_Mankind
{
    public class StartUp
    {
        static void Main()
        {
            try
            {
                string input = Console.ReadLine();
                string[] args = input.Trim().Split();

                string firstName = args[0];
                string lastName = args[1];
                string facultyNumber = args[2];
                Student student = new Student(firstName, lastName, facultyNumber);

                input = Console.ReadLine();
                args = input.Trim().Split();
                firstName = args[0];
                lastName = args[1];
                decimal salary = decimal.Parse(args[2]);
                decimal workHours = decimal.Parse(args[3]);
                Worker worker = new Worker(firstName, lastName, salary, workHours);

                Console.WriteLine(student);
                Console.WriteLine();
                Console.WriteLine(worker);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }
    }
}
