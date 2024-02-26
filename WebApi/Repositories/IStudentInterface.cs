using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IStudentInterface
    {
         string AddStudent(StudentModel student);
        string[] GetCourse();
        List<StudentModel> GetStudent();
        StudentModel GetOneStudent(int id);
        void UpdateStudent(StudentModel student);
        void DeleteStudent(int id);
    }
}