using Newtonsoft.Json;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Infra.HttpClients.Utils;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnTimeWorker.Infra.HttpClients
{
    public class LoginClient
    {
        private static readonly string URL = "https://localhost:44379/login";
        private static readonly string LOGIN_URL = URL + "/login";

        public async Task<Worker> LoginAsync(User user)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(user));
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(LOGIN_URL, httpContent);
                if (responseContent == null)
                    return null;

                return JsonConvert.DeserializeObject<Worker>(responseContent);
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
