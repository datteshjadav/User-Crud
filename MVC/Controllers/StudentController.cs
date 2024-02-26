using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MVC.Controllers
{
    // [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public StudentController(ILogger<StudentController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            if (session.GetInt32("userid") != null){
                return View();
            }else{
                return RedirectToAction("Login", "User");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}