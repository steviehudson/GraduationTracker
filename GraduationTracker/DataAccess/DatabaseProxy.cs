using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationTracker.Models;
using GraduationTracker.Utilities;
using Microsoft.VisualBasic;

namespace GraduationTracker.DataAccess
{
    public class DatabaseProxy : IDatabaseProxy
    {
        private string connString = ConfigurationManager.AppSettings["connectionString"];

        public IEnumerable<Student> GetStudents()
        {
            IList<Student> students = new List<Student>();

            var ds = ReturnDataSet("[dbo].[GetRequirements]");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var studentId = row.Field<int>("StudentId");

                var student = new Student(row.Field<int>("ID"), (Standing) row.Field<int>("Standing"))
                {
                    Courses = (from course in ds.Tables[2].AsEnumerable()
                            select new Course(
                                course.Field<int>("ID"),
                                course.Field<string>("Name"),
                                course.Field<int>("Mark"),
                                course.Field<int>("Credits")
                            ))
                        .Where(x => x.Id = studentId)
                        .Cast<Course>()
                        .ToList()

                };
                students.Add(student);
            }
            return students;
        }

        public Student GetStudent(int id)
        {
            Student student = new Student();

            SqlParameter[] parameters = { new SqlParameter("@ID", id) };
            var ds = ReturnDataSet("[dbo].[GetStudents]", parameters);

            return student;
        }

        public IEnumerable<Diploma> GetDiplomas()
        {
            IList<Diploma> diplomas = new List<Diploma>();

            var ds = ReturnDataSet("[dbo].[GetDiplomas]");

            return diplomas;
        }

        public Diploma GetDiploma(int id)
        {
            Diploma diploma = new Diploma();

            SqlParameter[] parameters = { new SqlParameter("@ID", id) };
            var ds = ReturnDataSet("[dbo].[GetStudents]", parameters);

            return diploma;
        }

        public IEnumerable<Requirement> GetRequirements()
        {
            IList<Requirement> requirements = new List<Requirement>();

            var ds = ReturnDataSet("[dbo].[GetRequirements]");

            return requirements;
        }

        public Requirement GetRequirement(int id)
        {
            Requirement requirement = new Requirement();

            SqlParameter[] parameters = { new SqlParameter("@ID", id)};
            var ds = ReturnDataSet("[dbo].[GetRequirements]", parameters);

            return requirement;

        }

        public DataSet ReturnDataSet(string storedProc, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //conn.Open();
                var cmd = new SqlCommand(storedProc, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (parameters != null) cmd.Parameters.AddRange(parameters);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);

                return ds;
            }
        }
    }
}
