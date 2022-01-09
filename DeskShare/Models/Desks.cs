using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeskShare.Models
{

    public class Desks
    {
         [Key]  public int _Id { get; set; }
         public int _RoomId { get; set; }
         public int _Order { get; set; }
        public string _Name { get; set; }
       public int _Screens { get; set; }
       public bool _Keyboard { get; set; }
      public bool _Mouse { get; set; }
    public bool _Docking { get; set; }
       public bool _Computer { get; set; }
        public bool _Stand { get; set; }
      

    }
}
