using Skymey_main_lib.Models;

namespace SkymeyBlazor.Interfaces.User
{
    public class IUserService
    {
        public IUserService(WebApplication s) { }
        public IUserService() { }
        public async Task<List<ExchangesViewModel>> GetExchanges()
        {
            return new List<ExchangesViewModel> { new ExchangesViewModel() };
        }
    }
}
