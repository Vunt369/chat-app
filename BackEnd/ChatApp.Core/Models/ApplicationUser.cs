using Microsoft.AspNetCore.Identity;

namespace ChatApp.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Avartar { get; set; }
    }
}