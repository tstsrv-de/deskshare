using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskShare.Models
{
    public class ViewDesk
    {
        public Desks _Desk { get; set; }
        public List<Bookings> _BookingsList { get; set; }
    }
}
