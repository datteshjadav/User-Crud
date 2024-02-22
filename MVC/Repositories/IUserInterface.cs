using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Models;

namespace WebApi.Repositories
{
    public interface IUserInterface
    {
        bool Login(LoginModel user);
    }
}