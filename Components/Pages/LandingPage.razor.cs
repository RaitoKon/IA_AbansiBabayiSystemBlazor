using IA_AbansiBabayiSystemBlazor.Data;
using IA_AbansiBabayiSystemBlazor.Data.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace IA_AbansiBabayiSystemBlazor.Components.Pages
{
    public partial class LandingPage
    {
        private LoginModel loginModel = new();
        private string errorMessage;
        private bool isLoading = false;

        public class LoginModel
        {
            [Required(ErrorMessage = "Email is required.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            public string Password { get; set; }
        }

        protected override void OnInitialized()
        {
            var uri = new Uri(Navigation.Uri);
            var query = QueryHelpers.ParseQuery(uri.Query);

            if (query.TryGetValue("error", out var error))
            {
                errorMessage = error;
                ToggleLoginForm();
            }
        }

        private async Task HandleLoginAsync()
        {
            if (string.IsNullOrWhiteSpace(loginModel.Email) || string.IsNullOrWhiteSpace(loginModel.Password))
            {
                errorMessage = "Please fill in both fields.";
                return;
            }

            errorMessage = null;
            isLoading = true;
            StateHasChanged();

            try
            {
                await Task.Delay(1000);

                var errorMessageFromJs = await JS.InvokeAsync<string>("submitLoginForm", loginModel.Email, loginModel.Password);

                if (!string.IsNullOrEmpty(errorMessageFromJs))
                {
                    errorMessage = errorMessageFromJs;
                    isLoading = false;
                    StateHasChanged();
                }
                // If no error returned and no redirect happened, there might be an issue
                else
                {
                    // Add a small delay to see if redirect happens
                    await Task.Delay(1000);
                    // If still here, show generic error
                    errorMessage = "Login completed but no redirect occurred.";
                    isLoading = false;
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Login error: {ex.Message}";
                isLoading = false;
                StateHasChanged();
                Logger.LogError(ex, "Login error for {Email}", loginModel.Email);
            }
        }

        // Your existing UI methods remain the same
        private bool showLoginForm = false;
        private bool showBodyEffect = false;

        private void ToggleLoginForm()
        {
            showLoginForm = !showLoginForm;
            showBodyEffect = !showBodyEffect;

            // Reset form when opening/closing
            if (showLoginForm)
            {
                loginModel = new LoginModel();
                errorMessage = null;
            }
        }

        private bool showDropDown = false;

        private void ToggleDropDown()
        {
            showDropDown = !showDropDown;
        }

        private string backgroundImage = "images/BG-Base.png";
        private double imageOpacity = 1.0;

        private async Task ChangeBackground(string newImage)
        {
            imageOpacity = 0;
            await Task.Delay(200);
            backgroundImage = newImage;
            imageOpacity = 1;
        }

        private void ResetBackground()
        {
            backgroundImage = "images/BG-Base.png";
        }

        private string currentCard = "login-card";

        private void ShowRegistrationChoices()
        {
            currentCard = "registrationChoices-card";
        }

        private string selectedRole = string.Empty;
        private void SelectedRole(string role)
        {
            if (role == "Scout")
            {
                selectedRole = role;
                currentCard = "selectScoutLevel-card";
            }
            else
            {
                selectedRole = role;
                RegistrationStateService.SelectedRole = selectedRole;
                Navigation.NavigateTo("/registrationPage");
            }
        }

        private int selectedScoutLevel;

        private void SelectedScoutLevel(int status)
        {
            selectedScoutLevel = status;
            RegistrationStateService.SelectedRole = selectedRole;
            RegistrationStateService.SelectedScoutLevel = selectedScoutLevel;
            Navigation.NavigateTo("/registrationPage");
        }

        private void SelectedScoutLevelWithRole(int status)
        {
            selectedScoutLevel = status;
            RegistrationStateService.SelectedRole = "Scout";
            RegistrationStateService.SelectedScoutLevel = selectedScoutLevel;
            Navigation.NavigateTo("/registrationPage");
        }

        private void CancelRegistration()
        {
            currentCard = "login-card";
            ToggleLoginForm();
        }
    }
}