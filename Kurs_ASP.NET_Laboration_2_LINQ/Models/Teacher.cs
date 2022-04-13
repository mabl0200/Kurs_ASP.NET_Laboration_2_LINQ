using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kurs_ASP.NET_Laboration_2_LINQ.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public ICollection<Course> Courses { get; set; }

        public string GetFullName()
        {

            return $"{FirstName} {LastName}";
        }
    }
}
