using DBAppsIntroduction;
using System;
using System.Data.SqlClient;

namespace DBAppsIndtroduction
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] minionInfo = Console.ReadLine().Split();
            string minionName = minionInfo[1];
            int minionAge = int.Parse(minionInfo[2]);
            string minionTown = minionInfo[3];
            string villainName = Console.ReadLine().Split()[1];

            using (SqlConnection dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();

                int townId;
                string townStatement = @"SELECT Id FROM Towns WHERE Name = @Name";
                using (SqlCommand townCommand = new SqlCommand(townStatement, dbCon))
                {
                    townCommand.Parameters.AddWithValue("@Name", minionTown);
                    townId = GetId(dbCon, townCommand);
                    if (townId == -1)
                    {
                        string insertTown = @"INSERT INTO Towns (Name) VALUES (@townName)";
                        using (SqlCommand insertTownCommand = new SqlCommand(insertTown, dbCon))
                        {
                            insertTownCommand.Parameters.AddWithValue("@townName", minionTown);

                            insertTownCommand.ExecuteNonQuery();
                            Console.WriteLine($"Town {minionTown} was added to the database.");
                        }
                        townId = GetId(dbCon, townCommand);
                    }
                }

                int villainId;
                string villainStatement = @"SELECT Id FROM Villains WHERE Name = @Name";
                using (SqlCommand villainCommand = new SqlCommand(villainStatement, dbCon))
                {
                    villainCommand.Parameters.AddWithValue("@Name", villainName);
                    villainId = GetId(dbCon, villainCommand);
                    if (villainId == -1)
                    {
                        string insertVillain = @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
                        using (SqlCommand insertTownCommand = new SqlCommand(insertVillain, dbCon))
                        {
                            insertTownCommand.Parameters.AddWithValue("@villainName", villainName);

                            insertTownCommand.ExecuteNonQuery();
                            Console.WriteLine($"Villain {villainName} was added to the database.");
                        }
                        villainId = GetId(dbCon, villainCommand);
                    }
                }

                int minionId;
                string minionStatement = @"SELECT Id FROM Minions WHERE Name = @Name";
                using (SqlCommand minionCommand = new SqlCommand(minionStatement, dbCon))
                {
                    minionCommand.Parameters.AddWithValue("@Name", minionName);
                    minionId = GetId(dbCon, minionCommand);
                    if (minionId == -1)
                    {
                        string insertMinion = @"INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";
                        using (SqlCommand insertMinionCommand = new SqlCommand(insertMinion, dbCon))
                        {
                            insertMinionCommand.Parameters.AddWithValue("@name", minionName);
                            insertMinionCommand.Parameters.AddWithValue("@age", minionAge);
                            insertMinionCommand.Parameters.AddWithValue("@townId", townId);

                            insertMinionCommand.ExecuteNonQuery();
                        }
                        minionId = GetId(dbCon, minionCommand);
                    }
                }

                string minionsVillainsStatement = @"SELECT * FROM MinionsVillains WHERE MinionId = @minionId AND VillainId = @villainId";
                using (SqlCommand minionsVillainsCommand = new SqlCommand(minionsVillainsStatement, dbCon))
                {
                    minionsVillainsCommand.Parameters.AddWithValue("@minionId", minionId);
                    minionsVillainsCommand.Parameters.AddWithValue("@villainId", villainId);
                    if (GetId(dbCon, minionsVillainsCommand) == -1)
                    {
                        string insertMinionsVillains = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";
                        using (SqlCommand insertMinionsVillainsCommand = new SqlCommand(insertMinionsVillains, dbCon))
                        {
                            insertMinionsVillainsCommand.Parameters.AddWithValue("@minionId", minionId);
                            insertMinionsVillainsCommand.Parameters.AddWithValue("@villainId", villainId);

                            insertMinionsVillainsCommand.ExecuteNonQuery();
                        }
                    }

                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                }
            }
        }

        private static int GetId(SqlConnection dbCon, SqlCommand command)
        {
            int result = -1;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = (int)reader[0];
                }
            }

            return result;
        }
    }
}
