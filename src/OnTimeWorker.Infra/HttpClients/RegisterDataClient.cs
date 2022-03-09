using Newtonsoft.Json;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Core.Models.Filters;
using OnTimeWorker.Infra.HttpClients.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnTimeWorker.Infra.HttpClients
{
    public class RegisterDataClient
    {
        private static readonly string URL = "https://localhost:44379/registerData";

        private static readonly string GET_YEARS_URL = URL + "/GetAllYearsFromWorkerDataRegisters";
        private static readonly string GET_MONTHS_URL = URL + "/GetAllMonthsFromYear";
        private static readonly string GET_FILTERED_REGISTERS_URL = URL + "/GetFilteredDataRegisters";

        private static readonly string GET_DATAREGISTER_URL = URL + "/GetDataRegisterById";
        private static readonly string UPDATE_DATAREGISTER_URL = URL + "/update";

        //
        public async Task<List<string>> GetAllYearsFromWorkerDataRegisters(Worker worker)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(worker));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(GET_YEARS_URL, httpContent);
                if (responseContent == null)
                    return null;

                return JsonConvert.DeserializeObject<List<string>>(responseContent);
            } catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Month>> GetAllMonthsFromYear(GetDistinctMonthsFromYear getDistinctMonthsFromYear)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(getDistinctMonthsFromYear));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(GET_MONTHS_URL, httpContent);
                if (responseContent == null)
                    return null;

                return JsonConvert.DeserializeObject<List<Month>>(responseContent);
            } catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<RegisterData>> GetFilteredDataRegisters(GetFilteredRegisters getFilteredRegisters)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(getFilteredRegisters));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(GET_FILTERED_REGISTERS_URL, httpContent);
                if (responseContent == null)
                    return null;

                return JsonConvert.DeserializeObject<List<RegisterData>>(responseContent);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        public async Task<RegisterData> GetDataRegisterById(RegisterData registerData)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(registerData));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(GET_DATAREGISTER_URL, httpContent);
                if (responseContent == null)
                    return null;

                return JsonConvert.DeserializeObject<RegisterData>(responseContent);
            } catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update(RegisterData registerData)
        {
            try
            {
                string json = await Task.Run(() => JsonConvert.SerializeObject(registerData));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                string responseContent = await HttpClientUtils.GetHttpClientResponse(UPDATE_DATAREGISTER_URL, httpContent);
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
