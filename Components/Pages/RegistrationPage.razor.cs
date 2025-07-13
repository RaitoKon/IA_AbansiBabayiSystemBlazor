
using IA_AbansiBabayiSystemBlazor.Data.Models;
using IA_AbansiBabayiSystemBlazor.Service;
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
        private string currentGradeOrYear = string.Empty;

        private string currentPosition = string.Empty;  //Troop Leader
        private string currentTorNT = string.Empty;

        private string currentTroopNumber = string.Empty;  //Scout & Co leader

        private string currentRole = string.Empty;  //All
        private string currentFname = string.Empty;
        private string currentMInitial = string.Empty;
        private string currentLname = string.Empty;
        private string currentStatus = string.Empty;
        private DateOnly? currentBirthDate = null;
        private string currentBeneficiary = string.Empty;
        private string currentEmail = string.Empty;
        private string selectedLeaderName = string.Empty;

        private List<TroopLeaderInfo> troopLeaders = new();

        public class TroopLeaderInfo
        {
            public int LeaderId { get; set; }
            public string FullName { get; set; } = string.Empty;
        }
        protected override async Task OnInitializedAsync()
        {
            currentRole = PassedDataRoute.ScoutLevel;
            currentStatus = PassedDataRoute.Status;

            troopLeaders = await (
            from leader in AppDbContext.RegisteredTroopLeaders
            join account in AppDbContext.RegisteredTroopLeaderAccounts on leader.LeaderId equals account.LeaderId
            join userRole in AppDbContext.UserRoles on account.Id equals userRole.UserId
            join role in AppDbContext.Roles on userRole.RoleId equals role.Id
            where role.Name == "Troop Leader"
                && leader.LeaderPosition == "Leader"
            select new TroopLeaderInfo
            {
                LeaderId = leader.LeaderId,
                FullName = leader.LeaderFname + " " + leader.LeaderMInitial + " " + leader.LeaderLname
            }
        )
        .Distinct()
        .OrderBy(x => x.LeaderId)
        .ToListAsync();
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

        private bool toggleTroopLeaderDropdown = false;

        private void ToggleTroopLeaderDropdown()
        {
            toggleTroopLeaderDropdown = !toggleTroopLeaderDropdown;
        }

        private void SelectTroopNumber(string leaderId, string fullName)
        {
            currentTroopNumber = leaderId;
            selectedLeaderName = fullName;
            toggleTroopLeaderDropdown = false;
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
            errorMessage = "";
            await Task.Delay(2000);

            if (currentRole == "Troop Leader") { 

                errorMessage = string.Empty;
                successMessage = string.Empty;

                if (string.IsNullOrWhiteSpace(currentFname) ||
                    string.IsNullOrWhiteSpace(currentMInitial) ||
                    string.IsNullOrWhiteSpace(currentLname) ||
                    string.IsNullOrWhiteSpace(currentPosition) ||
                    string.IsNullOrWhiteSpace(currentRole) ||
                    string.IsNullOrWhiteSpace(currentTorNT) ||
                    string.IsNullOrWhiteSpace(currentStatus) ||
                    !currentBirthDate.HasValue ||
                    string.IsNullOrWhiteSpace(currentBeneficiary) ||
                    string.IsNullOrWhiteSpace(currentEmail)
                    )
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

                try
                {

                    var existingLeader = AppDbContext.RegisteredTroopLeaders.FirstOrDefault(l => l.LeaderEmail == currentEmail);
                    var registerdLeader = AppDbContext.TroopLeaders.FirstOrDefault(l => l.LeaderEmail == currentEmail);
                    var existingMember = AppDbContext.RegisteredTroopMembers.FirstOrDefault(l => l.TroopMemEmail == currentEmail);
                    var registerdMember = AppDbContext.TroopMembers.FirstOrDefault(l => l.TroopMemEmail == currentEmail);

                    if (existingLeader != null || registerdLeader != null || existingMember != null || registerdMember != null)
                    {
                        errorMessage = "Your email is already registered. Proceed to log-in";
                        return;
                    }

                    var newLeader = new TroopLeaderRegistration
                    {
                        LeaderFname = currentFname,
                        LeaderMInitial = currentMInitial,
                        LeaderLname = currentLname,
                        LeaderPosition = currentPosition,
                        CoLeaderTroopNumber = validatedtroopNumber,
                        LeaderRole = currentRole,
                        LeaderTorNT = currentTorNT,
                        LeaderRegStatus = currentStatus,
                        LeaderEmail = currentEmail,
                        LeaderBeneficiary = currentBeneficiary,
                        LeaderBirthdate = currentBirthDate
                    };

                    AppDbContext.TroopLeaders.Add(newLeader);
                    await AppDbContext.SaveChangesAsync();

                    await EmailService.SendRegistrationSubmittedEmailAsync(currentEmail, currentFname);

                    NavigationManager.NavigateTo("/registrationConfirmedPage");
                }
                catch (Exception ex)
                {
                    errorMessage = $"An error occurred: {ex.Message}";
                }
            }

            else if (new[] { "Twinklers", "Star", "Junior", "Senior", "Cadet" }.Contains(currentRole))
            {

                errorMessage = string.Empty;
                successMessage = string.Empty;

                if (string.IsNullOrWhiteSpace(currentTroopNumber) ||
                    string.IsNullOrWhiteSpace(currentFname) ||
                    string.IsNullOrWhiteSpace(currentMInitial) ||
                    string.IsNullOrWhiteSpace(currentLname) ||
                    string.IsNullOrWhiteSpace(currentRole) ||
                    string.IsNullOrWhiteSpace(currentGradeOrYear) ||
                    string.IsNullOrWhiteSpace(currentStatus) ||
                    !currentBirthDate.HasValue ||
                    string.IsNullOrWhiteSpace(currentBeneficiary) ||
                    string.IsNullOrWhiteSpace(currentEmail)
                    )
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

                try
                {

                    var existingLeader = AppDbContext.RegisteredTroopLeaders.FirstOrDefault(l => l.LeaderEmail == currentEmail);
                    var registerdLeader = AppDbContext.TroopLeaders.FirstOrDefault(l => l.LeaderEmail == currentEmail);
                    var existingMember = AppDbContext.RegisteredTroopMembers.FirstOrDefault(l => l.TroopMemEmail == currentEmail);
                    var registerdMember = AppDbContext.TroopMembers.FirstOrDefault(l => l.TroopMemEmail == currentEmail);

                    if (existingMember != null || registerdMember!= null)
                    {
                        errorMessage = "Your email is already used. Proceed to log-in";
                        return;
                    }

                    var newMember = new TroopMemberRegistration
                    {
                        TroopMemFname = currentFname,
                        TroopMemMinitial = currentMInitial,
                        TroopMemLname = currentLname,
                        TroopMemTroopNumber = validatedtroopNumber,
                        TroopMemRole = currentRole,
                        TroopMemRegStatus = currentStatus,
                        TroopMemEmail = currentEmail,
                        TroopMemBeneficiary = currentBeneficiary,
                        TroopMemBirthdate = currentBirthDate,
                        TroopMemGradeOrYear = currentGradeOrYear
                    };

                    AppDbContext.TroopMembers.Add(newMember);
                    await AppDbContext.SaveChangesAsync();

                    await EmailService.SendRegistrationSubmittedEmailAsync(currentEmail, currentFname);

                    NavigationManager.NavigateTo("/registrationConfirmedPage");
                }
                catch (Exception ex)
                {
                    errorMessage = $"An error occurred: {ex.Message}";
                }


            }
            else
            {
                errorMessage = "Invalid role. Please select a valid scout level.";
            }
        }

    }
}
