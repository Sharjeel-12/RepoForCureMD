using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPatientProject.Models
{
    // Class for a patient 
    public class Patient
    {
        public string Name;
        public string Description;
        public DateOnly VisitDate;
        public TimeOnly VisitTime;
        public string VisitType;
        public string DoctorName;
        public Patient(string name, DateOnly visitdate, TimeOnly visittime, string visittype, string desc, string dr)
        {
           Name = name;
           Description = desc;
           VisitDate = visitdate;
           VisitTime = visittime;
           VisitType = visittype;
           DoctorName = dr;
        }

    }

}
