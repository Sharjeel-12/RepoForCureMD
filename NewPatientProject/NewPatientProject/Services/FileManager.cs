using NewPatientProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NewPatientProject.Services
{
    public class FileManager
    {
        /*
         Lets list up some functions of this class: -
        1- Read the file and prepare the patient objects from the csv file: PatientRecords.csv
        2- Write data to the .csv file using the loaded 
        3- we write data as: WriteAllText(_FirstLine);AppendAllLines(_ObjStrings)

         */
        private static readonly string _FilePath = @"../../PatientRecords.csv";
        public static List<Patient> PatientObjects_Loaded= new List<Patient>();
        private static string _FirstLine = "Name,Description,VisitDate,VisitTime,VisitType,DoctorName\n";
        private static List<string> _ObjStrings= new List<string>();
        public FileManager() {

            if (File.Exists(_FilePath))
            {
                string first_line=File.ReadAllLines(_FilePath)[0];
                if (first_line == _FirstLine)
                {
                    _ObjStrings = File.ReadAllLines(_FilePath)[1..].ToList();
                }
                else
                {
                    _ObjStrings = File.ReadAllLines(_FilePath).ToList();
                }
            }
            else
            {
                File.WriteAllText(_FilePath, "");   
            }

        }
        public static List<Patient> PreparePatientObjectsFromFile()
        {
            
            if (_ObjStrings.Count > 0)
            {
                foreach (string str in _ObjStrings) {
                    string[] Patient_params=str.Split(',');
                    Patient patient=new Patient(Patient_params[0], DateOnly.ParseExact(Patient_params[1],"yyyy-MM-dd"), TimeOnly.ParseExact(Patient_params[2], "HH:mm:ss"), Patient_params[3], Patient_params[4], Patient_params[5]);
                    PatientObjects_Loaded.Add(patient);
                }
            }
            
            return PatientObjects_Loaded;
            // this returned list of objects will be going to the Record Manager for further processing
        }
        public static List<string> ConvertObjsToStrings(List<Patient> PatientObjects)
        {
            List<string> ObjectStrings=new List<string>();
            foreach (Patient obj in PatientObjects)
            {
                ObjectStrings.Add($"{obj.Name},{obj.VisitDate.ToString()},{obj.VisitTime.ToString()},{obj.VisitType},{obj.Description},{obj.DoctorName}");

            }
            return ObjectStrings;
        }
        // function to write all the patient records in a .csv file
        public static void WritePatientRecord(List<Patient> PatientObjects)
        {
            List<string> data_strings=ConvertObjsToStrings(PatientObjects);
            File.WriteAllText(_FilePath, _FirstLine);
            File.AppendAllLines(_FilePath, data_strings);

        }

        // function to read a file and display the records of the Patient
        public static void DisplayAllPatientRecord()
        {
            List<Patient> List_of_Patients=PreparePatientObjectsFromFile();
            if(List_of_Patients.Count > 0)
            {
                foreach(Patient patient in List_of_Patients)
                {

                    Console.WriteLine($"Name of Patient: {patient.Name}");
                    Console.WriteLine($"Visit Date: {patient.VisitDate.ToString()}");
                    Console.WriteLine($"Visit Time: {patient.VisitTime.ToString()}");
                    Console.WriteLine($"Type of Visit: {patient.VisitType}");
                    Console.WriteLine($"Desciption: {patient.Description}");

                }
            }
            else
            {
                Console.Write("The file is empty\n\n");
                Console.Write("There is no record to display");
            }
        }

    }
}
