using Microsoft.AspNetCore.Identity;

namespace FarmPulse.Core.Models.Identity
{
    public class AppUser : IdentityUser

    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
      
        
        //From IdentityUser
        public string DisplayName { get; set; }      
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


    }

}
