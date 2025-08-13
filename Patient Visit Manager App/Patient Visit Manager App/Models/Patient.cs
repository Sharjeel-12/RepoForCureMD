namespace Patient_Visit_Manager_App.Models
{
    public class Patient
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int VisitID { get; set; }
        public int PatientID {  get; set; }
        public string PatientEmail { get; set; }

    }
}
