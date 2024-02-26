using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Models;

namespace MVC.Repositories
{
    public interface IUserInterface
    {
        bool Login(LoginModel user);
        void SignOut();
        void Register(Register register);
    }
}