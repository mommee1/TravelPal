﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal.Manage
{
    public interface IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Countries Location { get; set; }

        public void IUser(string username, string password, Countries location)
        {

        }
    }
}
