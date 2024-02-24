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
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserInterface _userHelperClass;
        private readonly IWebHostEnvironment _environment; 
        public UserController(ILogger<UserController> logger, IUserInterface userHelperClass,IWebHostEnvironment environment)
        {
            _logger = logger;
            _userHelperClass = userHelperClass;
             _environment = environment;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        #region Login Methods

        #endregion


        #region Register Methods

        // [Produces("application/json")]

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Register register)
        {
            //Code For File Upload:
            if (register.Image != null && register.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploadsimg");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + register.Image.FileName;
                //var uniqueFileName =  item.Image.FileName; //To Get Only File Name
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    register.Image.CopyTo(stream);
                }

                // Save The File Path To Our DB Table In c_image Field:
                register.c_image = uniqueFileName;
            }

            _userHelperClass.Register(register);
            return RedirectToAction("Index","Home");
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}