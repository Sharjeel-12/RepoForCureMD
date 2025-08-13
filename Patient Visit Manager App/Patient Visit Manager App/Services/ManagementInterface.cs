using Patient_Visit_Manager_App.Models;

namespace Patient_Visit_Manager_App.Services
{
    public interface IManagementInterface
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

    }
}
