using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Models;

namespace WebApi.Repositories
{
    public interface IUserInterface
    {
        List<Register> GetUserRegister();
    }
}