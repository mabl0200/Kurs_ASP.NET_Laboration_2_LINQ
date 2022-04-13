using System;
using System.Collections.Generic;
using System.Text;

namespace Kurs_ASP.NET_Laboration_2_LINQ.Models
{
    public class StudentCourse
    {
        public int ID { get; set; }

        public int StudentID { get; set; }
        public Student Student { get; set; }

        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
