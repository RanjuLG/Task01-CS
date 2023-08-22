using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace CSVToMySQLUploader
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvFilePath = "QI01.CSV";
            string connectionString = "server=127.0.0.1;port=3306;user id=root;password=19723231;database=dspcs;AllowLoadLocalInfile=true;";


            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = CommandType.Text;

                            // Load data from CSV into MySQL table
                            command.CommandText = $"LOAD DATA LOCAL INFILE '{csvFilePath}' " +
                                                  "INTO TABLE powerconsumption " +
                                                  "FIELDS TERMINATED BY ',' " +
                                                  "ENCLOSED BY '\"' " +
                                                  "LINES TERMINATED BY '\\n' " +
                                                  "(time, QI_CIT, QI_P14, QI_P11, QI_P12, QI_P15, QI_P17, QI_P21, QI_P21_2, QI_P48, QI_P52, QI_P53, QI_P54, QI_P55, QI_P56, QI_P58, QI_P60_1, QI_P60_2, QI_P60_3, QI_P51, QI_P06, QI_P21_TOT, QI_P03, QI_P50, QI_P18\r\n)"; // Specify your columns

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        Console.WriteLine("CSV data successfully uploaded to MySQL.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
