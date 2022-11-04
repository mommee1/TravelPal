using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal.Manage
{
    public class Trip : Travel
    {
        public TripTypes TripType;
        public Trip(TripTypes tripType, string destination, Countries country, int travellers) : base(destination, country, travellers)
        {
            TripType = tripType;
        }

        //public string  GetInfo()
        //{
        //    return $"";
        //}
    }
}