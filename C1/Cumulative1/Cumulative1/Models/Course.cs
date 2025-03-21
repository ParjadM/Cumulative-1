namespace Cumulative1.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string? Coursecode { get; set; }
        public int Teacherid { get; set; } 
        public DateTime? Startdate { get; set; } 
        public DateTime? Finishdate { get; set; } 
        public string? Name { get; set; }
    }
}