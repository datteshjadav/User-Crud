using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Repositories;

namespace MVC.Controllers
{
    // [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentInterface _studentInterface;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentController(ILogger<StudentController> logger, IStudentInterface studentInterface, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _studentInterface = studentInterface;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}