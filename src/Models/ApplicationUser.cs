using Microsoft.AspNetCore.Identity;

namespace src.Models {
    public class ApplicationUser : IdentityUser {
        public string? firstName {get; set;}
        public string? lastName {get; set;}
    }
}