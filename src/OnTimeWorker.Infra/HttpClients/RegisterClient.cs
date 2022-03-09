using Newtonsoft.Json;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Infra.HttpClients.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnTimeWorker.Infra.HttpClients
{
    public class RegisterClient
    {
        private static readonly string URL = "https://localhost:44379/register";
        private static readonly string GET_REGISTER_URL = URL + "/getByWorkerCurrent";
        private static readonly string INSERT_REGISTER_URL = URL + "/insert";

        public async Task<List<Register>> GetWorkerRegistersCurrentAsync(Worker worker)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(worker));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(GET_REGISTER_URL, httpContent);
                if (responseContent == null)
                    return null;

                return JsonConvert.DeserializeObject<List<Register>>(responseContent);
            } catch (Exception ex)
            {
                throw ex;
            }         
        }
        public async Task<bool> InsertNewRegister(Register register)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(register));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(INSERT_REGISTER_URL, httpContent);
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
