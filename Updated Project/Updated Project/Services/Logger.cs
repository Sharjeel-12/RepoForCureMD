using System;
using System.IO;

namespace PatientVisitManager.Services
{
    public class Logger
    {
        private readonly string _logPath;

        public Logger(string logPath = null)
        {
            _logPath = logPath ?? Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\activity_log.txt"));
        }

        public void Notify(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[INFO] " + msg);
            Console.ResetColor();
            Log(msg, true);
        }

        public void Warn(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARN] " + msg);
            Console.ResetColor();
            Log(msg, false);
        }

        public void Log(string msg, bool success)
        {
            var status = success ? "SUCCESS" : "FAIL";
            var entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {status} | {msg}";
            try { File.AppendAllText(_logPath, entry + Environment.NewLine); } catch { }
        }
    }
}
