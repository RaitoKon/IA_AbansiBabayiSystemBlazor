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

namespace IA_AbansiBabayiSystemBlazor.Components.Pages
{
    public partial class LandingPage
    {

        private LoginModel loginModel = new();
        private string errorMessage;

        public class LoginModel
        {
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        }
        private async Task LoginAsync()
        {
            errorMessage = ""; // clear previous message

            var formData = new MultipartFormDataContent
        {
            { new StringContent(loginModel.Email), "email" },
            { new StringContent(loginModel.Password), "password" }
        };

            var response = await Http.PostAsync("/auth/login", formData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                if (result != null && result.TryGetValue("redirectUrl", out var url))
                {
                    NavigationManager.NavigateTo(url, forceLoad: true);
                }
            }
            else
            {
                try
                {
                    var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    errorMessage = error?["message"] ?? "Login failed.";
                }
                catch
                {
                    errorMessage = "Login failed. Unexpected error.";
                }
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