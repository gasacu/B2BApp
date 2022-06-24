using Microsoft.AspNetCore.Identity;

namespace B2BApp.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
