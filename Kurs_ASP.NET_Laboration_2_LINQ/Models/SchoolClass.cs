using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kurs_ASP.NET_Laboration_2_LINQ.Models
{
    public class SchoolClass
    {
        [Key]
        public int SchoolClassID { get; set; }
        [Required]
        public string SchoolClassName { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
