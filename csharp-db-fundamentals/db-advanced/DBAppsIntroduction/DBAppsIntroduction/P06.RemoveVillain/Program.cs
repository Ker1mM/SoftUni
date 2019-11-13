using System;
using System.Data.SqlClient;

namespace DBAppsIntroduction
{
    class Program
    {
        static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());
            string villainName = "";
            using (SqlConnection dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();
                string nameStatement = @"SELECT Name FROM Villains WHERE Id = @villainId";
                using (SqlCommand command = new SqlCommand(nameStatement, dbCon))
                {
                    command.Parameters.AddWithValue("@villainId", villainId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            villainName = reader["Name"].ToString();
                        }
                    }
                }

                if (villainName == "")
                {
                    Console.WriteLine("No such villain was found.");
                }
                else
                {
                    Console.WriteLine("{0} was deleted.", villainName);
                    int deletedMinionsCount = 0;
                    string deleteMinionsVillain = @"DELETE FROM MinionsVillains 
                                                        WHERE VillainId = @villainId";

                    using (SqlCommand command = new SqlCommand(deleteMinionsVillain, dbCon))
                    {
                        command.Parameters.AddWithValue("@villainId", villainId);
                        deletedMinionsCount = command.ExecuteNonQuery();
                    }

                    string deleteVillain = @"DELETE FROM Villains
                                                WHERE Id = @villainId";

                    using (SqlCommand command = new SqlCommand(deleteVillain, dbCon))
                    {
                        command.Parameters.AddWithValue("@villainId", villainId);
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine($"{deletedMinionsCount} minions were released.");
                }
            }
        }
    }
}
