using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
    public string Major { get; set; }
    public double Tuition { get; set; }
}
public class StudentClubs
{
    public int StudentID { get; set; }
    public string ClubName { get; set; }
}
public class StudentGPA
{
    public int StudentID { get; set; }
    public double GPA { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Student collection
        IList<Student> studentList = new List<Student>() {
            new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
            new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
            new Student() { StudentID = 3, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
            new Student() { StudentID = 4, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
            new Student() { StudentID = 5, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
            new Student() { StudentID = 6, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
            new Student() { StudentID = 7, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
        };
        // Student GPA Collection
        IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
            new StudentGPA() { StudentID = 1,  GPA=4.0} ,
            new StudentGPA() { StudentID = 2,  GPA=3.5} ,
            new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
            new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
            new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
            new StudentGPA() { StudentID = 6,  GPA=2.5} ,
            new StudentGPA() { StudentID = 7,  GPA=1.0 }
        };
        // Club collection
        IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
        };

        //Group by GPA and display the student's IDs
        Console.WriteLine("");
        Console.WriteLine("~Group by GPA and display the student's IDs:");
        Console.WriteLine("");
        var groupByGPA = from s in studentGPAList
                         group s by s.GPA;

        foreach (var group in groupByGPA)
        {
            Console.WriteLine($"GPA: {group.Key}");
            foreach (var student in group)
            {
                Console.WriteLine($"StudentID: {student.StudentID}");
            }
        }

        //Sort by Club, then group by Club and display the student's IDs
        Console.WriteLine("");
        Console.WriteLine("~Sort by Club, then group by Club and display the student's IDs:");
        Console.WriteLine("");
        var sortedByClub = from c in studentClubList
                           orderby c.ClubName
                           group c by c.ClubName;
                            
        foreach (var group in sortedByClub)
        {
            Console.WriteLine($"Club: {group.Key}");
            foreach (var student in group)
            {
                Console.WriteLine($"   StudentID: {student.StudentID}");
            }
        }

        //Count the number of students with a GPA between 2.5 and 4.0
        var numOfStudent = studentGPAList.Count(s => s.GPA >= 2.5 && s.GPA <= 4.0);
        Console.WriteLine("");
        Console.WriteLine($"~Number of students with a GPA between 2.5 and 4.0:  {numOfStudent}");
        Console.WriteLine("");

        //Average all student's tuition
        var Avg = studentList.Average(s => s.Tuition);
        Console.WriteLine("");
        Console.WriteLine($"~Average all student's tuition: {Avg}");
        Console.WriteLine("");

        //Find the student paying the most tuition and display their name, major, and tuition
        Console.WriteLine("");
        Console.WriteLine("~Student paying the most tuition:");
        Console.WriteLine("");
        var studentWithHighestTuition = studentList.OrderByDescending(s => s.Tuition).First();
        Console.WriteLine($"   Name: {studentWithHighestTuition.StudentName}");
        Console.WriteLine($"   Major: {studentWithHighestTuition.Major}");
        Console.WriteLine($"   Tuition: {studentWithHighestTuition.Tuition}");

        //Join the student list and student GPA list on student ID and display the student's name, major, and GPA
        Console.WriteLine("");
        Console.WriteLine("~Joined list of student name, major, and GPA:");
        Console.WriteLine("");
        var joinedList = studentList.Join(studentGPAList,
                                           s => s.StudentID,
                                           g => g.StudentID,
                                           (s, g) => new { s.StudentName, s.Major, g.GPA });
        foreach (var student in joinedList)
        {
            Console.WriteLine($"   Name: {student.StudentName}, Major: {student.Major}, GPA: {student.GPA}");
        }

        //Join the student list and student club list. Display the names of only those students who are in the Game club.
        Console.WriteLine("");
        Console.WriteLine("~Names of students who are in the Game club:");
        Console.WriteLine("");
        var gameClubMembers = studentList.Join(studentClubList,
                                               s => s.StudentID,
                                               c => c.StudentID,
                                               (s, c) => new { s.StudentName, c.ClubName })
                                         .Where(sc => sc.ClubName == "Game");
        foreach (var member in gameClubMembers)
        {
            Console.WriteLine($"   {member.StudentName}");
        }
    }
}
