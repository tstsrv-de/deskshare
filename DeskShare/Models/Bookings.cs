using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DeskShare.Models
{
    public class Bookings
    {
       [Key]  public int _Id { get; set; }
        public DateTime _Start { get; set; }
       public DateTime _End { get; set; }
      public DateTime _Timestamp { get; set; }

        public string _User { get; set; }
       public int _Desk { get; set; }
    }
}
