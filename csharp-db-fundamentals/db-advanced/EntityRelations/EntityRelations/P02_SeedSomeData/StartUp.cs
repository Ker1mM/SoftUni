using P01_StudentSystem.Data;
using P01_StudentSystem.Data.Models;
using System;
using System.Linq;

namespace P02_SeedSomeData
{
    class StartUp
    {
        static void Main(string[] args)
        {

            using (StudentSystemContext contex = new StudentSystemContext())
            {
                contex.Database.EnsureDeleted();
                contex.Database.EnsureCreated();

                Seed(contex);
            }
        }

        private static void Seed(StudentSystemContext context)
        {
            var students = new Student[]
            {
                new Student
                {
                    Name = "Ivancho",
                    PhoneNumber = "0876621928",
                    RegisteredOn = new DateTime(1978, 1, 1),
                    Birthday = new DateTime(1977, 12, 31)
                },

                new Student
                {
                    Name = "Penka",
                    RegisteredOn = new DateTime(2001, 1, 1),
                },

                new Student
                {
                    Name = "Gosho",
                    PhoneNumber = "0876621892",
                    RegisteredOn = DateTime.Now,
                    Birthday = new DateTime(2000, 1, 27)
                }
            };
            context.Students.AddRange(students);

            var courses = new Course[]
            {
                new Course
                {
                    Name = "C# DB Advanced",
                    Description = "Some staff!",
                    StartDate = new DateTime(2019, 1, 1),
                    EndDate = new DateTime(2019, 5, 12),
                    Price = 450.50m
                },

                new Course
                {
                    Name = "JS Basics",
                    Description = "Basic staff",
                    StartDate = new DateTime(2020, 2, 3),
                    EndDate = new DateTime(2020, 3, 2),
                    Price = 150.23m
                },

                new Course
                {
                    Name = "Algorithm",
                    Description = "Not so basic staff",
                    StartDate = new DateTime(2019, 3, 11),
                    EndDate = new DateTime(2019, 6, 22),
                    Price = 100
                }
            };
            context.Courses.AddRange(courses);

            var resources = new Resource[]
            {
                new Resource
                {
                    Name = "Do not know!",
                    Url = "www.whoknows.notme",
                    ResourceType = ResourceType.Other,
                    CourseId = courses[0].CourseId
                },

                new Resource
                {
                    Name = "Do not care!",
                    Url = "www.whocares.notme.again",
                    ResourceType = ResourceType.Video,
                    CourseId = courses[1].CourseId
                },

                new Resource
                {
                    Name = "Do not!",
                    Url = "www.what.eu",
                    ResourceType = ResourceType.Presentation,
                    CourseId = courses[2].CourseId
                },
            };
            context.Resources.AddRange(resources);

            var homeworks = new Homework[]
            {
                new Homework
                {
                    Content = "Whatever sails your boat",
                    ContentType = ContentType.Application,
                    SubmissionTime = DateTime.Now,
                    StudentId = students[0].StudentId,
                    CourseId = courses[0].CourseId
                },

                new Homework
                {
                    Content = "Whatever drives your car",
                    ContentType = ContentType.Pdf,
                    SubmissionTime = DateTime.Now,
                    StudentId = students[1].StudentId,
                    CourseId =  courses[0].CourseId
                },

                new Homework
                {
                    Content = "Whatever moves your feet",
                    ContentType = ContentType.Zip,
                    SubmissionTime = DateTime.Now,
                    StudentId = students[2].StudentId,
                    CourseId =  courses[0].CourseId
                }
            };
            context.HomeworkSubmissions.AddRange(homeworks);

            var studentCourses = new StudentCourse[]
            {
                new StudentCourse
                {
                    StudentId = students[0].StudentId,
                    CourseId = courses[0].CourseId
                },

                new StudentCourse
                {
                    StudentId = students[1].StudentId,
                    CourseId = courses[1].CourseId
                },

                new StudentCourse
                {
                    StudentId = students[2].StudentId,
                    CourseId = courses[2].CourseId
                }
            };
            context.StudentCourses.AddRange(studentCourses);

            context.SaveChanges();
        }
    }
}
