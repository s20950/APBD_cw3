using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Cwiczenie3.Models
{
    public class Student
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        [RegularExpression("s[0-9]+", ErrorMessage = @"Index should start with s and have at least one number")]
        public string indexNumber { get; set; }
        [Required]
        public string birthDay { get; set; }
        [Required]
        public Course course { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StudyMode studyMode { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        public string mothersName { get; set; }
        [Required]
        public string fathersName { get; set; }

        public Student(string firstName, string lastName, string indexNumber, string birthDay, Course course, StudyMode studyMode, string email, string mothersName, string fathersName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.indexNumber = indexNumber;
            this.birthDay = birthDay;
            this.course = course;
            this.studyMode = studyMode;
            this.email = email;
            this.mothersName = mothersName;
            this.fathersName = fathersName;
        }

        public static Student FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Student student = new Student(
            values[0],
            values[1],
            values[2],
            values[3],
            new Course(values[4]),
            (StudyMode)Enum.Parse(typeof(StudyMode), values[5], true),
            values[6],
            values[7],
            values[8]);
            return student;
        }
        
        public override string ToString()
        {
            return
                $"{firstName},{lastName},{indexNumber},{birthDay},{course.courseName},{studyMode},{email},{mothersName},{fathersName}";
        }
    }
}