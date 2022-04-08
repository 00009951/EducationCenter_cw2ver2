using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationCenter_cw2.Models
{
    public class Student
    {
        public int? studentId { get; set; }
        [DisplayName("Name")]
       
        public string name { get; set; }
        [DisplayName("Phone Number")]
        public string phoneNumber { get; set; }

    }
}