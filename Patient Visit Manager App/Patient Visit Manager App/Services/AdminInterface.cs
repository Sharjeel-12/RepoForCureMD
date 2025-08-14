using Patient_Visit_Manager_App.Models;

namespace Patient_Visit_Manager_App.Services
{
    public interface IAdminInterface
    {
        // Add new patient
        Task AddNewPatientRecord(Patient patient);
        Task DeletePatientRecord(int id);
        Task UpdatePatientRecord(int id);
        
        // Adding new visit
        Task AddNewVisit(Visit visit);
        Task DeleteVisit(int id);
        Task UpdateVisit(int id);
       
        // Adding new docotor record
        Task AddNewDoctorRecord(Doctor doc);
        Task DeleteDoctorRecord(int id);
        Task UpdateDoctorRecord(int id);

        // Displaying Functions: -
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
