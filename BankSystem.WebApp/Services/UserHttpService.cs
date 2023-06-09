using BankSystem.Domain;
using BankSystem.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BankSystem.WebApp.Services
{
    public class UserHttpService: IUserHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public UserHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _remoteServiceBaseUrl = "https://localhost:7018/api";
        }

        public async Task<ServiceResult<UserEntity>> AuthenticateUser(string username, string password)
        {
            var uri = _remoteServiceBaseUrl + "/User/Authenticate";
            var authData = new StringContent(JsonConvert.SerializeObject(new { username, password }), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, authData);
            
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var problemDetails = JsonConvert.DeserializeObject<JsonErrorResponse>(await response.Content.ReadAsStringAsync());
                throw new Exception(string.Join(',', problemDetails.Messages));
            }
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServiceResult<UserEntity>>(responseString);
        }

        public async Task<ServiceResult<UserEntity>> GetUserData(int userId)
        {
            var uri = _remoteServiceBaseUrl + $"/User/{userId}";
            var response = await _httpClient.GetAsync(uri);
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var problemDetails = JsonConvert.DeserializeObject<JsonErrorResponse>(await response.Content.ReadAsStringAsync());
                throw new Exception(string.Join(',', problemDetails.Messages));
            }
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServiceResult<UserEntity>>(responseString);
        }
    }
}
