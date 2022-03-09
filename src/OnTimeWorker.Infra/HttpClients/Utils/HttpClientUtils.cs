using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnTimeWorker.Infra.HttpClients.Utils
{
    public class HttpClientUtils
    {
        public static async Task<string> GetHttpClientResponse(string url, HttpContent httpContent)
        {
            if (url == null || url == "")
                return null;

            if (httpContent == null)
                return null;

            using var httpClient = new HttpClient();

            var httpResponse = await httpClient.PostAsync(url, httpContent);
            if (httpResponse.Content == null)
            {
                return null;
            }

            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}
