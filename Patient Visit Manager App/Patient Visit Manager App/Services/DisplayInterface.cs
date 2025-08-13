using Patient_Visit_Manager_App.Models;
namespace Patient_Visit_Manager_App.Services
{
    public interface IDisplayInterface
    {
        // functions for fetching patient record.
        Task<List<Patient>> GetAllPatientRecordAsync();
        Task<List<Patient>> GetPatientByNameAsync(string name);
        Task<List<Patient>> GetPatientByVisitDateAsync(DateOnly date);
        Task<List<Patient>> GetPatientByEmailAsync(string email);
        
        // Functions for fetching doctor record.
        Task<List<Doctor>> GetAllDoctorRecordAsync();
        Task<List<Doctor>> GetDoctorByNameAsync();
        Task<List<Doctor>> GetDoctorBySpecializationAsync();
        Task<List<Patient>> GetDcotorByVisitDateAsync(DateOnly date);
        Task<List<Patient>> GetDoctorByEmailAsync(string email);

        // displays the statistics
        Task displayStatsAsync();

    }
}
