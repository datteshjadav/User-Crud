using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserApiController : ControllerBase
    {
        private IUserInterface _iuserrepo;
        private readonly IWebHostEnvironment _environment; 

        public UserApiController(IUserInterface iuserrepo,IWebHostEnvironment environment)
        {
            _iuserrepo = iuserrepo;
            _environment = environment;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromForm]Register register)
        {
             //Code For File Upload:
            if (register.Image != null && register.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine("D:/Case Point/Intern_Jemin/C#/CoreMVC/Git Project/User-Crud/MVC/wwwroot/", "uploadsimg");
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
            _iuserrepo.Register(register);
            return Ok("User Registerd Successfully");
        }

    }
}