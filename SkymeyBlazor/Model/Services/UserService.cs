using Skymey_main_lib.Models;
using SkymeyBlazor.Interfaces.User;

namespace SkymeyBlazor.Model.Services
{
    public class UserService : IUserRepository
    {
        public async Task<List<ExchangesViewModel>> GetExchanges()
        {
            return await new HttpClient().GetFromJsonAsync<List<ExchangesViewModel>>("https://46.22.247.253:5007/api/Crypto/GetExchanges");
        }
    }
}
