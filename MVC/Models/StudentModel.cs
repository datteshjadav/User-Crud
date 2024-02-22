using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class StudentModel
    {
        public int c_studid {get;set;}
        public string? c_studname {get;set;}
        public int c_studage {get;set;}
        public int c_studphone {get;set;}
        public string[]? c_studcourse {get;set;}
        public string? c_studaddress {get;set;}
    }
}