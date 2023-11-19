using Skymey_main_lib.Models;

namespace SkymeyBlazor.Interfaces.User
{
    public class IUserRepository
    {
        public async Task<List<ExchangesViewModel>> GetExchanges()
        {
            return new List<ExchangesViewModel> { new ExchangesViewModel() };
        }
    }
}
