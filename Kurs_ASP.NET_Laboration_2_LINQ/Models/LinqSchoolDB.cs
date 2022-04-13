using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurs_ASP.NET_Laboration_2_LINQ.Models
{
    class LinqSchoolDB : DbContext
    {
        
        public DbSet<Course> Courses { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-59ADV49\\SQLEXPRESS; Initial Catalog = LinqSchoolDB; Integrated Security = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasOne(s => s.Student)
                .WithMany(sc => sc.StudentCourses)
                .HasForeignKey(si => si.StudentID);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(s => s.Course)
                .WithMany(sc => sc.StudentCourses)
                .HasForeignKey(ci => ci.CourseID);

        }
    }
}
