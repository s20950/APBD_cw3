using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Cwiczenie3.Models
{
    public class Student
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string indexNumber { get; set; }
        public string birthDay { get; set; }
        public Course course { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StudyMode studyMode { get; set; }

        public string email { get; set; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }

        public Student()
        {
        }

        public Student(string firstName, string lastName, string indexNumber, string birthDay, Course course,
            StudyMode studyMode, string email, string mothersName, string fathersName)
        {
            this.firstName = firstName ?? throw new ArgumentNullException("firstName is required");
            this.lastName = lastName ?? throw new ArgumentNullException("lastName is required");
            this.indexNumber = indexNumber ?? throw new ArgumentNullException("indexNumber is required");
            this.birthDay = birthDay ?? throw new ArgumentNullException("birthDay is required");
            this.course = course ?? throw new ArgumentNullException("course is required");
            if (studyMode != StudyMode.DZIENNE || studyMode != StudyMode.INTERNETOWE || studyMode != StudyMode.ZAOCZNE)
                throw new InvalidEnumArgumentException("studyMode is required");
            this.studyMode = studyMode;
            this.email = email ?? throw new ArgumentNullException("email is required");
            this.mothersName = mothersName ?? throw new ArgumentNullException("mothersName is required");
            this.fathersName = fathersName ?? throw new ArgumentNullException("fathersName is required");
        }

        public static Student FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Student student = new Student();
            student.firstName = values[0];
            student.lastName = values[1];
            student.indexNumber = values[2];
            student.birthDay = values[3];
            student.course = new Course();
            student.course.courseName = values[4];
            student.studyMode = (StudyMode)Enum.Parse(typeof(StudyMode), values[5], true);
            student.email = values[6];
            student.mothersName = values[7];
            student.fathersName = values[8];
            return student;
        }

        public override string ToString()
        {
            return
                $"{firstName},{lastName},{indexNumber},{birthDay},{course.courseName},{studyMode},{email},{mothersName},{fathersName}";
        }
    }
}