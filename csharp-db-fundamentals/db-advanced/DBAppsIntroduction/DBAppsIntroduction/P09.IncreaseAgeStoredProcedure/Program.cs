using DBAppsIntroduction;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace P09.IncreaseAgeStoredProcedure
{
    class Program
    {
        static void Main(string[] args)
        {
            int minionId = int.Parse(Console.ReadLine());

            using (SqlConnection dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();

                string statement = @"EXEC usp_GetOlder @id";
                using (SqlCommand command = new SqlCommand(statement, dbCon))
                {
                    command.Parameters.AddWithValue("@Id", minionId);
                    command.ExecuteNonQuery();
                }

                string selectStatement = @"SELECT Name, Age FROM Minions WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(selectStatement, dbCon))
                {
                    command.Parameters.AddWithValue("@Id", minionId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"{reader["Name"].ToString()} {(int)reader["Age"]}");
                        }
                    }
                }
            }
        }
    }
}
