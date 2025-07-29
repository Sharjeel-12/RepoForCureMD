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

        Console.WriteLine("1: Add New Patient's Record");
        Console.WriteLine("2: View previous Patient's Records");
        Console.WriteLine("3: Update Patient's Records");
        Console.WriteLine("4: Delete any Patient's record");
        Console.WriteLine("5: Search any Patient's Record");
    }
    public static void getCommand(string key)
    {
        switch (key) {
            case "1":
                RecordAdder();
                break;

            default:
                Console.WriteLine("You pressed invalid key command");
                break;

        }


    }

    public static void RecordAdder()
    {
        Console.WriteLine("Hi");
    }
}






//The class for the patient
public class Patient
{   
    public string Name { get; set; }
    public int ID { get; set; }
    public string VisitType { get; set; }
    public DateOnly VisitDate { get; set; }

    public static List<string> records=new List<string>();
    public static List<Patient> AllPatients = new List<Patient>() {};
    
    // Parameterized Constructor 
    public Patient(string name,int id, string visittype, DateOnly visitdate)
    {   
        if (AllPatients.Count != 0)
        {   int numPatients = AllPatients.Count;
            for (int i=0; i<numPatients;i++)
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
    {   if (patient.ID != -1)
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
                patientRecord=records[i];
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
    {   int status = 0;
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

    }
}