using Newtonsoft.Json;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Infra.HttpClients.Utils;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnTimeWorker.Infra.HttpClients
{
    public class WorkerClient
    {
        private static readonly string URL = "https://localhost:44379/worker";
        private static readonly string UPDATE_WORKER_URL = URL + "/update";
        private static readonly string GET_STATUS_URL = URL + "/GetStatusByWorkerId";
        private static readonly string UPDATE_STATUS_URL = URL + "/UpdateWorkingStatus";

        public async Task<bool> UpdateWorker(Worker worker)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(worker));
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(UPDATE_WORKER_URL, httpContent);
                if (responseContent == null)
                    return false;

                return JsonConvert.DeserializeObject<bool>(responseContent);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<WorkerStatus> GetStatusByWorkerId(Worker worker)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(worker));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(GET_STATUS_URL, httpContent);
                if (responseContent == null)
                    return null;

                return JsonConvert.DeserializeObject<WorkerStatus>(responseContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateWorkingStatus(Worker worker)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(worker));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(UPDATE_STATUS_URL, httpContent);
                if (responseContent == null)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
