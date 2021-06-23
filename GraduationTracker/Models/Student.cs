using System.Collections.Generic;
using GraduationTracker.Utilities;

namespace GraduationTracker.Models
{
    public class Student
    {
        public int Id { get; set; }
        public IList<Course> Courses { get; set; } = new List<Course>();
        public Standing Standing { get; set; } = Standing.None;

        public Student(int id, Standing standing = Standing.None)
        {
            Id = id;
            Standing = standing;
        }
    }
}
