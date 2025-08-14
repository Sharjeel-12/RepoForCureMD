using Patient_Visit_Manager_App.Models;
using System.Data.SqlClient;

namespace Patient_Visit_Manager_App.Data
{
    public class SQL_DB : IRepoInterface
    {
        private static string _Connection_String;
        public SQL_DB(string connectionString)
        {
            _Connection_String = connectionString;
        }

        public async Task<SqlConnection> createConnectionAsync() 
        {
            SqlConnection newConnection = new SqlConnection(_Connection_String);
            await newConnection.OpenAsync();
            return newConnection;
        }
        public async Task<SqlCommand> createCommand(string sql_command)
        {
            SqlCommand newCommand = new SqlCommand(sql_command);
            return newCommand;
        }

        public async Task<IEnumerable<Patient>> ExecutePatientReadCommand(SqlCommand command)
        {
            List<Patient> result = new List<Patient>();
            using(SqlConnection connection = await createConnectionAsync())
            {
                command.Connection= connection;
                using(SqlDataReader reader= command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        Patient patient = new Patient();
                        patient.Name = $"{reader["patientName"]}";
                        patient.Description = $"{reader["patientDescription"]}";
                        patient.VisitID = Convert.ToInt32(reader["VisitID"]);
                        patient.PatientEmail = $"{reader["patientEmail"]}";
                        patient.PatientID = Convert.ToInt32(reader["patientID"]);
                        result.Add(patient);
                    }
                }
            }
            return result;
        }
        public async Task<IEnumerable<Doctor>> ExecuteDoctorReadCommand(SqlCommand command)
        {
            List<Doctor> result = new List<Doctor>();
            using (SqlConnection connection = await createConnectionAsync())
            {
                command.Connection = connection;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        Doctor doctor = new Doctor();
                        doctor.Name = $"{reader["doctorName"]}";
                        doctor.Specialization = $"{reader["Specialization"]}";
                        doctor.VisitID = Convert.ToInt32(reader["VisitID"]);
                        doctor.Email = $"{reader["doctorEmail"]}";
                        doctor.Id = Convert.ToInt32(reader["doctorID"]);
                        doctor.Phone = $"{reader["doctorPhone"]}";
                        result.Add(doctor);
                    }
                }
            }
            return result;
        }

        public async Task ExecuteWriteCommand(SqlCommand command)
        {
            using(SqlConnection connection= await createConnectionAsync())
            {
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();
            }
        }

    }
}
