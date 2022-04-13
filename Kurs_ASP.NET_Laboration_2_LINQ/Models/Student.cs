using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kurs_ASP.NET_Laboration_2_LINQ.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }


        public int SchoolClassID { get; set; }
        public SchoolClass SchoolClass { get; set; }


        public virtual ICollection<StudentCourse> StudentCourses { get; set; }


        public string GetFullName()
        {

            return $"{FirstName} {LastName}";
        }
    }
}
