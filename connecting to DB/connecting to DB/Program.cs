using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

public class Program
{
    static void Main()
    {
        //string connectionString = @"Data Source=DESKTOP-BGJ5S5C;Initial Catalog=MuhammadSharjeelFarzadDB;Integrated Security=True";


        //string connectionString = "Data Source = DESKTOP-BGJ5S5C; Initial Catalog = MuhammadSharjeelFarzadDB; Integrated Security = True;";

        string connectionString = @"Server=DESKTOP-BGJ5S5C\TEW_SQLEXPRESS;Database=MuhammadSharjeelFarzadDB;User Id=Test;Password=cure2000;";

        using (SqlConnection connection=new SqlConnection(connectionString)) 
        {
            string getDataCommand = "SELECT * FROM Patients";
            connection.Open();
            Console.WriteLine("Connected to the Data Base successfully. Reading the patient data");
            using(SqlCommand command= new SqlCommand(getDataCommand,connection)) 
            {
                using (SqlDataReader reader= command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Patient Name={reader["patientName"]}\n" +
                            $"Patient Email: {reader["patientEmail"]}");
                    }
                }
            
            }
            connection.Close();
        }
    }
}
