using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using WebApi.Repositories;


namespace MVC.Controllers
{


    // [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserInterface _UserHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IHttpContextAccessor httpContextAccessor, IUserInterface Helper)
        {
            _httpContextAccessor = httpContextAccessor;
            _UserHelper = Helper;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }
<<<<<<<<< Temporary merge branch 1
        public IActionResult Index()
        {
            //Test Added
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            //Test Added
            return View();
        }
=========

        #region Login Methods

        #endregion


        #region Register Methods

        #endregion








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}