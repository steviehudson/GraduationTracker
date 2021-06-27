using System.Collections.Generic;
using GraduationTracker.Utilities;

namespace GraduationTracker.Models
{
    public class Student
    {
        public int Id { get; }
        public IList<Course> Courses { get; }

        public Student(int id, IList<Course> courses)
        {
            Id = id;
            Courses = courses;
        }

        //TODO any private setters required
    }
}
