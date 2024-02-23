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
        public UserController(ILogger<UserController> logger, IUserInterface userHelperClass)
        {
            _logger = logger;
            _userHelperClass = userHelperClass;
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