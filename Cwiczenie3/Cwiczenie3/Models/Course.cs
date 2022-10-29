using System.ComponentModel.DataAnnotations;

namespace Cwiczenie3.Models
{
    public class Course
    {
        [Required]
        public string courseName { get; set; }

        public Course(string courseName)
        {
            this.courseName = courseName;
        }
    }
}
