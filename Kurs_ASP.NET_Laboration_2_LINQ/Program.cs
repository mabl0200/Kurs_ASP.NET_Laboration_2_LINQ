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
            //AddSchoolClass(context); /*Metod för att lägga till skolklass*/
            //AddTeacher(context); /*Metod för att lägga till lärare*/
            //AddStudents(context); /*Metod för att lägga till studenter*/
            //AddCourses(context); /*Metod för att lägga till kurser*/
            //RemoveFromDataBase(context); /*Metod för att ta bort dubbletter i databas*/
            //AddStudentsToCourses(context); /*Metod för att lägga till studenter till kurser*/

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
                            GetCourses();
                            using (LinqSchoolDB context = new LinqSchoolDB())
                            {
                                int courseChoice = Int32.Parse(Console.ReadLine());

                                var JoinCourse = (from c in context.Courses
                                                 join t in context.Teachers 
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
                                Console.WriteLine(new string('-', 30));
                                Console.WriteLine("Tryck ENTER för att återgå till menyn");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error);
                            throw;
                        }
                        break;
                    case 2: //Hämta alla elever med deras lärare, skriv ut både elevernas namn och namnet på alla lärare de har
                        try
                        {
                            using (LinqSchoolDB context = new LinqSchoolDB())
                            {
                                var schoolClass = from sc in context.SchoolClasses
                                                  select sc;

                                Console.WriteLine("Välj klass: ");
                                foreach (SchoolClass sc in schoolClass)
                                {
                                    Console.WriteLine($"Id {sc.SchoolClassID} Namn: {sc.SchoolClassName}");
                                }

                                int classChoice = Int32.Parse(Console.ReadLine());

                                var studentsAndTeachers = (from sc in context.StudentCourses
                                                           join c in context.Courses
                                                           on sc.CourseID equals c.CourseID
                                                           join t in context.Teachers
                                                           on c.TeacherID equals t.TeacherID
                                                           join s in context.Students
                                                           on sc.StudentID equals s.StudentID
                                                           join sch in context.SchoolClasses
                                                           on s.SchoolClassID equals sch.SchoolClassID
                                                           where s.SchoolClassID == classChoice
                                                           select new
                                                           {
                                                               SchoolClass = sch.SchoolClassName,
                                                               Student = s.GetFullName(),
                                                               Teacher = t.GetFullName(),
                                                               Course = c.CourseName
                                                           }).ToList();

                                Console.Clear();
                                var header = String.Format("{0,-8}{1,-20}{2,-20}{3,-10}\n",
                                                             "Klass", "Elev", "Lärare", "Kurs");
                                var student = "";
                                Console.WriteLine(header);
                                foreach (var item in studentsAndTeachers)
                                {
                                    if (student == null)
                                    {
                                        student = item.Student;
                                        var output = String.Format("{0,-8}{1,-20}{2,-20}{3,-10}",
                                                                item.SchoolClass, item.Student, item.Teacher, item.Course);
                                        Console.WriteLine(output);
                                    }
                                    else if (student == item.Student)
                                    {
                                        var output = String.Format("{0,-8}{1,-20}{2,-20}{3,-10}",
                                                                item.SchoolClass, " ", item.Teacher, item.Course);
                                        Console.WriteLine(output);

                                    }
                                    else if (student != item.Student)
                                    {
                                        student = item.Student;
                                        var output = String.Format("{0,-8}{1,-20}{2,-20}{3,-10}",
                                                                item.SchoolClass, item.Student, item.Teacher, item.Course);
                                        Console.WriteLine(new string('-', 60));
                                        Console.WriteLine(output);

                                    }
                                    
                                }
                                Console.WriteLine(new string('-', 60));
                                Console.ReadLine();
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case 3: //Hämta alla elever som läser en viss kurs och skriv ut deras namn samn vilken lärare de har i kursen
                        try
                        {
                            using (LinqSchoolDB context = new LinqSchoolDB())
                            {
                                //var courses = from c in context.Courses
                                //select c;

                                //Console.WriteLine("Välj kurs: ");
                                //foreach (Course c in courses)
                                //{
                                //    Console.WriteLine($"Id: {c.CourseID} Namn: {c.CourseName}");
                                //}
                                GetCourses();
                                //PrintCourses(courses);
                                int courseChoice = Int32.Parse(Console.ReadLine());

                                var studentCourseTeacher = (from sc in context.StudentCourses
                                                            join s in context.Students
                                                            on sc.StudentID equals s.StudentID
                                                            join c in context.Courses
                                                            on sc.CourseID equals c.CourseID
                                                            join t in context.Teachers
                                                            on c.TeacherID equals t.TeacherID
                                                            where sc.CourseID == courseChoice
                                                            select new
                                                            {
                                                                Student = s.GetFullName(),
                                                                Teacher = t.GetFullName(),
                                                                Course = c.CourseName
                                                            }).ToList();
                                
                                foreach (var item in studentCourseTeacher)
                                {
                                    Console.WriteLine($"Student: {item.Student} Kurs: {item.Course} Lärare: {item.Teacher}");
                                    
                                }
                                Console.ReadLine();
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        break;
                    case 4: //Ändra ett ämne från tex Programmering 2 till OOP
                        try
                        {
                            
                            using (LinqSchoolDB context = new LinqSchoolDB())
                            {
                                GetCourses();
                                int courseChoice = Int32.Parse(Console.ReadLine());

                                var courseToChange = (from c in context.Courses
                                                     where c.CourseID == courseChoice
                                                     select c).ToList();

                                
                                Console.WriteLine("Vad ska det nya kursnamnet vara?");
                                string newCourseName = Console.ReadLine();

                                foreach (Course course in courseToChange)
                                {
                                    if (course.CourseID == courseChoice)
                                    {
                                        Console.WriteLine($"Vill du byta {course.CourseName} till {newCourseName}?");
                                        string confirm = Console.ReadLine().ToLower();
                                        switch (confirm)
                                        {
                                            case "j":
                                                course.CourseName = newCourseName;
                                                context.SaveChanges();

                                                Console.WriteLine($"Nytt kursnamn är {course.CourseName}");
                                                break;
                                            default:
                                                break;
                                        }
                                        
                                    }
                                }
                                Console.WriteLine(new string('-', 30));
                                Console.WriteLine("Tryck ENTER för att återgå till menyn");
                                Console.ReadLine();
                                Console.Clear();

                            }

                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error);
                            throw;
                        }
                        break;
                    case 5: //Ändra lärare på en kurs
                        try
                        {
                            using (LinqSchoolDB context = new LinqSchoolDB())
                            {
                                GetCourses();
                                int courseChoice = Int32.Parse(Console.ReadLine());

                                var courseToChange = (from c in context.Courses
                                                      join t in context.Teachers
                                                      on c.TeacherID equals t.TeacherID
                                                      where c.CourseID == courseChoice
                                                      select c).ToList();

                                Console.WriteLine($"Lärare som har kursen nu: {courseToChange[0].TeacherID}");
                                Console.WriteLine("Vilken lärare ska ta över denna kurs?");
                                GetTeachers();
                                int newTeacher = Int32.Parse(Console.ReadLine());
                                
                                Console.ReadLine();
                                foreach (Course c in courseToChange)
                                {
                                    if (c.CourseID == courseChoice)
                                    {
                                        Console.WriteLine($"Vill du byta {c.TeacherID} till {newTeacher}?");
                                        string confirm = Console.ReadLine().ToLower();
                                        switch (confirm)
                                        {
                                            case "j":
                                                c.TeacherID = newTeacher;
                                                context.SaveChanges();

                                                Console.WriteLine($"Ny kursansvarig är {c.TeacherID}");
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
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
        public static void GetTeachers()
        {
            try
            {
                using (LinqSchoolDB context = new LinqSchoolDB())
                {
                    List<Teacher> teachers = (from t in context.Teachers
                                              select t).ToList();

                    Console.WriteLine("Välj lärare: ");
                    foreach (Teacher t in teachers)
                    {
                        Console.WriteLine($"Id: {t.TeacherID} Namn: {t.GetFullName()}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void GetCourses()
        {
            try
            {
                using (LinqSchoolDB context = new LinqSchoolDB())
                {
                    List<Course> courses = (from c in context.Courses
                                            select c).ToList();

                    Console.WriteLine("Välj kurs: ");
                    foreach (Course c in courses)
                    {
                        Console.WriteLine($"Id: {c.CourseID} Namn: {c.CourseName}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void AddSchoolClass(LinqSchoolDB context) /*Metod för att lägga till skolklass*/
        {
            try
            {
                using (context)
                {
                    SchoolClass schoolClass = new SchoolClass()
                    {
                        SchoolClassName = "NV1C"
                    };

                    context.Add(schoolClass);
                    context.SaveChanges();
                    Console.WriteLine($"Added {schoolClass.SchoolClassName}");
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                throw;
            }
        }
        public static void AddTeacher(LinqSchoolDB context) /*Metod för att lägga till lärare*/
        {
            try
            {
                List<string> FirstNames = new List<string>() { "Johanna", "Simon", "Camilla", "Daniel", "Susanna", "Marcus", "Cecilia", "Richard", "Agnes", "Per", "Petra", "Thomas" };
                List<string> LastNames = new List<string>() { "Almqvist", "Backlund", "Brolin", "Claesson", "Ekdahl", "Halvarsson", "Lundin", "Schmidt", "Vallin", "Eliasson", "Östlund", "Ali" };
                int randomFirstName = new Random().Next(0, 12);
                int randomLastName = new Random().Next(0, 12);

                using (context)
                {
                    Teacher teacher = new Teacher()
                    {
                        FirstName = FirstNames[randomFirstName],
                        LastName = LastNames[randomLastName]

                    };

                    context.Add(teacher);
                    context.SaveChanges();
                    Console.WriteLine($"Added {teacher.GetFullName()}");
                } 

            }
            catch (Exception)
            {
                throw;
            }

        }
        public static void AddStudents(LinqSchoolDB context) /*Metod för att lägga till studenter*/
        {
            try
            {
                List<string> FirstNames = new List<string>() { "Alice", "Frans", "Olivia", "William", "Saga", "Elias", "Astrid", "Nils", "Julia", "Malte", "Isabella", "Oliver" };
                List<string> LastNames = new List<string>() { "Andersson", "Frisk", "Skog", "Eriksson", "Nordström", "Blank", "Blom", "Öberg", "Sjögren", "Eliasson", "Holm", "Berg" };
                int randomFirstName = new Random().Next(0, 12);
                int randomLastName = new Random().Next(0, 12);

                using (context)
                {
                    Student student = new Student()
                    {
                        FirstName = FirstNames[randomFirstName],
                        LastName = LastNames[randomLastName],
                        SchoolClassID = 1
                    };

                    context.Add(student);
                    context.SaveChanges();
                    Console.WriteLine($"Added {student.GetFullName()}");
                } 
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static void AddCourses(LinqSchoolDB context) /*Metod för att lägga till kurser*/
        {
            try
            {
                using (context)
                {
                    Course course = new Course()
                    {
                        CourseName = "Fysik 1",
                        TeacherID = 3
                    };

                    context.Add(course);
                    context.SaveChanges();
                    Console.WriteLine($"Added course: {course.CourseName}");

                } 
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void AddStudentsToCourses(LinqSchoolDB context)
        {
            try
            {
                using (context)
                {
                    var schoolClass = (from sc in context.Students
                                      where sc.SchoolClassID == 3
                                      select sc).ToList();

                    foreach (Student s in schoolClass)
                    {
                        StudentCourse studentCourse = new StudentCourse()
                        {
                            StudentID = s.StudentID,
                            CourseID = 6
                        };
                        context.Add(studentCourse);
                        context.SaveChanges();
                        Console.WriteLine($"Adderade {studentCourse.StudentID} till {studentCourse.CourseID} ");
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                throw;
            }
        }
        public static void RemoveFromDataBase(LinqSchoolDB context)
        {
            try
            {
                using (context)
                {
                    var student = context.Students.Where(s => s.StudentID == 4).FirstOrDefault();

                    if (student is Student)
                    {
                        context.Remove(student);
                    }
                    context.SaveChanges();

                    Console.WriteLine($"Tog bort {student.GetFullName()} från databas");
                    Console.ReadLine();

                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }
}
