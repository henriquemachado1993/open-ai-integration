using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string url, Dictionary<string, string> headers)
        {
            var httpRequestMesssage = new HttpRequestMessage(HttpMethod.Get, CreateUri(url)).Headers(headers);
            return client.SendAsync(httpRequestMesssage);
        }

        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string url, T value, MediaTypeFormatter mediaTypeFormatter, Dictionary<string, string> headers)
        {
            var content = new ObjectContent<T>(value, mediaTypeFormatter);
            return client.PostAsync(url, content, headers);
        }

        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string url, HttpContent httpContent, Dictionary<string, string> headers)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, CreateUri(url)).Headers(headers);
            request.Content = httpContent;
            return client.SendAsync(request);
        }

        private static Uri CreateUri(String uri)
        {
            if (string.IsNullOrWhiteSpace(uri))            
                return null;
            var uriResponse = new Uri(uri, UriKind.RelativeOrAbsolute);
            return uriResponse;
        }

        private static HttpRequestMessage Headers(this HttpRequestMessage httpRequestMessage, Dictionary<string, string> headers)
        {
            if (httpRequestMessage == null || headers == null)
                return httpRequestMessage;
            foreach (var item in headers)
            {
                if (item.Key == nameof(httpRequestMessage.Headers.Accept) && !string.IsNullOrWhiteSpace(item.Value))
                {
                    var acceptHeader = item.Value;
                    httpRequestMessage.Headers.Accept.ParseAdd(acceptHeader);
                    continue;
                }
                httpRequestMessage.Headers.Add(item.Key, item.Value);
            }
            return httpRequestMessage;
        }
    }
}
