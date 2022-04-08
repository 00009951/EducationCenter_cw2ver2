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
            get { return WebConfigurationManager.ConnectionStrings["EducationCenterConnStr"].ConnectionString; }
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
                            std.studentId = rdr.GetInt32(rdr.GetOrdinal("studentId"));

                            if (!rdr.IsDBNull(rdr.GetOrdinal("name")))
                                std.name = rdr.GetString(rdr.GetOrdinal("name"));
                            std.phoneNumber = rdr.GetString(rdr.GetOrdinal("phoneNumber"));

                            students.Add(std);
                        }
                    }
                }
            }
            return students;
        }

        public Student GetById(int id)
        {
            using(var conn = new SqlConnection(ConnStr))
            {
                using(var cmd = conn.Create.Command())
                {
                    cmd.CommantText = @"SELECT [StudentId]
                                              ,[Name]
                                              ,[PhoneNumber]
                                        FROM [dbo].[Student]
                                        WHERE StudentId = @StudentId
                                        ";
                    cmd.Parameters.AddWithValue("@StudentId", id);

                    conn.Open();
                    using(var rdr = cmd.ExecuteReader())
                    {
                        if(rdr.Read())
                        {
                            student = new Student()
                            {
                                StudentId = id,
                                Name = rdr.GetString(rdr.GetOrdinal("Name")),
                                PhoneNumber = rdr.GetString(rdr.GetOrdinal("Phone Number")),
                            }
                        }
                    }

                }
            }
            return student;
        }

        public void Insert(Student emp)
        {
            using (var conn = new SqlConnection(ConnStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [DBO].[Student]
                                                           ([StudentID]
                                                            ,[Name]
                                                            ,[PhoneNumber]
                                                            )
                                                        VALUES
                                                            (
                                                              @StudentID
                                                              @Name
                                                              @PhoneNumber
                                                              )";
                    var pStudentID = cmd.CreateParameter();
                    pStudentID.ParameterName = "pStudentID";
                    pStudentID.Value = emp.studentId;
                    pStudentID.Direction = System.Data.ParameterDirection.Input;
                    pStudentID.DbType = System.Data.DbType.AnsiString;
                    cmd.Parameters.Add(pStudentID);

                    cmd.Parameters.AddWithValue("@Name", emp.name);
                    cmd.Parameters.AddWithValue("@PhoneNumber", emp.phoneNumber);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Student emp)
        {
            using(var conn = new SqlConnection(ConnStr))
            {
                using (var cmd = conn.Create.Command())
                {
                    cmd.CommandText = @"UPDATE [dbo].[Student]
                                                SET [Name] = @Name
                                                ,[PhoneNumber] = @PhoneNumber
                                                 WHERE StudentId = [StudentId] = @StudentId";

                    cmd.Parameters.Add(pStudentID);

                    cmd.Parameters.AddWithValue("@Name", emp.name);
                    cmd.Parameters.AddWithValue("@PhoneNumber", emp.phoneNumber);
                    cmd.Parameters.AddWithValue("@StudentId", emp.StudentId);

                    conn.Open();
                    cmd.ExecuteNonQuiery();
                }
            }
        }

    }
}