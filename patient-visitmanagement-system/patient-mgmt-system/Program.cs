// PatientVisitManager.cs
/*
 ***************IMPORTANT NOTE********************************************************

1- The CSV file is saved as PatientRecords.csv

2- It's saved in the current working directory (usually at bin\Debug\net7.0\ if you are using Visual Studio)

************************************************************************************** 
 */


using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class Patient
{
    public string Name { get; set; }
    public string DoctorName { get; set; }
    public string VisitType { get; set; }
    public string Description { get; set; }
    public DateTime VisitDate { get; set; }

    public Patient(string name, string doctorName, string visitType, string description, DateTime visitDate)
    {
        Name = name;
        DoctorName = doctorName;
        VisitType = visitType;
        Description = description;
        VisitDate = visitDate;
    }

    public override string ToString()
    {
        return $"{Name},{DoctorName},{VisitType},{Description},{VisitDate:yyyy-MM-dd}";
    }

    public static Patient FromCsv(string csvLine)
    {
        var parts = csvLine.Split(',');
        return new Patient(parts[0], parts[1], parts[2], parts[3], DateTime.ParseExact(parts[4], "yyyy-MM-dd", CultureInfo.InvariantCulture));
    }
}

public class PatientDataHandler
{
    public static string filepath = "PatientRecords.csv";

    public static void WriteRecord(List<string> array)
    {
        File.WriteAllLines(filepath, array);
    }

    public static List<string> ReadAll()
    {
        if (!File.Exists(filepath)) return new List<string>();
        return File.ReadAllLines(filepath).ToList();
    }

    public static void DisplayFileContent()
    {
        if (!File.Exists(filepath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filepath);
        if (lines.Length == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        Console.WriteLine("Name\tDoctor\tType\tDescription\tDate");
        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            Console.WriteLine(string.Join("\t", values));
        }
    }
}

public class UndoRedoManager
{
    private Stack<Action> undoStack = new Stack<Action>();
    private Stack<Action> redoStack = new Stack<Action>();

    public void AddUndo(Action action)
    {
        if (undoStack.Count >= 10) undoStack = new Stack<Action>(undoStack.Reverse().Skip(1));
        undoStack.Push(action);
        redoStack.Clear();
    }

    public void Undo()
    {
        if (undoStack.Count == 0) { 
            Console.WriteLine("Nothing to undo."); 
            return; 
        }
        var action = undoStack.Pop();
        action.Invoke();
        redoStack.Push(action);
    }

    public void Redo()
    {
        if (redoStack.Count == 0) { 
            Console.WriteLine("Nothing to redo."); 
            return;
        }
        var action = redoStack.Pop();
        action.Invoke();
        undoStack.Push(action);
    }
}

public class PatientManager
{
    public List<Patient> AllPatients;
    private UndoRedoManager history;

    public PatientManager(UndoRedoManager undoManager)
    {
        history = undoManager;
        AllPatients = PatientDataHandler.ReadAll().Select(Patient.FromCsv).ToList();
    }

    public void AddPatientRecord(Patient patient)
    {
        AllPatients.Add(patient);
        Save();
        history.AddUndo(() => { AllPatients.Remove(patient); Save(); });
        Console.WriteLine("Visit Added!");
    }

    public void DeletePatientRecord(string name, DateTime date)
    {
        var visit = AllPatients.FirstOrDefault(p => p.Name == name && p.VisitDate.Date == date.Date);
        if (visit != null)
        {
            AllPatients.Remove(visit);
            Save();
            history.AddUndo(() => { AllPatients.Add(visit); Save(); });
            Console.WriteLine("Patient Record Deleted Successfully");
        }
        else Console.WriteLine("Record not found.");
    }

    public void UpdateRecord(string name, DateTime date, Patient updated)
    {
        int index = AllPatients.FindIndex(p => p.Name == name && p.VisitDate.Date == date.Date);
        if (index != -1)
        {
            Patient old = AllPatients[index];
            AllPatients[index] = updated;
            Save();
            history.AddUndo(() => { AllPatients[index] = old; Save(); });
            Console.WriteLine("Record Updated Successfully");
        }
        else Console.WriteLine("Record not found.");
    }

    public void SearchPatientRecord(string keyword)
    {
        var found = AllPatients.Where(p =>
            p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            p.DoctorName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            p.VisitType.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            p.VisitDate.ToString("yyyy-MM-dd").Contains(keyword)).ToList();

        if (found.Count == 0)
            Console.WriteLine("No matching record found.");
        else
            foreach (var p in found) Console.WriteLine(p);
    }

    public void Save()
    {
        List<string> lines = AllPatients.Select(p => p.ToString()).ToList();
        PatientDataHandler.WriteRecord(lines);
    }

    public void ShowVisitStatistics()
    {
        var groups = AllPatients.GroupBy(p => p.VisitType);
        Console.WriteLine("Visit Count per Type:");
        foreach (var g in groups)
        {
            Console.WriteLine($"{g.Key}: {g.Count()} visits");
        }
    }

    public void WeeklySummary()
    {
        DateTime weekAgo = DateTime.Now.AddDays(-7);
        var recent = AllPatients.Where(p => p.VisitDate >= weekAgo).ToList();
        Console.WriteLine("Weekly Visit Summary:");
        foreach (var p in recent)
        {
            Console.WriteLine(p);
        }
    }

    public void GenerateMockData(int count)
    {
        string[] names = { "Ali", "Ahmed", "Sara", "Mina", "Tariq" };
        string[] doctors = { "Dr. A", "Dr. B", "Dr. C" };
        string[] types = { "Consultation", "Follow-up", "Emergency" };
        Random rnd = new Random();

        for (int i = 0; i < count; i++)
        {
            Patient p = new Patient(
                names[rnd.Next(names.Length)] + i,
                doctors[rnd.Next(doctors.Length)],
                types[rnd.Next(types.Length)],
                "Mock description",
                DateTime.Now.AddDays(-rnd.Next(30)));
            AllPatients.Add(p);
        }
        Save();
        Console.WriteLine($"{count} Mock Entries Generated");
    }
}

public class AppInterface
{
    public static void Welcome()
    {
        Console.WriteLine("\n*************************************************************\n");
        Console.WriteLine("**********Welcome to the Patient Management Application******");
        Console.WriteLine("\n*************************************************************\n");
        Console.WriteLine("Choose your action:\n");
    }

    public static void DisplayMenu()
    {
        Console.WriteLine("\n1: Add New Patient Record");
        Console.WriteLine("2: View all Patient Records");
        Console.WriteLine("3: Update Patient Record");
        Console.WriteLine("4: Delete Patient Record");
        Console.WriteLine("5: Search Patient Records");
        Console.WriteLine("6: Show Visit Statistics");
        Console.WriteLine("7: Show Weekly Summary");
        Console.WriteLine("8: Generate Mock Records");
        Console.WriteLine("9: Undo\n10: Redo\n0: Exit");
    }
}

public class PatientVisitManager
{
    public static void Main()
    {
        UndoRedoManager history = new UndoRedoManager();
        PatientManager manager = new PatientManager(history);
        AppInterface.Welcome();

        while (true)
        {
            AppInterface.DisplayMenu();
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();
            try
            {
                switch (input)
                {
                    case "1":
                        Console.Write("Enter Patient Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Doctor Name: ");
                        string doc = Console.ReadLine();
                        Console.Write("Enter Visit Type: ");
                        string type = Console.ReadLine();
                        Console.Write("Enter Visit Description: ");
                        string desc = Console.ReadLine();
                        Console.Write("Enter Visit Date (yyyy-MM-dd): ");
                        DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        Patient patient = new Patient(name, doc, type, desc, date);
                        manager.AddPatientRecord(patient);
                        break;
                    case "2":
                        PatientDataHandler.DisplayFileContent();
                        break;
                    case "3":
                        Console.Write("Enter Patient Name to Update: ");
                        string oldName = Console.ReadLine();
                        Console.Write("Enter Visit Date (yyyy-MM-dd): ");
                        DateTime oldDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        Console.Write("Enter New Name: ");
                        string newName = Console.ReadLine();
                        Console.Write("Enter New Doctor Name: ");
                        string newDoc = Console.ReadLine();
                        Console.Write("Enter New Visit Type: ");
                        string newType = Console.ReadLine();
                        Console.Write("Enter New Description: ");
                        string newDesc = Console.ReadLine();
                        Console.Write("Enter New Date (yyyy-MM-dd): ");
                        DateTime newDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        manager.UpdateRecord(oldName, oldDate, new Patient(newName, newDoc, newType, newDesc, newDate));
                        break;
                    case "4":
                        Console.Write("Enter Patient Name to Delete: ");
                        string delName = Console.ReadLine();
                        Console.Write("Enter Visit Date (yyyy-MM-dd): ");
                        DateTime delDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        manager.DeletePatientRecord(delName, delDate);
                        break;
                    case "5":
                        Console.Write("Enter keyword to search: ");
                        string key = Console.ReadLine();
                        manager.SearchPatientRecord(key);
                        break;
                    case "6":
                        manager.ShowVisitStatistics();
                        break;
                    case "7":
                        manager.WeeklySummary();
                        break;
                    case "8":
                        Console.Write("How many mock entries to generate: ");
                        int num = int.Parse(Console.ReadLine());
                        manager.GenerateMockData(num);
                        break;
                    case "9": history.Undo(); break;
                    case "10": history.Redo(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
