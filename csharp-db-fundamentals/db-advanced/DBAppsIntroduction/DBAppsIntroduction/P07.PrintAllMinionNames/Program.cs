using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DBAppsIntroduction
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> names = new List<string>();
            using (SqlConnection dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();
                string minionNames = @"SELECT Name FROM Minions";
                using (SqlCommand command = new SqlCommand(minionNames, dbCon))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            names.Add(reader["Name"].ToString());
                        }
                    }
                }
            }

            int last = names.Count-1;
            for (int i = 0; i < names.Count / 2; i++)
            {
                Console.WriteLine(names[i]);
                Console.WriteLine(names[last - i]);
            }
            if (names.Count % 2 == 1)
            {
                Console.WriteLine(names[names.Count / 2]);
            }

        }
    }
}
