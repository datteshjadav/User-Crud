using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class StudentModel
    {
        public int c_studid {get;set;} = 0; 
        public string c_studname {get;set;} = string.Empty;
        public int c_studage {get;set;} = 0;
        public long c_studphone {get;set;} = 0;
        public string c_studcourse {get;set;} = string.Empty;
        public string c_studaddress {get;set;} = string.Empty;
    }
}