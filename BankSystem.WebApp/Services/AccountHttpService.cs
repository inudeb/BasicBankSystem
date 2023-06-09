using BankSystem.Domain;
using BankSystem.WebApp.Services.Interfaces;
using Newtonsoft.Json;

namespace BankSystem.WebApp.Services
{
    public class AccountHttpService: IAccountHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;


        public AccountHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _remoteServiceBaseUrl = "https://localhost:7018/api";
        }

        public async Task<ServiceResult<AccountEntity>> CreateAccount(int userId, decimal balance)
        {
            var uri = _remoteServiceBaseUrl + "/Account";
            var accountData = new StringContent(JsonConvert.SerializeObject(new { userId, Balance = balance }), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, accountData);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var problemDetails = JsonConvert.DeserializeObject<JsonErrorResponse>(await response.Content.ReadAsStringAsync());
                throw new Exception(string.Join(',', problemDetails.Messages));
            }
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServiceResult<AccountEntity>>(responseString);
        }

        public async Task<ServiceResult<AccountEntity>> Deposit(int accountId, decimal amount)
        {
            var uri = _remoteServiceBaseUrl + "/Account/Deposit";
            var accountData = new StringContent(JsonConvert.SerializeObject(new { accountId, amount }), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, accountData);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var problemDetails = JsonConvert.DeserializeObject<JsonErrorResponse>(await response.Content.ReadAsStringAsync());
                throw new Exception(string.Join(',', problemDetails.Messages));
            }
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServiceResult<AccountEntity>>(responseString);
        }

        public async Task<List<AccountEntity>> GetUserAccounts(int userID)
        {
            var uri = _remoteServiceBaseUrl + $"/Account/User/{userID}";
            var response = await _httpClient.GetAsync(uri);
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var problemDetails = JsonConvert.DeserializeObject<JsonErrorResponse>(await response.Content.ReadAsStringAsync());
                throw new Exception(string.Join(',', problemDetails.Messages));
            }
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<AccountEntity>>(responseString);
        }

        public async Task<ServiceResult<AccountEntity>> Withdraw(int accountId, decimal amount)
        {
            var uri = _remoteServiceBaseUrl + "/Account/Withdraw";
            var accountData = new StringContent(JsonConvert.SerializeObject(new { accountId, amount }), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, accountData);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var problemDetails = JsonConvert.DeserializeObject<JsonErrorResponse>(await response.Content.ReadAsStringAsync());
                throw new Exception(string.Join(',', problemDetails.Messages));
            }
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServiceResult<AccountEntity>>(responseString);
        }
    }
}
