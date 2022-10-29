using Cwiczenie3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenie3.Services
{
    public interface ICsvDatabaseService
    {
        //crud
        List<Student> GetStudents();
        Student GetStudent(string studentIndex);
        string CreateStudent(Student student);
        string UpdateStudent(string studentIndex, Student student);
        string DeleteStudent(string studentIndex);
        void CreateDb(string path);

    }
}
