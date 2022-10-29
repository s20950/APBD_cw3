using System.Text;
using System.Text.RegularExpressions;
using Cwiczenie3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenie3.Services
{
    public class CsvDatabaseService : ICsvDatabaseService
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\students.csv");

        public CsvDatabaseService()
        {
            CreateDb(path);
        }

        public List<Student> GetStudents()
        {
            var students = File.ReadAllLines(path).Select(Student.FromCsv).ToList();
            return students;
        }

        public Student GetStudent(string studentIndex)
        {
            var student = File.ReadAllLines(path).Select(Student.FromCsv).ToList()
                .Find(v => v.indexNumber.Equals(studentIndex));
            return student;
        }

        public string CreateStudent(Student student)
        {
            var students = GetStudents();
            var s = students.Find(s => s.indexNumber.Equals(student.indexNumber));
            if (s != null)
                throw new DuplicateIndexException("Student with the same index already exists");
            var pattern = "s[0-9]+";
            var rx = new Regex(pattern);
            if (!Regex.IsMatch(student.indexNumber, pattern))
                throw new InvalidIndexException("Index has incorrect format");

            Student st = new Student(
                student.firstName,
                student.lastName,
                student.indexNumber,
                student.birthDay,
                student.course,
                student.studyMode,
                student.email,
                student.mothersName,
                student.fathersName
            );
            var studentToAdd = st.ToString();
            File.AppendAllText(path, "\n" + studentToAdd);

            return "Student added";
        }

        public string UpdateStudent(string studentIndex, Student student)
        {
            var students = File.ReadAllLines(path).Select(Student.FromCsv).ToList();
            foreach (var s in students)
            {
                if (s.indexNumber.Equals(studentIndex))
                {
                    s.firstName = student.firstName;
                    s.lastName = student.lastName;
                    s.birthDay = student.birthDay;
                    s.course = student.course;
                    s.studyMode = student.studyMode;
                    s.email = student.email;
                    s.mothersName = student.mothersName;
                    s.fathersName = student.fathersName;
                }
            }

            StringBuilder sb = new StringBuilder();
            students.ForEach(s => sb.AppendLine(s.ToString()));
            string trimmedString = sb.ToString().Trim();
            File.WriteAllText(path, string.Join("\n", trimmedString.Split("\n")));

            return "Student updated";
        }

        public string DeleteStudent(string studentIndex)
        {
            var students = File.ReadAllLines(path).Select(Student.FromCsv).ToList();
            if (students.Remove(students.Find(s => s.indexNumber.Equals(studentIndex))))
            {
                StringBuilder sb = new StringBuilder();
                students.ForEach(s => sb.AppendLine(s.ToString()));
                string trimmedString = sb.ToString().Trim();
                File.WriteAllText(path, string.Join("\n", trimmedString.Split("\n")));
                return "Student removed";
            }
            else
            {
                throw new NullReferenceException("Student doesn't exist");
            }
        }

        public void CreateDb(string path)
        {
            var directory = Path.GetDirectoryName(path);
            var file = Path.GetFileName(path);
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(path))
            {
                File.Create(path);
                File.AppendText("Jan,Kowalski,s1234,3/20/1991,Informatyka,Dzienne,kowalski@wp.pl,Jan,Anna");
                File.AppendText("Anna,Kowalska,s0001,3/20/1965,Informatyka,Dzienne,kowalski@wp.pl,Krzysztof,Buraczan");
            }
        }
    }
}