using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PatientVisitManager.Models;

namespace PatientVisitManager.Services
{
    public class VisitManager
    {
        private readonly string _filePath =
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\PatientRecords.csv"));
        private List<Visit> _visits;
        private readonly Logger _logger;
        private readonly FeeCalculator _feeCalculator;

        public VisitManager(Logger log, FeeCalculator feeCalc)
        {
            _logger = log;
            _feeCalculator = feeCalc;
            Initialize();
        }

        private void Initialize()
        {
            if (!File.Exists(_filePath))
            {
                _visits = GenerateMockData(300);
                Save();
                _logger.Notify("Generated 300 mock visits.");
            }
            else
            {
                _visits = Load();
            }
        }

        private List<Visit> Load()
        {
            var list = new List<Visit>();
            foreach (var line in File.ReadAllLines(_filePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try { list.Add(Visit.FromCsv(line)); }
                catch { _logger.Warn($"Skipped bad line: {line}"); }
            }
            return list;
        }

        private void Save()
        {
            var header = "PatientName,DoctorName,VisitType,VisitDateTime,DurationInMinutes,Fee";
            var lines = new[] { header }.Concat(_visits.Select(v => v.ToString()));
            File.WriteAllLines(_filePath, lines);
        }

        private List<Visit> GenerateMockData(int count)
        {
            var rnd = new Random();
            var fn = new[] { "Ali", "Sara", "John", "Mira", "Omar", "Zain", "Nida", "Hina" };
            var ln = new[] { "Khan", "Ahmed", "Raza", "Farooq", "Shah", "Bilal", "Nawaz", "Rizvi" };
            var docs = new[] { "Dr. A", "Dr. B", "Dr. C", "Dr. D" };
            var types = new[] { "Consultation", "Follow-up", "Emergency" };
            var start = DateTime.Now.AddYears(-1);

            var list = new List<Visit>();
            for (int i = 0; i < count; i++)
            {
                var type = types[rnd.Next(types.Length)];
                var v = new Visit
                {
                    PatientName = $"{fn[rnd.Next(fn.Length)]} {ln[rnd.Next(ln.Length)]}",
                    DoctorName = docs[rnd.Next(docs.Length)],
                    VisitType = type,
                    VisitDateTime = start.AddDays(rnd.Next(365)).AddMinutes(rnd.Next(1440)),
                    DurationInMinutes = new[] { 15, 30, 45, 60 }[rnd.Next(4)],
                    Fee = _feeCalculator.GetFee(type)
                };
                list.Add(v);
            }
            return list;
        }

        // NEW: Reads a Visit from user input
        public Visit ReadVisitFromInput(FeeCalculator feeCalc)
        {
            var v = new Visit();
            Console.Write("Patient Name: "); v.PatientName = Console.ReadLine();
            Console.Write("Doctor Name: "); v.DoctorName = Console.ReadLine();
            Console.Write("Visit Type: "); v.VisitType = Console.ReadLine();
            Console.Write("Visit DateTime (yyyy‑MM‑dd HH:mm): ");
            v.VisitDateTime = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null);
            Console.Write("Duration (minutes): "); v.DurationInMinutes = int.Parse(Console.ReadLine());
            v.Fee = feeCalc.GetFee(v.VisitType);
            return v;
        }

        public List<Visit> GetSnapshot() =>
            _visits.Select(v => v.Clone()).ToList();

        public void RestoreSnapshot(List<Visit> snap)
        {
            _visits = snap.Select(v => v.Clone()).ToList();
            Save();
            _logger.Notify("State restored via Undo/Redo.");
        }

        public void AddVisit(Visit v)
        {
            _visits.Add(v);
            Save();
            _logger.Notify($"Added visit for {v.PatientName} at {v.VisitDateTime:yyyy-MM-dd HH:mm}");
        }

        public void DeleteVisit(int idx)
        {
            if (idx < 0 || idx >= _visits.Count)
            {
                _logger.Warn("Delete failed: invalid index");
                return;
            }
            var rm = _visits[idx];
            _visits.RemoveAt(idx);
            Save();
            _logger.Notify($"Deleted visit for {rm.PatientName} on {rm.VisitDateTime:yyyy-MM-dd HH:mm}");
        }

        public void Search(string query)
        {
            var found = _visits.Where(v => v.PatientName.Contains(query, StringComparison.OrdinalIgnoreCase));
            if (!found.Any()) Console.WriteLine("No records found.");
            else foreach (var v in found) Console.WriteLine(v);
            _logger.Log($"Search '{query}', found {found.Count()}", true);
        }

        public void DisplayAllVisits()
        {
            if (!_visits.Any())
            {
                Console.WriteLine("No visits recorded.");
                return;
            }
            Console.WriteLine("Index | Visit");
            for (int i = 0; i < _visits.Count; i++)
                Console.WriteLine($"{i}: {_visits[i]}");
            _logger.Log($"Displayed {_visits.Count} visits", true);
        }

        public void FilterAndSort()
        {
            Console.Write("Filter by (1) Doctor, (2) Type, (3) Date Range: ");
            var f = Console.ReadLine();
            IEnumerable<Visit> res = _visits;

            if (f == "1")
            {
                Console.Write("Doctor: ");
                var d = Console.ReadLine();
                res = res.Where(v => v.DoctorName.Equals(d, StringComparison.OrdinalIgnoreCase));
            }
            else if (f == "2")
            {
                Console.Write("Visit Type: ");
                var t = Console.ReadLine();
                res = res.Where(v => v.VisitType.Equals(t, StringComparison.OrdinalIgnoreCase));
            }
            else if (f == "3")
            {
                Console.Write("From (yyyy-MM-dd): "); var fm = DateTime.Parse(Console.ReadLine());
                Console.Write("To: "); var tm = DateTime.Parse(Console.ReadLine());
                res = res.Where(v => v.VisitDateTime.Date >= fm && v.VisitDateTime.Date <= tm);
            }

            Console.Write("Sort by (1) Date, (2) Fee: ");
            var s = Console.ReadLine();
            if (s == "1") res = res.OrderBy(v => v.VisitDateTime);
            else if (s == "2") res = res.OrderBy(v => v.Fee);

            foreach (var v in res)
                Console.WriteLine(v);

            _logger.Log($"FilterSort filter={f}, sort={s}, found {res.Count()}", true);
        }

        public void ShowStatistics()
        {
            var stats = _visits
                .GroupBy(v => v.VisitType)
                .Select(g => new { Type = g.Key, Count = g.Count(), Total = g.Sum(v => v.Fee) });
            Console.WriteLine("\nSummary by Visit Type:");
            Console.WriteLine("Type\tCount\tTotal Fees");
            foreach (var st in stats)
                Console.WriteLine($"{st.Type}\t{st.Count}\t{st.Total}");
            _logger.Log("Displayed summary statistics", true);
        }
    }
}
