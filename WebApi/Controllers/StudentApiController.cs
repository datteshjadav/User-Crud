using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Repositories;
using WebApi.Models;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentApiController : ControllerBase
    {
        private IStudentInterface _studrepo;
        public StudentApiController(IStudentInterface studrepo)
        {
            _studrepo = studrepo;
        }
        [HttpGet]
        [Route("getall")]
        public List<StudentModel> GetStudents()
        {
            return _studrepo.GetStudent();
        }
        // public IActionResult GetAllStudent(){  
        //     try{
        //     var ans= _studrepo.GetAllStudent();
        //     Console.WriteLine("Inside the StudentAPI controller,GetAllStudent",ans);
        //     return Ok(ans);
        //      }
        //     catch(Exception e){
        //         Console.WriteLine("Errror at getAllstudent",e);
        //     }
        //     return BadRequest("");
        // }
        [HttpGet]
        [Route("Getstudentbyid")]
        public ActionResult<StudentModel> GetStudentByID(int id)
        {
            return _studrepo.GetOneStudent(id);
        }
        [HttpPost]
        [Route("AddStudent")]
        public IActionResult AddStudent(StudentModel student)
        {
            _studrepo.AddStudent(student);
            Console.WriteLine("Student Added");
            return Ok("Student Added Successfully!!!");
        }
        [HttpGet]
        public string[] GetDropdownValue()
        {
            return _studrepo.GetCourse();
        }
        [HttpPut]
        [Route("UpdateStudent")]
        public IActionResult UpdateStudent(StudentModel student)
        {
            _studrepo.UpdateStudent(student);
            return Ok("Student Updated Successfully!!!");
        }
        [HttpDelete]
        [Route("DeleteStudent/id/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _studrepo.DeleteStudent(id);
            return Ok("Student Deleted Successfully!!!");
        }

    }
}