using Newtonsoft.Json;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Infra.HttpClients.Utils;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnTimeWorker.Infra.HttpClients
{
    public class UserClient
    {
        private static readonly string URL = "https://localhost:44379/user";
        private static readonly string GET_USER_URL = URL + "/GetUserByWorkerName";
        private static readonly string UPDATE_USER_URL = URL + "/update";

        public async Task<User> GetUserByWorker(Worker worker)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(worker));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(GET_USER_URL, httpContent);
                if (responseContent == null)
                    return null;

                return JsonConvert.DeserializeObject<User>(responseContent);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(user));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(UPDATE_USER_URL, httpContent);
                if (responseContent == null)
                    return false;

                return JsonConvert.DeserializeObject<bool>(responseContent);     
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
