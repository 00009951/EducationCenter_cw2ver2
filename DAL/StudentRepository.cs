using EducationCenter_cw2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace EducationCenter_cw2.DAL
{
    public class StudentRepository : IStudentRepository
    {
        private string ConnStr
        {
            get{ return WebConfigurationManager.ConnectionStrings["EducationCenterConnStr"].ConnectionString; }
        }
        public IList<Student> GetAll()
        {
            IList<Student> students = new List<Student>();
            using (var conn = new SqlConnection(ConnStr)) 
            {
                using (var cmd = conn.CreateCommand()) 
                {
                    cmd.CommandText = @"select [studentId], [name], [phoneNumber]
                                      from [dbo].[student]";
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader()) 
                    {
                        while (rdr.Read()) 
                        {
                            var std = new Student();
                            std.studentId = rdr.GetInt32( rdr.GetOrdinal("studentId"));

                            if(!rdr.IsDBNull(rdr.GetOrdinal("name")))
                            std.name = rdr.GetString(rdr.GetOrdinal("name"));
                            std.phoneNumber = rdr.GetString(rdr.GetOrdinal("phoneNumber"));

                            students.Add(std);
                        }
                    }
                }
            }
            return students;
        }
    }
}