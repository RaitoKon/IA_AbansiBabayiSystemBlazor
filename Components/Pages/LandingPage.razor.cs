using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using IA_AbansiBabayiSystemBlazor.Data.Models;
using Microsoft.JSInterop;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Internal;

namespace IA_AbansiBabayiSystemBlazor.Components.Pages
{
    public partial class LandingPage
    {

        private LoginModel loginModel = new();

        public class LoginModel
        {
            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            public string Password { get; set; }
        }

        private string loginError = "";
        private async Task HandleLogin()
        {
            loginError = "";

            if (string.IsNullOrWhiteSpace(loginModel.Email) || string.IsNullOrWhiteSpace(loginModel.Password))
            {
                loginError = "Email and password are required.";
                return;
            }

            await using var db = DbContextFactory.CreateDbContext();
            var user = await db.Logins.FirstOrDefaultAsync(u => u.Email == loginModel.Email && u.Password == loginModel.Password);

            if (user != null)
            {
                // Save data to sessionStorage
                await JS.InvokeVoidAsync("sessionStorage.setItem", "userId", user.UserId.ToString());
                await JS.InvokeVoidAsync("sessionStorage.setItem", "userEmail", user.Email);
                await JS.InvokeVoidAsync("sessionStorage.setItem", "userRole", user.AuthRoleId);

                Logger.LogInformation("User logged in.");
                NavigationManager.NavigateTo("/userPage");
            }
            else
            {
                loginError = "Invalid email or password.";
                Logger.LogWarning("Invalid login attempt.");
            }
        }

        private bool showLoginForm = false;
        private bool showBodyEffect = false;

        private void ToggleLoginForm()
        {
            loginModel = new LoginModel();       // Reset the input fields
            loginError = string.Empty;

            showLoginForm = !showLoginForm;
            showBodyEffect = !showBodyEffect;
        }

        private bool showDropDown = false;

        private void ToggleDropDown()
        {
            showDropDown = !showDropDown;
        }

        private string backgroundImage = "images/BG-Base.png";
        private double imageOpacity = 1.0; // Fully visible

        private async Task ChangeBackground(string newImage)
        {
            imageOpacity = 0; // Start fade-out
            await Task.Delay(200); // Wait for fade-out effect
            backgroundImage = newImage; // Change the image source
            imageOpacity = 1; // Fade-in the new image
        }

        private void ResetBackground()
        {
            backgroundImage = "images/BG-Base.png";

        }


        private string currentCard = "login-card";

        private void ShowScoutLevel()
        {
            currentCard = "selectScoutLevel-card";
        }

        private string selectedScoutLevel = "";
        private void SelectedScoutLevel(string level)
        {
            if ( showLoginForm == false && showBodyEffect == false && selectedScoutLevel != null )
            {
                ToggleLoginForm();
                selectedScoutLevel = level;
                currentCard = "selectStatus-card";
            }
            else {

                selectedScoutLevel = level;
                currentCard = "selectStatus-card";
            }
            
        }

        private string selectedStatus = "";

        private void SelectedStatus(string status)
        {
            selectedStatus = status;

            PassedDataRoute.ScoutLevel = selectedScoutLevel;
            PassedDataRoute.Status = selectedStatus;

            NavigationManager.NavigateTo("/registrationPage");
        }

        private void CancelRegistration()
        {
            currentCard = "login-card";
            selectedScoutLevel = "";
            ToggleLoginForm();
        }

        private void OpenRegistrationPage()
        {
            NavigationManager.NavigateTo("/landingPageUser");
        }
    }
}