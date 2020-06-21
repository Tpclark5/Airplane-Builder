using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Services
{
    public class IdentityUserEmail: IdentityUser
    {
        public string Email { get; set; }
    }
}
