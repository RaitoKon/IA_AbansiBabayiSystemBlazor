using Microsoft.AspNetCore.Identity;

namespace IA_AbansiBabayiSystemBlazor.Data.Models
{
    public class ApplicationUser : IdentityUser
    {

        public bool MustChangePassword { get; set; } = true;

    }
}
