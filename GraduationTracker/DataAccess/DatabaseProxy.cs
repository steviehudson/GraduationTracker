using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationTracker.Models;
using Microsoft.VisualBasic;

namespace GraduationTracker.DataAccess
{
    public class DatabaseProxy : IDatabaseProxy
    {
        private string connString = ConfigurationManager.AppSettings["connectionString"];

        public IEnumerable<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            var ds = ReturnDataSet("[dbo].[GetRequirements]");

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
            List<Diploma> diplomas = new List<Diploma>();

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
            List<Requirement> requirements = new List<Requirement>();

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
