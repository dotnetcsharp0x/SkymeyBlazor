using RestSharp;
using Skymey_main_lib.Models;
using Skymey_main_lib.Models.JWT;
using Skymey_main_lib.Models.Tables.User;
using SkymeyBlazor.Interfaces.User;
using System;
using System.Net.Http.Json;
using System.Text.Json;

namespace SkymeyBlazor.Model.Services
{
    public class UserService : IUserRepository
    {
        public async Task<List<ExchangesViewModel>> GetExchanges()
        {
            return await new HttpClient().GetFromJsonAsync<List<ExchangesViewModel>>("https://46.22.247.253:5007/api/Crypto/GetExchanges");
        }
        public async Task<AuthenticatedResponse> Login(SU_001LoginViewModel sU_001LoginViewModel)
        {
            var options = new RestClientOptions("https://46.22.247.253:5007")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/User/Login", Method.Post);
            request.AddParameter("application/json", JsonSerializer.Serialize(sU_001LoginViewModel), ParameterType.RequestBody);
            var r = client.ExecuteAsync(request).Result.Content;
            var userd = JsonSerializer.Deserialize<AuthenticatedResponse>(r);
            return userd;
        }
    }
}
