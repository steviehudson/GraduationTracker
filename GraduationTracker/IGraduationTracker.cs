using GraduationTracker.Models;
using GraduationTracker.Utilities;

namespace GraduationTracker
{
    public interface IGraduationTracker
    {
        StudentResult GetStudentResult(int diplomaId, int studentId);
        void GetTotals(Diploma diploma, Student student, out int totalCredits, out int totalMarks);
        bool HasGraduated(int average);
        Standing GetStanding(int average);
    }
}