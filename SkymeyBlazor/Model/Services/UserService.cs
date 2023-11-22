using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using RestSharp;
using Skymey_main_lib.Models;
using Skymey_main_lib.Models.JWT;
using Skymey_main_lib.Models.Tables.User;
using SkymeyBlazor.Interfaces.User;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace SkymeyBlazor.Model.Services
{
    public class UserService : DelegatingHandler
    {
        private bool _refreshing;
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration) {
            _configuration = configuration;
        }
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
        public async Task<AuthenticatedResponse> Refresh(AuthenticatedResponse sU_001LoginViewModel)
        {
            var options = new RestClientOptions("https://46.22.247.253:5007")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/User/Refresh", Method.Post);
            request.AddParameter("application/json", JsonSerializer.Serialize(sU_001LoginViewModel), ParameterType.RequestBody);
            var r = client.ExecuteAsync(request).Result.Content;
            var userd = JsonSerializer.Deserialize<AuthenticatedResponse>(r);
            return userd;
        }

        public async Task<List<SU_001ListViewModel>> GetUsers(string token)
        {
            try
            {
                var options = new RestClientOptions("https://46.22.247.253:5007")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/api/User/GetUsers", Method.Get);
                //request.AddParameter("application/json", token, ParameterType.RequestBody);
                request.AddParameter("token", token);
                var r = client.ExecuteAsync(request).Result.Content;
                var userd = JsonSerializer.Deserialize<List<SU_001ListViewModel>>(r);
                return userd;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<SU_001ListViewModel> { };
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("ahahah");
            //var jwt = await _authenticationService.GetJwtAsync();
            var jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJOYW1lIjoiZHNvdG5pa292MTk5NkBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZHNvdG5pa292MTk5NkBnbWFpbC5jb20iLCJHcm91cFNpZCI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL2dyb3Vwc2lkIjoiMSIsIlJvbGUiOiJBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwibmJmIjoxNzAwNjI5MTk4LCJleHAiOjE3MDA2MjkyMTgsImlzcyI6Im15aG9sZCIsImF1ZCI6WyJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiXX0.pU-Q6tF-_g47RTgev_S81tttO41GLAI0RkXpEE8i8wA";

            var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;

            if (isToServer && !string.IsNullOrEmpty(jwt))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var response = await base.SendAsync(request, cancellationToken);

            if (!_refreshing && !string.IsNullOrEmpty(jwt) && response.StatusCode == HttpStatusCode.Unauthorized)
            {
                try
                {
                    _refreshing = true;

                    //if (await _authenticationService.RefreshAsync())
                    if(true)
                    {
                        //jwt = await _authenticationService.GetJwtAsync();
                        jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJOYW1lIjoiZHNvdG5pa292MTk5NkBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZHNvdG5pa292MTk5NkBnbWFpbC5jb20iLCJHcm91cFNpZCI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL2dyb3Vwc2lkIjoiMSIsIlJvbGUiOiJBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwibmJmIjoxNzAwNjI5MTk4LCJleHAiOjE3MDA2MjkyMTgsImlzcyI6Im15aG9sZCIsImF1ZCI6WyJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiLCJteWhvbGQiXX0.pU-Q6tF-_g47RTgev_S81tttO41GLAI0RkXpEE8i8wA";

                        if (isToServer && !string.IsNullOrEmpty(jwt))
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                        response = await base.SendAsync(request, cancellationToken);
                    }
                }
                finally
                {
                    _refreshing = false;
                }
            }

            return response;
        }

        //public async Task<List<SU_001ListViewModel>> GetUsers(string token)
        //{
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage(HttpMethod.Get, "https://46.22.247.253:5007/api/User/GetUsers?token="+token);
        //    //request.Headers.Add("Authorization", "Bearer " + token);
        //    var response = await client.SendAsync(request);
        //    response.EnsureSuccessStatusCode();
        //    return JsonSerializer.Deserialize<List<SU_001ListViewModel>>(response.Content.ToString());
        //}
    }
}
