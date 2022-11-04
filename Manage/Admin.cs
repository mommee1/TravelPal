using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;
using TravelPal.Manage;

namespace TravelPal.Manage
{
    public class Admin : IUser
    {
     
        public string Username { get; set ; }
        public string Password { get; set; }
        public Countries Location { get ; set ; }
    }
}
