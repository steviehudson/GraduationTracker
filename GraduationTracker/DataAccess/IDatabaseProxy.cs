using System.Collections.Generic;
using GraduationTracker.Models;

namespace GraduationTracker.DataAccess
{
    public interface IDatabaseProxy
    {
        IEnumerable<Student> GetStudents();
        Student GetStudent(int id);

        IEnumerable<Diploma> GetDiplomas();
        Diploma GetDiploma(int id);

        IEnumerable<Requirement> GetRequirements();
        Requirement GetRequirement(int id);
    }
}