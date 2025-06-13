
using IA_AbansiBabayiSystemBlazor.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.JSInterop;

namespace IA_AbansiBabayiSystemBlazor.Components.Pages
{
    public partial class RegistrationPage
    {

        private string errorMessage = "";
        private string successMessage = "";

        private string currentScoutNumber = string.Empty; //Scout

        private string currentPosition = string.Empty;  //Troop Leader
        private string currentTorNT = string.Empty;

        private string currentTroopNumber = string.Empty;  //Scout & Co leader

        private string currentRole = string.Empty;  //All
        private string currentFname = string.Empty;
        private string currentMInitial = string.Empty;
        private string currentLname = string.Empty;
        private string currentStatus = string.Empty;
        private string currentBirthDate = string.Empty;
        private string currentBeneficiary = string.Empty;
        private string currentEmail = string.Empty;

        protected override void OnInitialized()
        {
            currentRole = PassedDataRoute.ScoutLevel;
            currentStatus = PassedDataRoute.Status;
        }

        private bool toggleRegistrationDropdown = false;
        private void ToggleRegistrationDropdown()
        {
            toggleRegistrationDropdown = !toggleRegistrationDropdown;

        }

        private void SelectedRole(string selectedRole)
        {
            currentRole = selectedRole;
            toggleRegistrationDropdown = false;
        }

        private bool toggleStatusDropdown = false;

        private void ToggleStatusDropdown()
        {
            toggleStatusDropdown = !toggleStatusDropdown;
        }

        private bool togglePositionDropdown = false;
        private void TogglePositionDropdown()
        {

            togglePositionDropdown = !togglePositionDropdown;

        }

        private bool toggleTorNTDropdown = false;
        private void ToggleTorNTDropdown()
        {
            toggleTorNTDropdown = !toggleTorNTDropdown;
        }

        private bool toggleScoutNumberInput = true;
        private void SelectedStatus(string selectedStatus)
        {
            currentStatus = selectedStatus;
            toggleScoutNumberInput = false;
            ToggleStatusDropdown();
        }

        private void SelectedPosition(string selectedPosition)
        {
            currentPosition = selectedPosition;
            TogglePositionDropdown();
        }

        private void SelectedTorNT(string selectedTorNT)
        {
            currentTorNT = selectedTorNT;
            ToggleTorNTDropdown();
        }

        private async Task CancelRegistration()
        {
            NavigationManager.NavigateTo("/landingPage", forceLoad: true);
        }

        private async Task SubmitRegistration()
        {
            errorMessage = string.Empty;
            successMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(currentFname) ||
                string.IsNullOrWhiteSpace(currentMInitial) ||
                string.IsNullOrWhiteSpace(currentLname) ||
                string.IsNullOrWhiteSpace(currentPosition) ||
                string.IsNullOrWhiteSpace(currentRole) ||
                string.IsNullOrWhiteSpace(currentTorNT) ||
                string.IsNullOrWhiteSpace(currentStatus) ||
                string.IsNullOrWhiteSpace(currentBirthDate) ||
                string.IsNullOrWhiteSpace(currentBeneficiary))
            {
                errorMessage = "Please fill in all required fields.";
                return;
            }

            int? validatedtroopNumber = null;
            if (!string.IsNullOrWhiteSpace(currentTroopNumber))
            {
                if (int.TryParse(currentTroopNumber, out var parsedNumber))
                {
                    validatedtroopNumber = parsedNumber;
                }
                else
                {
                    errorMessage = "Invalid troop number.";
                    return;
                }
            }


            DateOnly? validatedbirthDate = null;
            if (!string.IsNullOrWhiteSpace(currentBirthDate))
            {
                if (DateOnly.TryParse(currentBirthDate, out var parsedDate))
                {
                    validatedbirthDate = parsedDate;
                }
                else
                {
                    errorMessage = "Invalid birth date. Use format mm-dd-yyyy.";
                    return;
                }
            }

            try
            {

                var existingLeader = AppDbContext.TroopLeaders.FirstOrDefault(l => l.LeaderEmail == currentEmail);

                if (existingLeader != null)
                {
                    errorMessage = "This email is already registered.";
                    return;
                }

                var newLeader = new TroopLeaderRegistration
                {
                    LeaderFname = currentFname,
                    LeaderMInitial = currentMInitial,
                    LeaderLname = currentLname,
                    LeaderPosition = currentPosition,
                    CoLeaderToopNumber = validatedtroopNumber,
                    LeaderRole = currentRole,
                    LeaderTorNT = currentTorNT,
                    LeaderRegStatus = currentStatus,
                    LeaderEmail = currentEmail,
                    LeaderBeneficiary = currentBeneficiary,
                    LeaderBirthdate = validatedbirthDate
                };

                AppDbContext.TroopLeaders.Add(newLeader);
                await AppDbContext.SaveChangesAsync();

                NavigationManager.NavigateTo("/registrationConfirmedPage");
            }
            catch (Exception ex)
            {
                errorMessage = $"An error occurred: {ex.Message}";
            }
        }

    }
}
