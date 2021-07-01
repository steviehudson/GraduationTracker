using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using GraduationTracker.Models;

namespace GraduationTracker.DataAccess
{
    public class DatabaseProxy : IDatabaseProxy
    {
        private readonly string _connString = ConfigurationManager.AppSettings["connectionString"];

        #region Reads

        public IEnumerable<Student> GetStudents()
        {
            var ds = ReturnDataSet("[dbo].[GetStudents]");

            return (from DataRow row in ds.Tables[0].Rows
                let studentId = row.Field<int>("StudentId")
                let courses = (from course in ds.Tables[1].AsEnumerable() select new Course(course.Field<int>("ID"), course.Field<int>("CourseId"), course.Field<string>("Name"), course.Field<int>("Mark")))
                    .Where(x => x.Id == studentId)
                    .Cast<Course>()
                    .ToList()
                select new Student(row.Field<int>("ID"), courses)).ToList();
        }

        //TODO implement cache
        public Student GetStudent(int id)
        {
            var students = GetStudents();
            return students.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Diploma> GetDiplomas()
        {
            var ds = ReturnDataSet("[dbo].[GetDiplomas]");

            return (from DataRow row in ds.Tables[0].Rows
                let diplomaId = row.Field<int>("DiplomasId")
                let requirements = (from requirement in ds.Tables[1].AsEnumerable() select requirement.Field<int>("Requirement"))
                    .Where(x => x == diplomaId)
                    .Cast<int>()
                    .ToArray()
                select new Diploma(row.Field<int>("ID"), row.Field<int>("Credits"), requirements)).ToList();
        }

        //TODO implement cache
        public Diploma GetDiploma(int id)
        {
            var diplomas= GetDiplomas();
            return diplomas.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Requirement> GetRequirements()
        {
            IList<Requirement> requirements = new List<Requirement>();

            var ds = ReturnDataSet("[dbo].[GetRequirements]");

            return (from DataRow row in ds.Tables[0].Rows
                let requirementId = row.Field<int>("DiplomasId")
                let modules = (from requirement in ds.Tables[1].AsEnumerable() select requirement.Field<int>("ModuleId"))
                    .Where(x => x == requirementId)
                    .Cast<int>()
                    .ToArray()
                select new Requirement(row.Field<int>("ID"), row.Field<int>("CourseId"), row.Field<int>("MinimumMark"), row.Field<int>("Credits"), modules)).ToList();
        }

        #endregion

        //TODO implement cache
        public Requirement GetRequirement(int id)
        {
            var requirements = GetRequirements();
            return requirements.FirstOrDefault(x => x.Id == id);
        }

        #region Writes

        //TODO: inserts/updates

        #endregion

        #region SQL Methods

        public DataSet ReturnDataSet(string storedProc, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(_connString))
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

        #endregion
    }
}
