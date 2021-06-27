using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using GraduationTracker.Models;
using GraduationTracker.Utilities;
using GraduationTracker.DataAccess;

namespace GraduationTracker
{
    public class GraduationTracker : IGraduationTracker
    {
        private IDatabaseProxy _dbProxy;

        public GraduationTracker(IDatabaseProxy dbProxy)
        {
            _dbProxy = dbProxy;
        }

        public StudentResult GetStudentResult(int diplomaId, int studentId)
        {
            Student student;
            Diploma diploma;

            try 
            {
                student = _dbProxy.GetStudent(studentId);
                diploma = _dbProxy.GetDiploma(diplomaId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("GraduationTracker > GetStudentResult", e);

                //TODO Messagebox
            }

            GetTotals(diploma, student, out var totalCredits, out var totalMarks);

            var hasGraduated = HasGraduated(totalMarks / student.Courses.Count);
            var standing = GetStanding(totalMarks / student.Courses.Count);

            return new StudentResult(totalCredits, hasGraduated, standing);
        }

       public void GetTotals(Diploma diploma, Student student, out int totalCredits, out int totalMarks)
        {
            totalCredits = 0;
            totalMarks = 0;

            foreach (var diplomaRequirement in diploma.Requirements)
            {
                Requirement requirement;
                try
                {
                    requirement = _dbProxy.GetRequirement(diplomaRequirement);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("GraduationTracker > GetStudentResult", e);

                    //TODO Messagebox
                }

                var studentCourse = student.Courses.FirstOrDefault(x => x.CourseId == requirement.CourseId);

                if (studentCourse != null)
                {
                    totalMarks += studentCourse.Mark;
                    if (studentCourse.Mark >= requirement.MinimumMark) totalCredits += requirement.Credits;
                }
            }
        }
       
       public bool HasGraduated(int average) => average >= 50;

       public Standing GetStanding(int average)
       {
           switch (average)
           {
                case < 50 :
                    return Standing.Remedial;
                case < 80 :
                    return Standing.Average;
                case < 95 :
                    return Standing.MagnaCumLaude;
                default :
                    return Standing.SumaCumLaude;
           }
       }
    }
}
