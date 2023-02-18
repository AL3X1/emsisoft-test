using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace EmsisoftTest.Tests.Infrastructure
{
    public static class HttpExtensions
    {
        public static async Task<HttpResponseMessage> PostAsync(this Task<HttpClient> client, string endpoint,
            string content)
        {
            return await (await client).PostAsync(endpoint, content.ToJsonContent());
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this Task<HttpClient> client, string endpoint,
            T content)
        {
            return await (await client).PostAsync(endpoint, content.ToJsonContent());
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string endpoint, T content)
        {
            return await client.PostAsync(endpoint, content.ToJsonContent());
        }

        public static async Task<HttpResponseMessage> PostAsync(this HttpClient client, string endpoint)
        {
            return await client.PostAsync(endpoint, null);
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string endpoint, T content)
        {
            return await client.PutAsync(endpoint, content.ToJsonContent());
        }

        public static async Task<HttpResponseMessage> PutAsync(this HttpClient client, string endpoint)
        {
            return await client.PutAsync(endpoint, null);
        }

        public static async Task<HttpResponseMessage> GetAsync(this Task<HttpClient> client, string endpoint)
        {
            return await (await client).GetAsync(endpoint);
        }

        public static async Task<HttpResponseMessage> GetAsync(this HttpClient client, string endpoint)
        {
            return await client.GetAsync(endpoint);
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this Task<HttpClient> client, string endpoint,
            T content)
        {
            return await (await client).PutAsync(endpoint, content.ToJsonContent());
        }

        public static HttpContent ToJsonContent<T>(this T content)
        {
            var jsonStringContent = JsonConvert.SerializeObject(content, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return new StringContent(jsonStringContent, Encoding.UTF8, "application/json");
        }

        public static HttpContent ToJsonContent(this string content)
        {
            return ToJsonContent<string>(content);
        }

        public static async Task<T> ToModel<T>(this HttpResponseMessage responseMessage)
            where T : class
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public static async Task<T> ToModel<T>(this Task<HttpResponseMessage> responseMessage)
            where T : class
        {
            var response = await responseMessage;
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public static Task<string> GetResponseStringAsync(this HttpResponseMessage responseMessage) => responseMessage.Content.ReadAsStringAsync();

        public static async Task<HttpResponseMessage> ValidateStatusCode(this Task<HttpResponseMessage> responseMessage,
            Action<HttpStatusCode> action)
        {
            var response = await responseMessage;
            action(response.StatusCode);
            return response;
        }
    }
}