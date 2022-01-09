using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskShare.Models
{
    public class Filter
    {
        public bool _FreeToday { get; set; }
        public bool _FreeTomorrow { get; set; }
        public bool _FreeWeek { get; set; }

        public bool _Mouse { get; set; }
        public bool _Computer { get; set; }
        public bool _Docking { get; set; }
        public bool _Keyboard { get; set; }
        public bool _NoScreen { get; set; }
        public bool _OneScreen { get; set; }
        public bool _TwoScreens { get; set; }
        public bool _ThreeScreens { get; set; }
    }
}
