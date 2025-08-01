using System;
using System.Collections.Generic;

namespace PatientVisitManager.Utils
{
    public class UndoRedoManager
    {
        private readonly Stack<Action> _undo = new();
        private readonly Stack<Action> _redo = new();
        private const int Capacity = 10;

        public int UndoCount() => _undo.Count;
        public int RedoCount() => _redo.Count;

        public void Execute(Action doAct, Action undoAct)
        {
            doAct();
            if (_undo.Count >= Capacity) Trim(_undo);
            _undo.Push(undoAct);
            _redo.Clear();
        }

        public void Undo()
        {
            if (_undo.Count == 0) { Console.WriteLine("Nothing to undo."); return; }
            var act = _undo.Pop();
            act();
            _redo.Push(act);
        }

        public void Redo()
        {
            if (_redo.Count == 0) { Console.WriteLine("Nothing to redo."); return; }
            var act = _redo.Pop();
            act();
            _undo.Push(act);
        }

        private void Trim(Stack<Action> stack)
        {
            var arr = stack.ToArray();
            Array.Resize(ref arr, Capacity - 1);
            stack.Clear();
            foreach (var a in arr) stack.Push(a);
        }
    }
}
