using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPal.Manage;
public class TravelManager
{
    List<Travel> travels = new();

    public void Addtravel(Travel travel)
    {
        travels.Add(travel);
    }

    public void RemoveTravel(Travel travel)
    {
        travels.Remove(travel);
    }
}

