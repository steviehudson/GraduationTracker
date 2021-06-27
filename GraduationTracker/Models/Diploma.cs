namespace GraduationTracker.Models
{
    public class Diploma
    {
        public int Id { get; }
        public int Credits { get; }
        public int[] Requirements { get; }

        public Diploma(int id, int credits, int[] requirements)
        {
            Id = id;
            Credits = credits;
            Requirements = requirements;
        }

        //TODO any private setters required
    }
}
