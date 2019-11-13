using System;

namespace P18.PINValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine();
            string gender = Console.ReadLine();
            string EGN = Console.ReadLine();

            if (CheckName(name) && CheckEGN(EGN, gender))
            {
                Console.WriteLine("{{\"name\":\"{0}\",\"gender\":\"{1}\",\"pin\":\"{2}\"}}",
                    name, gender, EGN);
            }
            else
            {
                Console.WriteLine("<h2>Incorrect data</h2>");
            }
        }

        public static bool CheckEGN(string egn, string gender)
        {
            bool result = true;

            if (egn.Length != 10)
            {
                result = false;
            }
            else
            {
                result = Checksum(egn) && CheckCityAndGender(egn.Substring(6, 3), gender) && CheckBirthYear(egn.Substring(0, 6));
            }
            return result;
        }

        public static bool Checksum(string egn)
        {
            int totalSum = 0;
            totalSum += int.Parse(egn.Substring(0, 1)) * 2 +
                int.Parse(egn.Substring(1, 1)) * 4 +
                int.Parse(egn.Substring(2, 1)) * 8 +
                int.Parse(egn.Substring(3, 1)) * 5 +
                int.Parse(egn.Substring(4, 1)) * 10 +
                int.Parse(egn.Substring(5, 1)) * 9 +
                int.Parse(egn.Substring(6, 1)) * 7 +
                int.Parse(egn.Substring(7, 1)) * 3 +
                int.Parse(egn.Substring(8, 1)) * 6;

            int remainder = totalSum % 11;
            remainder = remainder == 10 ? 0 : remainder;

            return int.Parse(egn.Substring(9, 1)) == remainder;

        }

        public static bool CheckCityAndGender(string codeString, string gender)
        {
            bool result = true;
            int code = int.Parse(codeString);

            if (code < 43)
            {
                result = false;
            }
            else
            {
                int genderCode = code % 10;

                if (genderCode % 2 == 0)
                {
                    result = gender == "male";
                }
                else
                {
                    result = gender == "female";
                }
            }
            return result;
        }

        public static bool CheckBirthYear(string birthday)
        {
            bool result = true;
            int month = int.Parse(birthday.Substring(2, 2));
            int day = int.Parse(birthday.Substring(4, 2));
            if (month < 0 || month > 12 && month <= 20 || month > 32 && month <= 40 || month > 52)
            {
                result = false;
            }
            else if (day < 1 || day > 31)
            {
                result = false;
            }

            return result;
        }

        public static bool CheckName(string name)
        {
            bool result = true;

            string[] tokens = name.Split();
            if (tokens.Length != 2)
            {
                result = false;
            }
            else
            {
                if (!char.IsUpper(tokens[0][0]) || !char.IsUpper(tokens[1][0]))
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
