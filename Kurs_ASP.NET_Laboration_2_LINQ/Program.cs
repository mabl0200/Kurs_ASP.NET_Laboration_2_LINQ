using Kurs_ASP.NET_Laboration_2_LINQ.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Kurs_ASP.NET_Laboration_2_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddSchoolClass(); /*Metod för att lägga till skolklass*/
            //AddTeacher(); /*Metod för att lägga till lärare*/
            //AddStudents(); /*Metod för att lägga till studenter*/
            //AddCourses(); /*Metod för att lägga till kurser*/

            bool keepLooping = true;
            while (keepLooping)
            {
                Console.WriteLine(" ---- MENYVAL ----");
                Console.WriteLine("1: Hämta alla lärare som undervisar i ett visst ämne");
                Console.WriteLine("2: Hämta alla elever med deras lärare");
                Console.WriteLine("3: Hämta alla elever som läser en viss kurs");
                Console.WriteLine("4: Editera ett ämne");
                Console.WriteLine("5: Uppdatera lärare till en viss kurs");
                Console.WriteLine("6: Avsluta");

                int choice = Int32.Parse(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1: //Hämta alla lärare som undervisar i ett visst ämne
                        try
                        {
                            Console.WriteLine("Välj kurs");
                            using (var db = new LinqSchoolDB())
                            {
                                IEnumerable<Course> courses = from c in db.Courses 
                                                              select c;

                                foreach (Course c in courses)
                                {
                                    Console.WriteLine($"Id: {c.CourseID} Namn: {c.CourseName}");
                                }

                                int courseChoice = Int32.Parse(Console.ReadLine());

                                var JoinCourse = (from c in db.Courses
                                                 join t in db.Teachers 
                                                 on c.TeacherID equals t.TeacherID
                                                 where c.CourseID == courseChoice
                                                 select new 
                                                 {
                                                     Course = c.CourseName,
                                                     Teacher = t.GetFullName()
                                                 }).ToList();

                                
                                foreach (var t in JoinCourse)
                                {
                                    Console.WriteLine($"{t.Teacher} undervisar i {t.Course}");
                                }
                            }
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error);
                            throw;
                        }
                        break;
                    case 2: //Hämta alla elever med deras lärare, skriv ut både elevernas namn och namnet på alla lärare de har
                        break;
                    case 3: //Hämta alla elever som läser en viss kurs och skriv ut deras namn samn vilken lärare de har i kursen
                        break;
                    case 4: //Ändra ett ämne från tex Programmering 2 till OOP
                        break;
                    case 5: //Ändra lärare på en kurs
                        break;
                    case 6:
                        keepLooping = false;
                        break;
                    default:
                        Console.WriteLine("Oj något blev fel, testa igen");
                        break;
                }

            }

            

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
        public static void AddStudents() /*Metod för att lägga till studenter*/
        {
            try
            {
                List<string> FirstNames = new List<string>() { "Alice", "Frans", "Olivia", "William", "Saga", "Elias", "Astrid", "Nils", "Julia", "Malte", "Isabella", "Oliver" };
                List<string> LastNames = new List<string>() { "Andersson", "Frisk", "Skog", "Eriksson", "Nordström", "Blank", "Blom", "Öberg", "Sjögren", "Eliasson", "Holm", "Berg" };
                int randomFirstName = new Random().Next(0, 12);
                int randomLastName = new Random().Next(0, 12);

                using LinqSchoolDB context = new LinqSchoolDB();

                Student student = new Student()
                {
                    FirstName = FirstNames[randomFirstName],
                    LastName = LastNames[randomLastName],
                    SchoolClassID = 3
                };

                context.Add(student);
                context.SaveChanges();
                Console.WriteLine($"Added {student.GetFullName()}");
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static void AddCourses() /*Metod för att lägga till kurser*/
        {
            try
            {
                using LinqSchoolDB context = new LinqSchoolDB();

                Course course = new Course()
                {
                    CourseName = "Fysik 1",
                    TeacherID = 3
                };

                context.Add(course);
                context.SaveChanges();
                Console.WriteLine($"Added course: {course.CourseName}");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
