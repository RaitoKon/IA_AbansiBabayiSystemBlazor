using IA_AbansiBabayiSystemBlazor.Data.Models;

namespace IA_AbansiBabayiSystemBlazor.Service
{
    public class UserSessionService
    {
        public Login CurrentUser { get; private set; }

        public void SetUser(Login user)
        {
            CurrentUser = user;
        }

        public void ClearUser()
        {
            CurrentUser = null;
        }

        public bool IsLoggedIn => CurrentUser != null;

    }
}
