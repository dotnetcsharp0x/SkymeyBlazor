using Skymey_main_lib.Models.Tables.User;
using SkymeyBlazor.Models;

namespace SkymeyBlazor.Services
{
    public interface IAuthenticationService
    {
        event Action<string?>? LoginChange;

        ValueTask<string> GetJwtAsync();
        Task<DateTime> LoginAsync(SU_001LoginViewModel model);
        Task LogoutAsync();
        Task<bool> RefreshAsync();
    }
}