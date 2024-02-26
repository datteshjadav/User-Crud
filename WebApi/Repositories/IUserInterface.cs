using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IUserInterface
    {
        bool Login(LoginModel user);
        Register ApiLogin(LoginModel user);
        void SignOut();
        void Register(Register register);
    }
}