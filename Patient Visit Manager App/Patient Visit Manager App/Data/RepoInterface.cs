using Microsoft.AspNetCore.Mvc.Infrastructure;
using Patient_Visit_Manager_App.Models;
using System.Data.SqlClient;
namespace Patient_Visit_Manager_App.Data
{
    public interface IRepoInterface
    {
        Task<SqlConnection> createConnectionAsync();
        Task<SqlCommand> createCommand(string sql_command);
        
        // executes a read only command
        Task<IEnumerable<Patient>> ExecutePatientReadCommand(SqlCommand command);
        
        // executes a read only command
        Task<IEnumerable<Patient>> ExecuteDoctorReadCommand(SqlCommand command);

        // can be Create,Update, and Delete sql command
        Task ExecuteWriteCommand(SqlCommand command);
        

    }
}
