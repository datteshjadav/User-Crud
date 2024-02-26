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
        private readonly IUserInterface _UserHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserInterface _userHelperClass;
        private readonly IWebHostEnvironment _environment; 
        public UserController(IHttpContextAccessor httpContextAccessor, IUserInterface Helper, IUserInterface userHelperClass,IWebHostEnvironment environment)
        {
            _httpContextAccessor = httpContextAccessor;
            _UserHelper = Helper;
            _userHelperClass = userHelperClass;
             _environment = environment;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }
        # region Login Methods
        public IActionResult Login()
        {
            //Test Added
            var session = _httpContextAccessor.HttpContext.Session;
            if(session.GetInt32("userid") == null){
                return View();
            }else{
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost]
        //Reset to Original working Commit
        public IActionResult Login(LoginModel user)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            if (session.GetInt32("userid") == null)
            {
                if (_UserHelper.Login(user))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }


        }
        [HttpGet]
        public IActionResult SignOut()
        {
            _UserHelper.SignOut();
            return RedirectToAction("Login");
        }

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