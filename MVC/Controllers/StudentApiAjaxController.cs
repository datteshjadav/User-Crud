using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using MVC.Repositories;

namespace MVC.Controllers
{
    // [Route("[controller]")]
    public class StudentApiAjaxController : Controller
    {
        private readonly ILogger<StudentAjaxController> _logger;
        private readonly IStudentInterface _studentInterface;


        public StudentApiAjaxController(ILogger<StudentAjaxController> logger, IStudentInterface studentInterface)
        {
            _logger = logger;
            _studentInterface = studentInterface;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetDropdownValue(){
            string[] course = _studentInterface.GetCourse();
            return Ok(course);
        }

        public IActionResult GetStudents(){
            List<StudentModel> students = _studentInterface.GetStudent();
            return Ok(students);
        }

        [HttpPost]
        public IActionResult addStudent(StudentModel student){
            Console.WriteLine("Name: "+student.c_studname);
            _studentInterface.AddStudent(student);
            return Ok("Student added successfully!!!");
        }
        
        [HttpGet]
        public IActionResult GetStudentDetails(int id){
            Console.WriteLine("ID: "+id);
            StudentModel student = _studentInterface.GetOneStudent(id);
            return Ok(student);
        }
        
        [HttpPut]
        public IActionResult UpdateStudent(StudentModel student){
            Console.WriteLine("Name: "+student.c_studname);
            _studentInterface.UpdateStudent(student);
            return Ok("Values updated");
        }
        
        [HttpDelete]
        public IActionResult DeleteStudent(int id){
             Console.WriteLine("ID: "+id);
            _studentInterface.DeleteStudent(id);
            return Ok("Student Deleted");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}