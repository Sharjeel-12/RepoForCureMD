using Microsoft.AspNetCore.DataProtection.Repositories;
using Patient_Visit_Manager_App.Data;
using Patient_Visit_Manager_App.Models;

namespace Patient_Visit_Manager_App.Services
{
    public class AdminService:IAdminInterface
    {
        private static IRepoInterface _Repo;
        public AdminService(IRepoInterface repo)
        {
            _Repo = repo;
        }
        public async Task AddNewPatientRecord(Patient patient)
        {
            string command_string=$"INSERT INTO Patients()"
            _Repo.createCommand();
        }



    }
}
