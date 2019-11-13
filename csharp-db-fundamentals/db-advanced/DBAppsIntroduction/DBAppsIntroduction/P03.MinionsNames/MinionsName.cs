using DBAppsIntroduction;
using System;
using System.Data.SqlClient;
using System.Text;

namespace DBAppsIndtroduction
{
    class MinionsName
    {
        static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());
            bool villainExists = false;
            using (SqlConnection dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();

                SqlCommand command = new SqlCommand(@"SELECT Name FROM Villains WHERE Id = @Id", dbCon);
                command.Parameters.AddWithValue("@Id", villainId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Villain: {0}", reader["Name"].ToString());
                        villainExists = true;
                    }
                    else
                    {
                        Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                    }
                }

                if (villainExists)
                {
                    SqlCommand getMinions = new SqlCommand(@"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name,
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name", dbCon);

                    getMinions.Parameters.AddWithValue("@Id", villainId);
                    StringBuilder sb = new StringBuilder();
                    using (SqlDataReader reader = getMinions.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long rowNumber = (long)reader[0];
                            string name = reader["Name"].ToString();
                            int age = (int)reader["Age"];
                            sb.AppendLine(rowNumber + ". " + name + " " + age);
                        }
                    }

                    if (sb.Length == 0) //Test with ID = 7
                    {
                        sb.AppendLine("(No Minions)");
                    }

                    Console.Write(sb.ToString());
                }
            }
        }
    }
}
