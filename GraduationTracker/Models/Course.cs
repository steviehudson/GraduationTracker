namespace GraduationTracker.Models
{
    public class Course
    {
        public int Id { get; }
        public int CourseId { get; }
        public string Name { get; }
        public int Mark { get; }

        public Course(int id, int courseId, string name, int mark)
        {
            Id = id;
            CourseId = courseId;
            Name = name;
            Mark = mark;
        }

        //TODO any private setters required
     }
}
