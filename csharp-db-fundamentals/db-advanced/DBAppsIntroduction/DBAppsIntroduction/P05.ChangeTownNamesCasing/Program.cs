using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DBAppsIntroduction
{
    class Program
    {
        static void Main(string[] args)
        {
            string countryName = Console.ReadLine();

            List<string> townNames = new List<string>();
            using (SqlConnection dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();
                string changeToUpper = @"UPDATE Towns
                                         SET Name = UPPER(Name)
                                         WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";
                using (SqlCommand command = new SqlCommand(changeToUpper, dbCon))
                {
                    command.Parameters.AddWithValue("@countryName", countryName);
                    command.ExecuteNonQuery();
                }

                string getAffectedNames = @"SELECT t.Name 
                                            FROM Towns as t
                                            JOIN Countries AS c ON c.Id = t.CountryCode
                                            WHERE c.Name = @countryName";

                using (SqlCommand command = new SqlCommand(getAffectedNames, dbCon))
                {
                    command.Parameters.AddWithValue("@countryName", countryName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            townNames.Add(reader["Name"].ToString());
                        }
                    }
                }
            }

            if (townNames.Count == 0)
            {
                Console.WriteLine("No town names were affected.");
            }
            else
            {
                Console.WriteLine($"{townNames.Count} town names were affected.");
                Console.WriteLine("[{0}]", string.Join(", ", townNames));
            }
        }
    }
}
