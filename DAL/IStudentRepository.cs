using EducationCenter_cw2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationCenter_cw2.DAL
{
    public interface IStudentRepository
    {
        IList<Student> GetAll();

        void Insert(Student emp);
        Student GetById(int id);

        void update(Student emp);
        void Delete(int id);
    }
}
