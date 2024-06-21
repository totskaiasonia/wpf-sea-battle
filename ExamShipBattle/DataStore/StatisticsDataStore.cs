
using ExamShipBattle.Model;
using System;
using System.Data.SqlClient;

namespace ExamShipBattle.DataStore
{
    internal class StatisticsDataStore : IDataStore<Statistics>
    {

        public string StrConn { get; set; }

        public StatisticsDataStore()
        {
            this.StrConn = "Server=localhost\\SQLEXPRESS; Database=statistics_db; Trusted_Connection=True; TrustServerCertificate=True;";
        }

        public bool AddItem(Statistics item)
        {
            return true;
        }
        public bool UpdateItem(Statistics item)
        {
            string commandWins = $"UPDATE [statistics] SET [wins] = {item.Wins} WHERE [id] = {item.ID};";
            string commandLosses = $"UPDATE [statistics] SET [losses] = {item.Losses} WHERE [id] = {item.ID};";
            using (SqlConnection conn = new SqlConnection(this.StrConn))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("DB connected");

                    SqlCommand comm = new SqlCommand(commandWins, conn);
                    if (comm.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine("Updated data");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update data");
                    }
                    comm = new SqlCommand(commandLosses, conn);
                    if (comm.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine("Updated data");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update data");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }

            }
            return true;
        }
        public string[] GetField(int id)
        {
            string[] res = new string[2];
            string command = $"SELECT * FROM [statistics] WHERE id = {id};";

            using (SqlConnection conn = new SqlConnection(this.StrConn))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("DB Connected");

                    SqlCommand comm = new SqlCommand(command, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        res[0] = $"{reader[1]}";
                        res[1] = $"{reader[2]}";
                        Console.WriteLine($"{reader[1]}, {reader[2]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
            return res;
        }
    }
}
