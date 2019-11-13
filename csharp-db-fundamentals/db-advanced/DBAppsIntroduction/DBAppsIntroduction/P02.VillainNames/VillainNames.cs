using System;
using System.Data.SqlClient;

namespace DBAppsIntroduction
{
    class VillainNames
    {
        static void Main(string[] args)
        {
            using (SqlConnection dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();

                SqlCommand command = new SqlCommand(@"  SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                                    FROM Villains AS v 
                                                    JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                                                    GROUP BY v.Id, v.Name 
                                                    HAVING COUNT(mv.VillainId) > 3 
                                                    ORDER BY COUNT(mv.VillainId)", dbCon);

                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        string villainName = (string)reader["Name"];
                        int minionCount = (int)reader["MinionsCount"];

                        Console.WriteLine("{0} - {1}", villainName, minionCount);
                    }
                }
            }
        }
    }
}
