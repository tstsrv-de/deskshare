using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskShare.Models
{
    public class ViewFloor
    {
        public Floors _Floor { get; set; }
        public List<ViewRoom> _RoomsList { get; set; }
    }
}
