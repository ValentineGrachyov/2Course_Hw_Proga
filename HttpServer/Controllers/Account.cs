using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Attributes;


    
namespace HttpServer.Controllers
{
    
    public class Account
    {    
        public Int32 Id { get; set; }        
        public string Name { get; set; }
        public string Password { get; set; }

        public Account(int id, string name, string pass)
        {
            Id = id;
            Name = name;
            Password = pass;
        }
    }

}
