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
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentInterface _studrepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentController(ILogger<StudentController> logger, IStudentInterface studrepo, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _studrepo = studrepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get All Student Data(Display)
        [HttpGet]
        public IActionResult GetAllStudData()
        {
            var studData =  _studrepo.GetStudent();
            return View(studData);
        }

        //Get a Particular Student Details Using GetAllStudDetails():
        [HttpGet]
        public IActionResult GetAllStudDetails(int id)
        {
            var studDetail = _studrepo.GetOneStudent(id);
            return View(studDetail);
        }

        //Simply Redirect To AddStud Page:
        [HttpGet]
        public IActionResult AddStud()
        {
            return View();
        }

        //Add Student Data Using AddStud() And Redirect To GetAllStudData View Page:  
        [HttpPost]
        public IActionResult AddStud(StudentModel stud)
        {
            _studrepo.AddStudent(stud);
            return RedirectToAction("GetAllStudData");
        }

        //Simply Redirect To UpdateStud Page With Its ID:
        [HttpGet]
        public IActionResult UpdateStud(int id)
        {
            var studUpdate = _studrepo.GetOneStudent(id);
            return View(studUpdate);
        }

        //Update The Student Data Using UpdateStud():
        [HttpPost]
        public IActionResult UpdateStud(StudentModel stud)
        {
            _studrepo.UpdateStudent(stud);
            return RedirectToAction("GetAllStudData");
        }

        //Simply Redirect To The DeleteStud Page With Its ID:
        [HttpGet]
        public IActionResult DeleteStud(int id)
        {
            var studDlt = _studrepo.GetOneStudent(id);
            return View(studDlt);
        }  

        [HttpPost]
        public IActionResult DeleteStudConfirmed(int id)
        {
            _studrepo.DeleteStudent(id);
            return RedirectToAction("GetAllStudData");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}