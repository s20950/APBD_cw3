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

        public int CreateStudent(Student student)
        {
            var students = GetStudents();
            var s = students.Find(s => s.indexNumber.Equals(student.indexNumber));
            if (s != null)
                throw new DuplicateIndexException("Student with the same index already exists");
           
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
            File.AppendAllText(path, "\n" + st.ToString());

            return 1;
        }

        public int UpdateStudent(string studentIndex, Student student)
        {
            var students = File.ReadAllLines(path).Select(Student.FromCsv).ToList();
            bool studentFound = false;
            int studentListIndex = students.FindIndex(0, s => s.indexNumber.Equals(student.indexNumber));
            if (studentListIndex != -1)
            {
                students[studentListIndex] = student;
                StringBuilder sb = new StringBuilder();
                students.ForEach(s => sb.AppendLine(s.ToString()));
                string trimmedString = sb.ToString().Trim();
                File.WriteAllText(path, string.Join("\n", trimmedString.Split("\n")));

                return 1;
            }

            return 0;
        }

        public int DeleteStudent(string studentIndex)
        {
            var students = File.ReadAllLines(path).Select(Student.FromCsv).ToList();
            if (students.Remove(students.Find(s => s.indexNumber.Equals(studentIndex))))
            {
                StringBuilder sb = new StringBuilder();
                students.ForEach(s => sb.AppendLine(s.ToString()));
                string trimmedString = sb.ToString().Trim();
                File.WriteAllText(path, string.Join("\n", trimmedString.Split("\n")));
                return 1;
            }

            return 0;
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