using System.Net.Http;
using Code_editor.Interfaces;
using Code_editor.Models;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Media;

namespace Code_editor.Services
{
    public class HttpService : IHttpService
    {
        private const string ApiKey = "ae47971fa534bee4a4d084ce7b4c0c19992c8ea2";
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {                           
            _httpClient = httpClient;
        }

        public async Task<RequestBody> SendCodeAsync(string language, string sourceCode, int memoryLimit, int timeLimit)
        {
            if (string.IsNullOrEmpty(sourceCode))
            {
                throw new ArgumentNullException(nameof(sourceCode));
            }

            try
            {
                RequestBody requestBody = new RequestBody(
                    sourceCode,
                    language,
                    memoryLimit,
                    timeLimit

                );

                string json = JsonSerializer.Serialize(requestBody);

                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.hackerearth.com/v4/partner/code-evaluation/submissions/");
                request.Headers.Add("client-secret", ApiKey);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(json, Encoding.UTF8);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var createdPet = JsonSerializer.Deserialize<RequestBody>(content);
                return createdPet;
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }
    }
}
