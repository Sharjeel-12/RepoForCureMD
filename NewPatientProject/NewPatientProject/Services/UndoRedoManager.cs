using NewPatientProject.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPatientProject.Services
{
    public class UndoRedoManager
    {  
        private readonly string _UndoFilePath = @"..\..\Items_to_Undo.txt";
        private readonly string _RedoFilePath = @"..\..\Items_to_Redo.txt";
        private static Queue<UndoRedoAction> Actions_Undo = new Queue<UndoRedoAction>();
        private static Queue<UndoRedoAction> Actions_Redo = new Queue<UndoRedoAction>();
        private static List<string> _UndoFileStrings= new List<string>();
        private static List<string> _RedoFileStrings= new List<string>();
        public UndoRedoManager()
        {
            // Check the existence of data in the Undo File.

            if (File.Exists(_UndoFilePath))
            {
                _UndoFileStrings=File.ReadAllLines(_UndoFilePath).ToList();
            }
            else
            {
                File.WriteAllText(_UndoFilePath, "");
            }

            // Check the existence of data in the Undo File.

            if (File.Exists(_RedoFilePath))
            {
                _RedoFileStrings = File.ReadAllLines(_RedoFilePath).ToList();
            }
            else
            {
                File.WriteAllText(_RedoFilePath, "");
            }
        }

        // the structure of the string is: "The record of the patient: {patient_name} has been {status}"
        // The {status} mean Added, Updated, or deleted
        public void GetActionsFromStrings()
        {
            if(_UndoFileStrings.Count > 0)
            {
                foreach (string undostr in _UndoFileStrings)
                {
                    string[] undo_action_words = undostr.Split(new[] { " the record of the patient: " }, StringSplitOptions.None);
                    string patient_name = undo_action_words[1];
                    string action = undo_action_words[0];
                    AddUndoAction(patient_name, action);
                }
            }



            if (_UndoFileStrings.Count > 0)
            {
                foreach (string redostr in _RedoFileStrings)
                {
                    string[] redo_action_words = redostr.Split(new[] { " the record of the patient: " }, StringSplitOptions.None);
                    string patient_name = redo_action_words[1];
                    string action = redo_action_words[0];
                    AddRedoAction(patient_name, action);
                }
            }

               
        }

        // Adding an action" -

        public void AddUndoAction(string patient_name, string action)
        {
            UndoRedoAction UndoAction=new UndoRedoAction(patient_name,action);
            if (Actions_Undo.Count > 10)
            {
                UndoRedoAction UndoGarbage=Actions_Undo.Dequeue();
                Actions_Undo.Enqueue(UndoAction);
            }
            else
            {
                Actions_Undo.Enqueue(UndoAction);
            }
        }
        public void AddRedoAction(string patient_name, string action)
        {
            UndoRedoAction RedoAction = new UndoRedoAction(patient_name, action);
            if (Actions_Redo.Count > 10)
            {
                UndoRedoAction RedoGarbage = Actions_Redo.Dequeue();
                Actions_Redo.Enqueue(RedoAction);
            }
            else
            {
                Actions_Redo.Enqueue(RedoAction);
            }
        }

        public Queue<UndoRedoAction> UndoAction(string name, string action)
        {
            UndoRedoAction undoaction = new UndoRedoAction(name, action);
            List<UndoRedoAction> List_UndoActions = Actions_Undo.ToList();
            List_UndoActions.Remove(undoaction);
            Queue<UndoRedoAction> Undo_Actions=new Queue<UndoRedoAction>(List_UndoActions);
            return Undo_Actions;
            // procedure
        }
        public Queue<UndoRedoAction> RedoAction(string name, string action)
        {
            UndoRedoAction redoaction = new UndoRedoAction(name, action);
            List<UndoRedoAction> List_RedoActions = Actions_Redo.ToList();
            List_RedoActions.Remove(redoaction);
            Queue<UndoRedoAction> Redo_Actions = new Queue<UndoRedoAction>(List_RedoActions);
            return Redo_Actions;
        }
        // get strings from actions as well
        public List<string> GetStringsFromActions(Queue<UndoRedoAction> Actions)
        {
            
            List<string> Action_Strings=new List<string>();
            foreach(UndoRedoAction action in Actions)
            {
                
                Action_Strings.Add($"{action.Action} the record of the patient: {action.PatientName}");
            }
            return Action_Strings;
        }

        // Writing in the undo record file: -
        public void WriteUndoRecord(Queue<UndoRedoAction> UndoActions)
        {
            List<string> UndoActionStrings=GetStringsFromActions(UndoActions);
            
            File.WriteAllLines(_UndoFilePath, UndoActionStrings);

        }
        // Writing in the redo record file: -

        public void WriteRedoRecord(Queue<UndoRedoAction> RedoActions)
        {
            List<string> RedoActionStrings = GetStringsFromActions(RedoActions);

            File.WriteAllLines(_RedoFilePath, RedoActionStrings);

        }

        public void DisplayActions(Queue<UndoRedoAction> Actions)
        {
            List<string> ActionStrings = GetStringsFromActions(Actions);
            foreach (string str in ActionStrings) {
                Console.WriteLine(str);
            }
        }
        /*********************************************************************************************************/
    }
}
