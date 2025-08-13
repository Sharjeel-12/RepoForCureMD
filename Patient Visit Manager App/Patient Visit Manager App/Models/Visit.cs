namespace Patient_Visit_Manager_App.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Duration { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public double Fee { get; set; }

    }
}
