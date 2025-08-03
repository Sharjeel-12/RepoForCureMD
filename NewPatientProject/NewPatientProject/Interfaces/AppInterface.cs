using NewPatientProject.Models;
using NewPatientProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPatientProject.Interfaces
{
    public interface IAppEvironment
    {
        void Greet();
        void DisplayMenu();
    }
    public interface IAdminInteraction
    {
        void AddPatientRecord();
        void DeletePatientRecord();
        void SearchPatientRecord();
        void ViewAllPatientRecords();
        void UndoAction();
        void RedoAction();
        void sortAndFilterRecords();
        void GenerateSummaryAndStats();
        void ManageConflicts();

    }
    public interface IReceptionInteraction
    {
        void SearchPatientRecord();
        void ViewAllPatientRecords();
        void sortAndFilterRecords();
        void GenerateSummaryAndStats();

    }
    public class Admin:IAdminInteraction
    {
        
        // Preparing the managers of the patient data as follows: -

        public FileManager file_manager = new FileManager();
        public RecordManager record_manager = new RecordManager();
        public UndoRedoManager undo_redo_manager = new UndoRedoManager();
        private static string[] VisitTypes = new string[] { "consultation", "follow-up", "emergency" };
        public Patient PreparePatientFromPrompt()
        {
            // Creating User Prompt for entering patient data: -

            Console.WriteLine("Please Enter the details of the new Patient");
            Console.Write("Enter the Patient Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter the Patient Visit Date (format: yyyy-mm-dd) :- ");
            string date = Console.ReadLine();
            //Making sure the correct date is entered by the user.....
            while (!(DateOnly.TryParseExact(date, "yyyy-MM-dd", out DateOnly result)))
            {
                Console.WriteLine("You entered the date in an invalid format");
                Console.WriteLine("Please re-enter the date in the corect format (yyyy-MM-dd)");
                date = Console.ReadLine();
            }
            Console.Write("Enter the Patient Visit Time (format: HH:mm:ss) :- ");
            string time = Console.ReadLine();
            //Making sure the correct meeting time is entered by the user.....
            while (!(TimeOnly.TryParseExact(time, "HH:mm:ss", out TimeOnly result)))
            {
                Console.WriteLine("You entered the meeting time in an invalid format");
                Console.WriteLine("Please re-enter the visit in the corect format (HH:mm:ss)");
                time = Console.ReadLine();
            }


            Console.Write("Enter the Patient Visit Type (Consultation, Follow-Up, Emergency) :- ");
            string visit_type = Console.ReadLine();
            //Making sure the correct visit type is entered by the user.....
            while (!VisitTypes.Contains(visit_type.ToLower()))
            {
                Console.WriteLine("You entered invalid visit type");
                Console.WriteLine("Please re-enter the visit type of the patient as one of the following: Consultation, Follow-Up, Emergency");
                visit_type = Console.ReadLine();

            }


            Console.Write("Enter the doctor name for consultation :- ");
            string dr = Console.ReadLine();
            Console.Write("Enter the patient description :- ");
            string desc = Console.ReadLine();
            Console.Write("Enter the visit duration :- ");
            int duration = Convert.ToInt32(Console.ReadLine());

            // Preparing the Patient Object
            Patient patient = new Patient(name, DateOnly.ParseExact(date, "yyyy-MM-dd"), TimeOnly.ParseExact(time, "HH:mm:ss"), visit_type, desc, dr, duration);

            return patient;
        }
        public void AddPatientRecord()
        { 
            
        // Calling functions from their respective classes
        
            Patient patient=PreparePatientFromPrompt();
            List<Patient>AllPatients=file_manager.PreparePatientObjectsFromFile();
            record_manager.setAllPatientRecord(AllPatients);
            List<Patient> updated_record_list=record_manager.AddNewPatient(patient);
            file_manager.WritePatientRecord(updated_record_list);
            undo_redo_manager.AddUndoAction(undo_action);


        }
        public void DeletePatientRecord()
        {

        }
        public void SearchPatientRecord()
        {

        }
        public void ViewAllPatientRecords()
        {

        }
        public void UndoAction()
        {

        }
        public void RedoAction()
        {

        }
        public void sortAndFilterRecords()
        {

        }
        public void GenerateSummaryAndStats()
        {

        }
        public void ManageConflicts()
        {

        }
    
    //*****************************************************************
    }
    public class Reception: IReceptionInteraction
    {
        public void SearchPatientRecord()
        {

        }
        public void ViewAllPatientRecords()
        {

        }
        public void sortAndFilterRecords()
        {

        }
        public void GenerateSummaryAndStats()
        {

        }

    
    
    //***********************************************************
    }









    //**************************************************
}
