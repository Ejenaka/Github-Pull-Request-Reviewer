using System.Net.Http.Json;
using GithubPullRequestReviewer.DataAccess.Contracts;

namespace GithubPullRequestReviewer.DataAccess.ApiClients
{
    public abstract class BaseApiClient
    {
        private readonly HttpClient _httpClient;

        protected BaseApiClient(string clientUrl, ITokenService gitHubTokenService)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(clientUrl);
            _httpClient.DefaultRequestHeaders.Add("access_token", gitHubTokenService.GetToken());
        }

        protected async Task<T?> RunGetRequestAsync<T>(string relativeUrl)
        {
            return await _httpClient.GetFromJsonAsync<T>(relativeUrl);
        }

        protected async Task<string> RunGetRequestAsync(string relativeUrl)
        {
            var response = await _httpClient.GetAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task RunDeleteRequestAsync(string relativeUrl)
        {
            var response = await _httpClient.DeleteAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
        }

        protected async Task RunPostRequestAsync<T>(string relativeUrl, T body)
        {
            var response = await _httpClient.PostAsJsonAsync(relativeUrl, body);
            response.EnsureSuccessStatusCode();
        }

        protected async Task<R?> RunPostRequestAsync<T, R>(string relativeUrl, T body)
        {
            var response = await _httpClient.PostAsJsonAsync(relativeUrl, body);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<R>();
        }

        protected async Task RunPutRequestAsync<T>(string relativeUrl, T body)
        {
            var response = await _httpClient.PutAsJsonAsync(relativeUrl, body);
            response.EnsureSuccessStatusCode();
        }

        protected async Task RunPatchRequestAsync<T>(string relativeUrl, T body)
        {
            var response = await _httpClient.PatchAsJsonAsync(relativeUrl, body);
            response.EnsureSuccessStatusCode();
        }
    }
}
