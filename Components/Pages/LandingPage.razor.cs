using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using IA_AbansiBabayiSystemBlazor.Data.Models;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using Azure;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.WebUtilities;

namespace IA_AbansiBabayiSystemBlazor.Components.Pages
{
    public partial class LandingPage
    {
        private LoginModel loginModel = new();
        private string errorMessage;

        public class LoginModel
        {
            [Required(ErrorMessage = "Email is required.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            public string Password { get; set; }
        }

        private async Task HandleLoginAsync()
        {
            // Optional: Basic client-side validation
            if (string.IsNullOrWhiteSpace(loginModel.Email) || string.IsNullOrWhiteSpace(loginModel.Password))
            {
                errorMessage = "Please fill in both fields.";
                return;
            }

            errorMessage = null;

            // ✅ Calls the JS form submitter that sends a POST to /auth/login
            await JS.InvokeVoidAsync("submitLoginForm", loginModel.Email, loginModel.Password);
        }

        protected override void OnInitialized()
        {
            var uri = new Uri(NavigationManager.Uri);
            var query = QueryHelpers.ParseQuery(uri.Query); // returns Dictionary<string, StringValues>

            if (query.TryGetValue("error", out var error) && error == "invalid")
            {
                errorMessage = "Invalid credentials.";
                ToggleLoginForm();
            }
        }

        private bool showLoginForm = false;
        private bool showBodyEffect = false;

        private void ToggleLoginForm()
        {
            showLoginForm = !showLoginForm;
            showBodyEffect = !showBodyEffect;

            Console.WriteLine("TOGGLE LOGIN CLICKED");
            Logger.LogWarning("TOGGLE LOGIN CLICKED");
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