using Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    public static class HttpClientHelper
    {

        private static HttpClient CreateHttpClient(string mediaType = "application/json")
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            return httpClient;
        }

        public static async Task<BusinessResult<T>> GetAsync<T>(string requestUri, string mediaType = "application/json")
        {
            using (var httpClient = CreateHttpClient(mediaType))
            {
                var response = await httpClient.GetAsync(requestUri);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = string.Empty;
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    using (var streamReader = new StreamReader(responseStream))
                        errorContent = await streamReader.ReadToEndAsync();

                    return BusinessResult<T>.CreateInvalidResult(errorContent);
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BusinessResult<T>>(json);
            }
        }

        public static async Task<BusinessResult<TResponse>> PostAsync<TRequest, TResponse>(string requestUri, TRequest request, string mediaType = "application/json")
        {
            using (var httpClient = CreateHttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, mediaType);

                var response = await httpClient.PostAsync(requestUri, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = string.Empty;
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    using (var streamReader = new StreamReader(responseStream))
                        errorContent = await streamReader.ReadToEndAsync();

                    return BusinessResult<TResponse>.CreateInvalidResult(errorContent);

                }

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BusinessResult<TResponse>>(responseJson);
            }
        }

        public static async Task<bool> PutAsync<T>(string requestUri, T request, string mediaType = "application/json")
        {
            using (var httpClient = CreateHttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, mediaType);

                var response = await httpClient.PutAsync(requestUri, content);

                return response.IsSuccessStatusCode;
            }
        }
    }
}
