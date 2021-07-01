using GraduationTracker.Utilities;

namespace GraduationTracker.Models
{
    public class StudentResult
    {
        public int TotalCredits { get; }
        public bool HasGraduated { get; }
        public Standing Standing { get; }

        public StudentResult(int totalCredits, bool hasGraduated, Standing standing)
        {
            TotalCredits = totalCredits;
            HasGraduated = hasGraduated;
            Standing = standing;
        }
    }

    //TODO any private setters required
}
