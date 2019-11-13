using DBAppsIntroduction;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace P08.IncreaseMinionAge
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] ids = Console.ReadLine().Split().Select(int.Parse).ToArray();

            using (SqlConnection dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();

                string statement = @"UPDATE Minions
                                        SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                                     WHERE Id = @Id";
                foreach (var id in ids)
                {
                    using (SqlCommand command = new SqlCommand(statement, dbCon))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }

                string selectStatement = @"SELECT Name, Age FROM Minions";

                using (SqlCommand command = new SqlCommand(selectStatement, dbCon))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Name"].ToString()} {(int)reader["Age"]}");
                        }
                    }
                }
            }
        }
    }
}
