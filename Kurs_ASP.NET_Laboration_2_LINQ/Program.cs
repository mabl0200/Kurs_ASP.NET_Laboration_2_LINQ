using Kurs_ASP.NET_Laboration_2_LINQ.Models;
using System;
using System.Collections.Generic;

namespace Kurs_ASP.NET_Laboration_2_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddSchoolClass(); /*Metod för att lägga till skolklass*/
            //AddTeacher(); /*Metod för att lägga till lärare*/
        }
        public static void AddSchoolClass() /*Metod för att lägga till skolklass*/
        {
            try
            {
                using LinqSchoolDB context = new LinqSchoolDB();

                SchoolClass schoolClass = new SchoolClass()
                {
                    SchoolClassName = "NV1C"
                };

                context.Add(schoolClass);
                context.SaveChanges();
                Console.WriteLine($"Added {schoolClass.SchoolClassName}");

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void AddTeacher() /*Metod för att lägga till lärare*/
        {
            try
            {
                List<string> FirstNames = new List<string>() { "Johanna", "Simon", "Camilla", "Daniel", "Susanna", "Marcus", "Cecilia", "Richard", "Agnes", "Per", "Petra", "Thomas" };
                List<string> LastNames = new List<string>() { "Almqvist", "Backlund", "Brolin", "Claesson", "Ekdahl", "Halvarsson", "Lundin", "Schmidt", "Vallin", "Eliasson", "Östlund", "Ali" };
                int randomFirstName = new Random().Next(0, 12);
                int randomLastName = new Random().Next(0, 12);

                using LinqSchoolDB context = new LinqSchoolDB();

                Teacher teacher = new Teacher()
                {
                    FirstName = FirstNames[randomFirstName],
                    LastName = LastNames[randomLastName]
                    
                };

                context.Add(teacher);
                context.SaveChanges();
                Console.WriteLine($"Added {teacher.GetFullName()}");

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
