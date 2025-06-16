namespace IA_AbansiBabayiSystemBlazor.Data.Models
{
    public class LoginInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResult
    {
        public bool MustChange { get; set; }
        public string UserId { get; set; }
    }
}