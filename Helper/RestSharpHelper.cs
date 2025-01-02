using Newtonsoft.Json;
using RestSharp;

namespace SandboxRazor.Helper
{
    public class RestSharpHelper
    {
        private readonly RestClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RestSharpHelper> _logger;
        private string _cachedAccessToken = string.Empty;
        private DateTime _accessTokenExpiry = DateTime.MinValue;

        private const string BaseUrl = "https://api.businesscentral.dynamics.com/v2.0/11560066-52c2-4a64-87e5-60e371425dd5/TRAIN/ODataV4/Company('Adhi%20Cakra%20Utama%20Mulia')";

        public RestSharpHelper(IConfiguration configuration, ILogger<RestSharpHelper> logger)
        {
            _configuration = configuration;
            _logger = logger;

            _client = new RestClient(new RestClientOptions(BaseUrl)
            {
                Timeout = TimeSpan.FromMinutes(5) // More readable timeout
            });
        }

        public async Task<string> GetAccessTokenAsync()
        {
            // Return cached token if valid
            if (!string.IsNullOrEmpty(_cachedAccessToken) && DateTime.UtcNow < _accessTokenExpiry)
            {
                return _cachedAccessToken;
            }

            // Prepare OAuth request
            var tenantId = _configuration["AzureAD:TenantId"];
            var clientId = _configuration["AzureAD:ClientId"];
            var clientSecret = _configuration["AzureAD:ClientSecret"];
            var scope = "https://api.businesscentral.dynamics.com/.default";
            var tokenEndpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";

            var tokenRequestData = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "scope", scope },
                { "grant_type", "client_credentials" }
            };

            var tokenRequest = new FormUrlEncodedContent(tokenRequestData);

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.PostAsync(tokenEndpoint, tokenRequest);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error fetching access token: {response.ReasonPhrase}");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse ?? "{}");
                _cachedAccessToken = tokenResult?["access_token"]
                    ?? throw new Exception("Access token not found in response.");
                _accessTokenExpiry = DateTime.UtcNow.AddSeconds(
                    int.Parse(tokenResult?["expires_in"] ?? "3600") - 60);

                return _cachedAccessToken;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting access token: {ex.Message}");
                throw;
            }
        }

        public async Task<List<T>> GetAsync<T>(string endpoint, int pageSize = 1000)
        {
            var allData = new List<T>();
            int skip = 0;

            try
            {
                var accessToken = await GetAccessTokenAsync();
                _client.AddDefaultHeader("Authorization", $"Bearer {accessToken}");

                while (true)
                {
                    var query = $"{BaseUrl}/{endpoint}&$skip={skip}";
                    var request = new RestRequest(query, Method.Get);

                    var response = await _client.ExecuteAsync(request);

                    if (!response.IsSuccessful)
                    {
                        LogError(response, "Error fetching data");
                    }

                    var pageData = JsonConvert.DeserializeObject<ListResponse<T>>(response.Content ?? "{}");

                    if (pageData?.Value == null || !pageData.Value.Any())
                    {
                        break;
                    }

                    allData.AddRange(pageData.Value);
                    skip += pageSize;

                    if (string.IsNullOrEmpty(pageData.NextLink))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }

            return allData;
        }

        private void LogError(RestResponse response, string errorMessage)
        {
            _logger.LogError($"{errorMessage}: {response.StatusDescription}. Details: {response.Content}");
            throw new Exception($"{errorMessage}: {response.StatusDescription}. Details: {response.Content}");
        }

        public class ListResponse<T>
        {
            [JsonProperty("value")]
            public List<T> Value { get; set; } = new List<T>();

            [JsonProperty("@odata.nextLink")]
            public string? NextLink { get; set; }
        }
    }
}