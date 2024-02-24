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
    public class StudentAjaxController : Controller
    {
        private readonly ILogger<StudentAjaxController> _logger;
        private readonly IStudentInterface _studentInterface;


        public StudentAjaxController(ILogger<StudentAjaxController> logger, IStudentInterface studentInterface)
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
            return Json(course);
        }

        public IActionResult GetStudents(){
            List<StudentModel> students = _studentInterface.GetStudent();
            return Json(students);
        }

        [HttpPost]
        public IActionResult addStudent(StudentModel student){
            // Console.WriteLine("Name: "+student.c_studname);
            string message = _studentInterface.AddStudent(student);
            return Json(message);
        }
        [HttpGet]
        public IActionResult GetStudentDetails(int id){
            // Console.WriteLine("Name: "+student.c_studname);
            StudentModel student = _studentInterface.GetOneStudent(id);
            return Json(student);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}