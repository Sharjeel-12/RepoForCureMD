// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

// class for the app interface
public class AppInterface
{
    public static void Welcome()
    {
        Console.WriteLine("\n*************************************************************\n\n");
        Console.WriteLine("**********Welcome to the Patient Management Application******");
        Console.WriteLine("\n*************************************************************\n\n");
        Console.WriteLine("Decide your actions through the following keys: - \n\n");

    }
    public static void DisplayMenu()
    {
        Console.WriteLine("*************************************************************\n\n");

        Console.WriteLine("1: Add New Patient Record");
        Console.WriteLine("2: View all previous Patient Records");
        Console.WriteLine("3: Update any Patient Records");
        Console.WriteLine("4: Delete any Patient record");
        Console.WriteLine("5: Search any Patient Record");
    }
    public static void getCommand(string key)
    {
        switch (key)
        {
            case "1":
                RecordAdder();
                break;
            case "2":
                ViewAllRecords();
                break;
            case "4":
                DeleteRecord();
                break;
            default:
                Console.WriteLine("You pressed invalid key command");
                break;

        }


    }

    public static void RecordAdder()
    {
        int id;
        string name, visit_type, visit_date;
        Console.WriteLine("Enter the Patient Name: -");
        name = Console.ReadLine();
        Console.WriteLine("Enter the Patient ID");
        id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the Patient visit type: -");
        visit_type = Console.ReadLine();
        Console.WriteLine("Enter the Patient visit date (Format: yyyy/mm/dd): -");
        visit_date = Console.ReadLine();
        string[] date = visit_date.Split("/").ToArray();
        int[] int_date = Array.ConvertAll(date, Convert.ToInt32);
        Patient patient = new Patient(name, id, visit_type, new DateOnly(int_date[0], int_date[1], int_date[2]));
        Patient.AddPatientRecord(patient);
        PatientDataHandler.WriteRecord(Patient.records);
        Console.WriteLine("Visit Added");
        DisplayMenu();
        getCommand(Console.ReadLine());
    }

    public static void ViewAllRecords()
    {
        PatientDataHandler.DisplayFileContent();
        DisplayMenu();
        getCommand(Console.ReadLine());
    }

    public static void DeleteRecord()
    {
        Console.WriteLine("Enter the Patient ID to delete his/her visit record: -");
        int id = Convert.ToInt32(Console.ReadLine());
        Patient.DeletePatientRecord(id);
        Console.WriteLine("Patient Record Deleted Successfully");
        DisplayMenu();
        getCommand(Console.ReadLine());
    }


}
// The class for file and patient data Handling: -

public class PatientDataHandler
{
    public static string filepath = @"D:\PatientRecords.csv";
    
    public static void WriteRecord(List<string> array)
    {
       
        File.WriteAllLines(filepath, array);
        

    }
    public static void DisplayFileContent()
    {
        string[] ReadLines= File.ReadAllLines(filepath);
        if (ReadLines.Length != 0)
        {
            foreach (string line in ReadLines)
            {
                string[] values = line.Split(",");
                Console.WriteLine(values[0] + "\t\t" + values[1] + "\t\t" + values[2] + "\t\t" + values[3] + "\t\t");
            }
        }
        else
        {
            File.WriteAllText(filepath, "Name,ID,VisitType, VisitDate");
            Console.Write("the file is empty. No record p☻resent");
        }
        
        
        
    }

}





//The class for the patient
public class Patient
{
    public static string filepath = @"D:\PatientRecords.csv";
    public string Name { get; set; }
    public int ID { get; set; }
    public string VisitType { get; set; }
    public DateOnly VisitDate { get; set; }
    public static string[] patient_records = File.ReadAllLines(filepath);
    public static List<string> records = new List<string>(patient_records);
    
    public static List<Patient> AllPatients = new List<Patient>() { };

    // Parameterized Constructor 
    public Patient(string name, int id, string visittype, DateOnly visitdate)
    {
        if (AllPatients.Count != 0)
        {
            int numPatients = AllPatients.Count;
            for (int i = 0; i < numPatients; i++)
            {
                if (AllPatients[i].ID == id)
                {
                    Console.WriteLine("This patient ID Already Exists. Please try again with new ID");
                    ID = -1;
                }
                else
                {
                    Name = name; VisitType = visittype; VisitDate = visitdate; ID = id;
                    AllPatients.Add(this);
                }
            }
        }
        else
        {
            Name = name; VisitType = visittype; VisitDate = visitdate; ID = id;
            AllPatients.Add(this);
        }


    }

    public static void AddPatientRecord(Patient patient)
    {
        if (patient.ID != -1)
        {
            records.Add($"{patient.Name},{patient.ID},{patient.VisitType},{patient.VisitDate}");
        }
        else
        {
            Console.WriteLine("this patient cannot be added to record");
        }




    }
    public static string SearchPatientRecord(int PatientID)
    {
        string patientRecord = "Record not found!";
        for (int i = 0; i < records.Count; i++)
        {

            if (PatientID == Convert.ToInt32(records[i].Split(",")[1]))
            {
                patientRecord = records[i];
            }
        }
        return patientRecord;
    }
    public static void DeletePatientRecord(int PatientID)
    {
        int status = 0;
        for (int i = 0; i < records.Count; i++)
        {

            if (PatientID == Convert.ToInt32(records[i].Split(",")[1]))
            {
                records.RemoveAt(i);
                status = 1;
            }

        }
        if (status == 0)
        {
            Console.WriteLine("No Patient Record Found");
        }
    }

    public static void UpdateRecord(int PatientID, string newname, DateOnly newdate, string newvisittype)
    {
        int status = 0;
        foreach (Patient patient in AllPatients)
        {
            if (patient.ID == PatientID)
            {
                patient.Name = newname;
                patient.VisitDate = newdate;
                patient.VisitType = newvisittype;
                Patient.DeletePatientRecord(PatientID);
                Patient.AddPatientRecord(patient);
                status = 1;
            }

        }
        if (status == 0)
        {
            Console.WriteLine("This Patient ID is not valid i.e. No patient with such ID exists");

        }
    }
    public static void DisplayRecords()
    {
        foreach (string record in records)
        {
            Console.WriteLine(record);
        }
    }
}



public class PatientVisitManager
{
    public static void Main()
    {
        string filePath = @"D:\PatientRecords.csv";

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            // Create the file and optionally write headers
            File.WriteAllText(filePath, "Name,Age,VisitType,VisitDate\n");
        }
        /*Patient patient1= new Patient("Harry", 1,"xyz",new DateOnly(2025,4,2));
        Patient patient2= new Patient("Jerry", 3,"xyz",new DateOnly(2025,4,2));
        Patient patient3= new Patient("Tom", 13, "xyz", new DateOnly(2025, 4, 2));

        Patient.AddPatientRecord(patient1);
        Patient.AddPatientRecord(patient2);
        Patient.AddPatientRecord(patient3);
        Console.WriteLine("Before Update: -");
        Patient.DisplayRecords();
        Patient.UpdateRecord(3, "Querry", new DateOnly(2025, 2, 2), "abc");
        Console.WriteLine("After updating");
        Patient.DisplayRecords();*/


        AppInterface.Welcome();
        AppInterface.DisplayMenu();
        AppInterface.getCommand(Console.ReadLine());   

    }
}
;
