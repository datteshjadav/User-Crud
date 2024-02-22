using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Models;

namespace MVC.Repositories
{
    public interface IStud
    {
        string AddStudent(StudentModel student);
        string[] GetCourse();
        List<StudentModel> GetStudent();
        StudentModel GetOneStudent(int id);
        void UpdateStudent(StudentModel student);
        void DeleteStudent(int id);
    }
}