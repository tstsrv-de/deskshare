using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskShare.Models
{
    public class ViewRoom
    {
        public Rooms _Room { get; set; }
        public List<ViewDesk> _DeskList { get; set; }
    }
}
