using Cwiczenie3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenie3.Services
{
    public interface ICsvDatabaseService
    {
        //crud
        List<Student> GetStudents();
        Student GetStudent(string studentIndex);
        int CreateStudent(Student student);
        int UpdateStudent(string studentIndex, Student student);
        int DeleteStudent(string studentIndex);
        void CreateDb(string path);

    }
}
