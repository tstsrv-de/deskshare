using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskShare.Models
{
    public class UserIdentity : IdentityUser
    {
            public bool _Perm { get; set; }
        
    }
}
