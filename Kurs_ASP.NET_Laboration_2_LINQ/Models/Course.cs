using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kurs_ASP.NET_Laboration_2_LINQ.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        [Required]
        public string CourseName { get; set; }

        
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
