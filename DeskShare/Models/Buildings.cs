using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeskShare.Models
{
  
    public class Buildings
    {
  [Key] public int _Id { get; set; }
  public int _Order { get; set; }
       public string _Location { get; set; } 
   public string _Name { get; set; }
    }
}
