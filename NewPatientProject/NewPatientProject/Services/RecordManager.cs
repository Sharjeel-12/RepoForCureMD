using NewPatientProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPatientProject.Services
{   
    public class RecordManager

    {
        // this list also needs to be loaded with the objects first!!!
        // So we need some class to do the file reading job

        private List<Patient> record_list=new List<Patient>();
        public void setAllPatientRecord(List<Patient> ListOfPatients)
        {
            record_list.AddRange(ListOfPatients);
        }
        public List<Patient> AddNewPatient(Patient patient)
        {
            record_list.Add(patient);
            return record_list;
        }
        public List<Patient> DeletePatient(string name)
        {
            foreach(Patient patient in record_list)
            {
                if(patient.Name == name)
                {
                    record_list.Remove(patient);
                }
            }
            return record_list;
        }
        // Notes: this function will further be updated for better performance: -
        public List<Patient> UpdatePatientRecord(string name)
        {
            foreach (Patient patient in record_list)
            {
                if (patient.Name == name)
                {   Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Patient Found!\n");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please Enter the new details of the Patient");
                    Console.Write("Re-Enter the Patient Name: ");
                    string new_name=Console.ReadLine();
                    Console.Write("Enter the Patient new Visit Date (format: yyyy-mm-dd) :- ");
                    string new_date = Console.ReadLine();
                    Console.Write("Enter the Patient new Visit Time (format: HH:mm:ss) :- ");
                    string new_time = Console.ReadLine();
                    Console.Write("Enter the Patient new Visit Type (Consultation, Follow-Up, Emergency) :- ");
                    string new_visit_type = Console.ReadLine();
                    Console.Write("Enter the new doctor name for consultation :- ");
                    string new_dr = Console.ReadLine();
                    Console.Write("Enter the patient description :- ");
                    string new_desc = Console.ReadLine();
                    
                    
                    // Assigning new parameters of the patient
                    patient.Name = new_name;
                    patient.VisitDate=DateOnly.ParseExact(new_date,"yyy-MM-dd");
                    patient.VisitTime=TimeOnly.ParseExact(new_time,"HH:mm:ss");
                    patient.DoctorName = new_dr;
                    patient.Description = new_desc;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Patient's Record Updated Successfully");


                }
            }
            return record_list;

        }
        public List<Patient> GetAllPatientRecord() {
            
            return record_list;
        
        }
    }
}
