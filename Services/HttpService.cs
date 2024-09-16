using System.Net.Http;
using Code_editor.Interfaces;
using Code_editor.Models;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Code_editor.Services
{
    public class HttpService : IHttpService
    {
        private const string ApiKey = "ae47971fa534bee4a4d084ce7b4c0c19992c8ea2";
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpService> _logger; 

        public HttpService(HttpClient httpClient, ILogger<HttpService> logger = null) 
        {
            _httpClient = httpClient;
            _logger = logger; 
        }

        public async Task<ResponseBody> SendCodeAsync(string language, string sourceCode, int memoryLimit, int timeLimit)
        {
            if (string.IsNullOrEmpty(sourceCode))
            {
                throw new ArgumentNullException(nameof(sourceCode), "Source code cannot be empty");
            }

            try
            {
                RequestBody requestBody = new RequestBody(
                    sourceCode,
                    language,
                    memoryLimit,
                    timeLimit
                );

                string json = JsonConvert.SerializeObject(requestBody);

                using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.hackerearth.com/v4/partner/code-evaluation/submissions/");
                request.Headers.Add("client-secret", ApiKey);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(json, Encoding.UTF8);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                _logger?.LogInformation("Sending POST request to HackerEarth API.");

                using (var response = await _httpClient.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        _logger?.LogError($"API Error: {response.StatusCode} - {errorContent}");
                        throw new Exception($"API Error: {response.StatusCode} - {errorContent}");
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var responseBody = JsonConvert.DeserializeObject<ResponseBody>(content);

                    var resultUrl = responseBody.StatusUpdateUrl;
                    if (!Uri.IsWellFormedUriString(resultUrl, UriKind.Absolute))
                    {
                        _logger?.LogError("Invalid URL received for status update.");
                        throw new Exception("Invalid URL received for status update.");
                    }

                    _logger?.LogInformation($"Result URL: {resultUrl}");

                    ResponseBody resultResponse = null;
                    while (resultResponse == null || string.IsNullOrEmpty(resultResponse.Result.ToString()))
                    {
                        await Task.Delay(1000);
                        var responseBodyUpdate = await GetUpdateUrl(resultUrl);
                        resultResponse = JsonConvert.DeserializeObject<ResponseBody>(responseBodyUpdate);
                    }

                    return  resultResponse;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError($"Error while sending code: {ex.Message}");
                throw new Exception($"Error while sending code: {ex.Message}");
            }
        }

        private async Task<string> GetUpdateUrl(string url)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                _logger?.LogError($"Invalid URL: {url}");
                throw new Exception("Invalid URL received for status update.");
            }

            var requestUpdate = new HttpRequestMessage(HttpMethod.Get, url);
            requestUpdate.Headers.Add("client-secret", ApiKey);

            _logger?.LogInformation($"Sending GET request to URL: {url}");

            var response = await _httpClient.SendAsync(requestUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger?.LogError($"API Error (Update URL): {response.StatusCode} - {errorContent}");
                throw new Exception($"API Error: {response.StatusCode} - {errorContent}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }


        public async Task<string> GetResultOutput(string urlCode)
        {
            try
            {
                if (!Uri.IsWellFormedUriString(urlCode, UriKind.Absolute))
                {
                    _logger?.LogError($"Invalid result URL: {urlCode}");
                    throw new Exception("Invalid result URL.");
                }

                var requestCode = new HttpRequestMessage(HttpMethod.Get, urlCode);
                requestCode.Headers.Add("client-secret", ApiKey);

                _logger?.LogInformation($"Sending GET request to retrieve output from URL: {urlCode}");

                var response = await _httpClient.SendAsync(requestCode);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger?.LogError($"API Error (Result URL): {response.StatusCode} - {errorContent}");
                    throw new Exception($"API Error: {response.StatusCode} - {errorContent}");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception ex) 
            {
            return ex.Message;
            }
        }
    }
}
