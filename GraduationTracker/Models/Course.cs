namespace GraduationTracker.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Mark { get; set; }
        public int Credits { get; }

        public Course(int id, string name, int mark, int credits)
        {
            Id = id;
            Name = name;
            Mark = mark;
            Credits = credits;
        }
     }
}
