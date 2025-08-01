using System;
using System.Collections.Generic;
using PatientVisitManager.Models;
using PatientVisitManager.Services;
using PatientVisitManager.Utils;
/*
 * IMPORTANT NOTE: -
 ADMIN username: admin
 ADMIN password: 1234

Reception username: reception
Reception password: 1234
 

Important Note: -

When the code is run again the csv file gets deleted.
I will fix it in the later version of my project soon.
 */
namespace PatientVisitManager
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Patient Visit Manager";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==============================================");
            Console.WriteLine("            Welcome to Visit Manager          ");
            Console.WriteLine("==============================================");
            Console.ResetColor();

            var role = RoleManager.Login();
            if (role == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Access denied. Exiting...");
                Console.ResetColor();
                return;
            }

            var logger = new Logger();
            var feeCalc = new FeeCalculator();
            var manager = new VisitManager(logger, feeCalc);
            var history = new UndoRedoManager();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n========== MAIN MENU ==========");
                Console.ResetColor();

                Console.WriteLine($"Undo available: {history.UndoCount()} | Redo available: {history.RedoCount()}");
                Console.WriteLine("1. Add Visit");
                Console.WriteLine("2. Search Visits");
                Console.WriteLine("3. Display All Visits");
                Console.WriteLine("4. Filter & Sort Visits");
                Console.WriteLine("5. Summary Statistics");
                if (role == UserRole.Admin)
                {
                    Console.WriteLine("6. Undo Last Action");
                    Console.WriteLine("7. Redo Last Action");
                    Console.WriteLine("8. Delete Visit");
                }
                Console.WriteLine("0. Exit");

                Console.Write("\nEnter choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ExecuteWithSnapshot(manager, history, () => manager.AddVisit(manager.ReadVisitFromInput(feeCalc)));
                        break;
                    case "2":
                        Console.Write("Enter patient name to search: ");
                        manager.Search(Console.ReadLine());
                        break;
                    case "3":
                        manager.DisplayAllVisits();
                        break;
                    case "4":
                        manager.FilterAndSort();
                        break;
                    case "5":
                        manager.ShowStatistics();
                        break;
                    case "6":
                        if (role == UserRole.Admin) history.Undo();
                        break;
                    case "7":
                        if (role == UserRole.Admin) history.Redo();
                        break;
                    case "8":
                        if (role == UserRole.Admin)
                        {
                            Console.Write("Enter index to delete: ");
                            if (int.TryParse(Console.ReadLine(), out int idx))
                                ExecuteWithSnapshot(manager, history, () => manager.DeleteVisit(idx));
                            else Console.WriteLine("Invalid input.");
                        }
                        break;
                    case "0":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Goodbye!");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        static void ExecuteWithSnapshot(VisitManager mgr, UndoRedoManager history, Action action)
        {
            var snap = mgr.GetSnapshot();
            history.Execute(action, () => mgr.RestoreSnapshot(snap));
        }
    }
}
