using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Core.Identity
{
	public class AppUser: IdentityUser
	{
        public string DisplayName { get; set; }
    }
}
