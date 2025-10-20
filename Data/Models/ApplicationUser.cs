using Microsoft.AspNetCore.Identity;

namespace IA_AbansiBabayiSystemBlazor.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool MustChangePassword { get; set; } = false;
        public int? AccountStatusId { get; set; }
        public virtual AccountStatus? AccountStatus { get; set; }

    }
}
