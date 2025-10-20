
using IA_AbansiBabayiSystemBlazor.Data.Models;
using IA_AbansiBabayiSystemBlazor.Hubs;
using IA_AbansiBabayiSystemBlazor.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.JSInterop;

namespace IA_AbansiBabayiSystemBlazor.Components.Pages
{
    public partial class RegistrationPage
    {

        private string passedSelectedRole = string.Empty;
        private int passedSelectedScoutLevel;
        private string currentRole =  string.Empty;
        private string currentScoutLevel = string.Empty;

        private string errorMessage = "";
        private string successMessage = "";

        private List<TroopMemberScoutLevel> TroopMemberScoutLevel = new();
        private List<LeaderPosition> LeaderPositions = new();
        private List<TroopInformation> TroopInformations = new();
        private List<TroopInformation> TroopLeaders = new();
        private HubConnection? _hubConnection;

        private TroopLeader leaderinfo = new();
        private TroopMember troopmemberinfo = new();
        private TroopInformation troopinfo = new();
        private Dictionary<string, string> validationErrors = new Dictionary<string, string>();


        [Inject]
        private IHubContext<AutoUpdateHub> HubContext { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {

            passedSelectedRole = RegistrationStateService.SelectedRole;
            passedSelectedScoutLevel = RegistrationStateService.SelectedScoutLevel;

            currentRole = passedSelectedRole;
            if (passedSelectedScoutLevel != null)
            {
                troopmemberinfo.TroopMemScoutLevelId = passedSelectedScoutLevel;
            }

            try
            {
                await LoadLeaderPositions();
                await LoadTroopMemberScoutLevel();
                await LoadTroopInformation();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error loading LeaderPositions");
                errorMessage = "Failed to load positions. Please try again later.";
            }

            // Setup SignalR
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/autoUpdateHub"))
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string>("ReceiveUpdate", async (tableName) =>
            {
                Logger.LogInformation("Received SignalR update for table: {Table}", tableName);
                if (tableName == "LeaderPosition")
                {
                    await LoadLeaderPositions();
                    await TroopLeaderService.LoadDataAsync();
                    await LoadTroopInformation();
                    await TroopInformationService.LoadDataAsync();
                    await InvokeAsync(StateHasChanged);
                }
            });

            await _hubConnection.StartAsync();
            Logger.LogInformation("SignalR connection started.");
        }

        private async Task LoadLeaderPositions()
        {
            await LeaderPositionService.LoadDataAsync();
            LeaderPositions = LeaderPositionService.Data.ToList();
        }

        private async Task LoadTroopMemberScoutLevel()
        {
            await ScoutLevelService.LoadDataAsync();
            TroopMemberScoutLevel = ScoutLevelService.Data.ToList();
        }

        private async Task LoadTroopInformation()
        {
            await TroopInformationService.LoadDataAsync(query => query
                .Include(t => t.TroopLeader) // This will load the related leader data
            );

            // Use the original entities - they now have TroopLeader loaded
            TroopInformations = TroopInformationService.Data
                .Where(troop => troop.TroopLeaderId != null)
                .ToList();
        }

        private async Task CancelRegistration()
        {
            NavigationManager.NavigateTo("/landingPage", forceLoad: true);
        }

        private async Task<bool> ValidateForm()
        {
            validationErrors.Clear();

            // Role validation
            if (string.IsNullOrWhiteSpace(currentRole))
            {
                validationErrors["Role"] = "Please select a role.";
            }

            // Scout specific validations
            if (currentRole == "Scout")
            {
                // Scout Level validation
                if (troopmemberinfo.TroopMemScoutLevelId == null || troopmemberinfo.TroopMemScoutLevelId == 0)
                {
                    validationErrors["ScoutLevel"] = "Scout Level is required.";
                }

                // Troop validation
                if (troopmemberinfo.TroopMemTroopNo == null)
                {
                    validationErrors["Troop"] = "Troop is required.";
                }

                // First Name validation
                if (string.IsNullOrWhiteSpace(troopmemberinfo.TroopMemFname))
                {
                    validationErrors["FirstName"] = "First Name is required.";
                }
                else if (troopmemberinfo.TroopMemFname.Trim().Length < 2)
                {
                    validationErrors["FirstName"] = "First Name must be at least 2 characters.";
                }

                // Last Name validation
                if (string.IsNullOrWhiteSpace(troopmemberinfo.TroopMemLname))
                {
                    validationErrors["LastName"] = "Last Name is required.";
                }
                else if (troopmemberinfo.TroopMemLname.Trim().Length < 2)
                {
                    validationErrors["LastName"] = "Last Name must be at least 2 characters.";
                }

                // Birthdate validation
                if (troopmemberinfo.TroopMemBirthdate == null)
                {
                    validationErrors["Birthdate"] = "Birthdate is required.";
                }
                else if (troopmemberinfo.TroopMemBirthdate > DateTime.Now)
                {
                    validationErrors["Birthdate"] = "Birthdate cannot be in the future.";
                }
                else if (troopmemberinfo.TroopMemBirthdate > DateTime.Now.AddYears(-4))
                {
                    validationErrors["Birthdate"] = "Must be at least 4 years old.";
                }

                // Grade/Year validation
                if (string.IsNullOrWhiteSpace(troopmemberinfo.TroopMemGradeOrYear))
                {
                    validationErrors["GradeYear"] = "Grade or Year is required.";
                }

                // Registration Status validation
                if (string.IsNullOrWhiteSpace(troopmemberinfo.TroopMemRegStatus))
                {
                    validationErrors["RegistrationStatus"] = "Registration status is required.";
                }

                // Beneficiary validation
                if (string.IsNullOrWhiteSpace(troopmemberinfo.TroopMemBeneficiary))
                {
                    validationErrors["Beneficiary"] = "Beneficiary is required.";
                }
                else if (troopmemberinfo.TroopMemBeneficiary.Trim().Length < 2)
                {
                    validationErrors["Beneficiary"] = "Beneficiary name must be at least 2 characters.";
                }

                // Email validation
                if (string.IsNullOrWhiteSpace(troopmemberinfo.TroopMemEmail))
                {
                    validationErrors["Email"] = "Email is required.";
                }
                else if (!IsValidEmail(troopmemberinfo.TroopMemEmail))
                {
                    validationErrors["Email"] = "Invalid email format.";
                }
            }

            // Troop Leader specific validations
            else if (currentRole == "Troop Leader")
            {
                // Position validation
                if (leaderinfo.LeaderPositionId == null || leaderinfo.LeaderPositionId == 0)
                {
                    validationErrors["Position"] = "Position is required.";
                }

                // Teaching status validation
                if (string.IsNullOrWhiteSpace(leaderinfo.LeaderTorNt))
                {
                    validationErrors["TeachingStatus"] = "Teaching status is required.";
                }

                // Troop Number validation
                if (leaderinfo.LeaderRegStatus == "Existing Leader" && leaderinfo.LeaderTroopNo == null)
                {
                    validationErrors["TroopNumber"] = "Troop Number is required for existing leaders.";
                }

                // First Name validation
                if (string.IsNullOrWhiteSpace(leaderinfo.LeaderFname))
                {
                    validationErrors["FirstName"] = "First Name is required.";
                }
                else if (leaderinfo.LeaderFname.Trim().Length < 2)
                {
                    validationErrors["FirstName"] = "First Name must be at least 2 characters.";
                }

                // Last Name validation
                if (string.IsNullOrWhiteSpace(leaderinfo.LeaderLname))
                {
                    validationErrors["LastName"] = "Last Name is required.";
                }
                else if (leaderinfo.LeaderLname.Trim().Length < 2)
                {
                    validationErrors["LastName"] = "Last Name must be at least 2 characters.";
                }

                // Birthdate validation
                if (leaderinfo.LeaderBirthdate == null)
                {
                    validationErrors["Birthdate"] = "Birthdate is required.";
                }
                else if (leaderinfo.LeaderBirthdate > DateTime.Now)
                {
                    validationErrors["Birthdate"] = "Birthdate cannot be in the future.";
                }
                else if (leaderinfo.LeaderBirthdate > DateTime.Now.AddYears(-4))
                {
                    validationErrors["Birthdate"] = "Must be at least 4 years old.";
                }

                // Registration Status validation
                if (string.IsNullOrWhiteSpace(leaderinfo.LeaderRegStatus))
                {
                    validationErrors["RegistrationStatus"] = "Registration status is required.";
                }

                // Beneficiary validation
                if (string.IsNullOrWhiteSpace(leaderinfo.LeaderBeneficiary))
                {
                    validationErrors["Beneficiary"] = "Beneficiary is required.";
                }
                else if (leaderinfo.LeaderBeneficiary.Trim().Length < 2)
                {
                    validationErrors["Beneficiary"] = "Beneficiary name must be at least 2 characters.";
                }

                // Email validation
                if (string.IsNullOrWhiteSpace(leaderinfo.LeaderEmail))
                {
                    validationErrors["Email"] = "Email is required.";
                }
                else if (!IsValidEmail(leaderinfo.LeaderEmail))
                {
                    validationErrors["Email"] = "Invalid email format.";
                }
            }

            // Email uniqueness check (for both roles)
            if (currentRole == "Scout" && !string.IsNullOrWhiteSpace(troopmemberinfo.TroopMemEmail))
            {
                var existingScoutEmail = TroopMemberService.Data.FirstOrDefault(x => x.TroopMemEmail == troopmemberinfo.TroopMemEmail);
                var aspExistingEmail = await UserManagerService.FindByEmailAsync(troopmemberinfo.TroopMemEmail);

                if (existingScoutEmail != null || aspExistingEmail != null)
                {
                    validationErrors["Email"] = "Email is already taken.";
                }
            }
            else if (currentRole == "Troop Leader" && !string.IsNullOrWhiteSpace(leaderinfo.LeaderEmail))
            {
                var existingLeaderEmail = TroopLeaderService.Data.FirstOrDefault(x => x.LeaderEmail == leaderinfo.LeaderEmail);
                var aspExistingEmail = await UserManagerService.FindByEmailAsync(leaderinfo.LeaderEmail);

                if (existingLeaderEmail != null || aspExistingEmail != null)
                {
                    validationErrors["Email"] = "Email is already taken.";
                }
            }

            // If there are any validation errors, show the first one as general error
            if (validationErrors.Any())
            {
                errorMessage = validationErrors.First().Value;
                return false;
            }

            successMessage = "Validation passed!";
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email && email.Contains("@");
            }
            catch
            {
                return false;
            }
        }
    
        // Method to clear validation errors (call this before validation)
        private void ClearValidation()
        {
            validationErrors.Clear();
            errorMessage = string.Empty;
            successMessage = string.Empty;
        }

        private bool isSubmitting = false;

        private async Task SubmitRegistration()
        {
            ClearValidation();
            isSubmitting = true;
            errorMessage = "";
            successMessage = "";

            await Task.Delay(3000);

            if (currentRole == "Scout")
            {

                troopmemberinfo.UserRole = "Scout";
                await SubmitTroopMemberRegistration();

            }
            else if (currentRole == "Troop Leader")
            {
                leaderinfo.UserRole = "Troop Leader";
                await SubmitLeaderRegistration();
            }
        }
        private async Task SubmitTroopMemberRegistration()
        {
            try
            {
                if (!await ValidateForm())
                    return;

                var troopMemUser = new ApplicationUser
                {
                    MustChangePassword = true,
                    UserName = troopmemberinfo.TroopMemEmail,
                    Email = troopmemberinfo.TroopMemEmail,
                    AccountStatusId = 1
                };

                var identityResult = await UserManagerService.CreateAsync(troopMemUser);
                if (!identityResult.Succeeded)
                {
                    errorMessage = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                    return;
                }

                troopmemberinfo.ApplicationUserId = troopMemUser.Id;
                troopmemberinfo.TroopMemDateRegistered = DateTime.Now;

                // ✅ Save TroopLeader
                await TroopMemberService.Add(troopmemberinfo);

                successMessage = "Registration submitted successfully!";
                NavigationManager.NavigateTo("/RegistrationConfirmedPage");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error submitting registration");
                errorMessage = $"Failed to submit registration. Please try again {ex.InnerException.Message}";
            }
            finally
            {
                isSubmitting = false;
            }


        }
        private async Task SubmitLeaderRegistration()
        {
            try
            {
                if (!await ValidateForm())
                    return;

                var leaderUser = new ApplicationUser
                {
                    MustChangePassword = true,
                    UserName = leaderinfo.LeaderEmail,
                    Email = leaderinfo.LeaderEmail,
                    AccountStatusId = 1
                };

                var identityResult = await UserManagerService.CreateAsync(leaderUser);
                if (!identityResult.Succeeded)
                {
                    errorMessage = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                    return;
                }

                leaderinfo.ApplicationUserId = leaderUser.Id;
                leaderinfo.LeaderDateRegistered = DateTime.Now;

                // ✅ Save TroopLeader
                await TroopLeaderService.Add(leaderinfo);

                successMessage = "Registration submitted successfully!";
                NavigationManager.NavigateTo("/RegistrationConfirmedPage");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error submitting registration");
                errorMessage = $"Failed to submit registration. Please try again {ex.InnerException.Message}";
            }
            finally
            {
                isSubmitting = false;
            }


        }

        private string GetScoutLevelColor(int? scoutLevelId)
        {
            return scoutLevelId switch
            {
                1 => "linear-gradient(to left, #f467a4 0%, #f9b8d4 30%, #ffffff 100%)", // twinklers
                2 => "linear-gradient(to left, #f6e03a 0%, #faf0a0 30%, #ffffff 100%)", // star
                3 => "linear-gradient(to left, #fdbd23 0%, #fee6a1 30%, #ffffff 100%)", // junior  
                4 => "linear-gradient(to left, #ff8546 0%, #ffc5a8 30%, #ffffff 100%)", // senior
                5 => "linear-gradient(to left, #a821e5 0%, #d9a6f2 30%, #ffffff 100%)", // cadet
                _ => "#ffffff"  // default
            };
        }
    }

}
