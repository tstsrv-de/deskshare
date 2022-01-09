using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeskShare.Models
{
  
    public class Floors
    {
      [Key] public int _Id { get; set; }
      public int _Order { get; set; }
        public int _BuildingId{ get; set; }
      public string _Name { get; set; }
    }
}
