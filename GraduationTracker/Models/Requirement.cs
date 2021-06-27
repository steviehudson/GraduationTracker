namespace GraduationTracker.Models
{
    public class Requirement
    {
        public int Id { get; }
        public int CourseId { get; }
        public int MinimumMark { get; }
        public int Credits { get; }
        public int[] Modules { get; }

        public Requirement(int id, int courseId, int minimumMark, int credits, int[] modules)
        {
            Id = id;
            CourseId = courseId;
            MinimumMark = minimumMark;
            Credits = credits;
            Modules = modules;
        }

        //TODO any private setters required
    }
}
