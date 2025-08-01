using System;

namespace PatientVisitManager.Models
{
    public class Visit
    {
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string VisitType { get; set; }
        public DateTime VisitDateTime { get; set; }
        public int DurationInMinutes { get; set; }
        public int Fee { get; set; }

        public override string ToString() =>
            $"{PatientName},{DoctorName},{VisitType},{VisitDateTime:yyyy‑MM‑dd HH:mm},{DurationInMinutes},{Fee}";

        public static Visit FromCsv(string line)
        {
            var p = line.Split(',');
            return new Visit
            {
                PatientName = p[0],
                DoctorName = p[1],
                VisitType = p[2],
                VisitDateTime = DateTime.ParseExact(p[3], "yyyy-MM-dd HH:mm", null),
                DurationInMinutes = int.Parse(p[4]),
                Fee = int.Parse(p[5])
            };
        }

        public Visit Clone() => (Visit)MemberwiseClone();
    }
}
