using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Shared.RestClients;
using Newtonsoft.Json;

namespace EasyChef.Contracts.Shared.RestClients
{
    public interface IRecepyRestClient
    {
        Task<HttpResult> Delete(long id);
        Task<HttpResult<RecepyDTO>> Get(long id);
        Task<HttpResult<RecepyDTO>> Post(RecepyDTO recepyDto);
        Task<HttpResult<RecepyDTO>> Put(RecepyDTO recepyDto);
    }

    public class RecepyRestClient : RestClient, IRecepyRestClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public RecepyRestClient(HttpClient httpClient, string apiBaseUrl)
        {
            this._httpClient = httpClient;
            this._apiBaseUrl = apiBaseUrl;
        }

        public async Task<HttpResult> Delete(long id)
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + "/api/Recepy" + id);
            return TransformResponse(response);
        }

        public async Task<HttpResult<RecepyDTO>> Get(long id)
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "/api/Recepy" + id);
            return await TransformResponse<RecepyDTO>(response);
        }

        public async Task<HttpResult<RecepyDTO>> Post(RecepyDTO recepyDto)
        {
            var response = await _httpClient.PostAsync(_apiBaseUrl + "/api/Recepy",
                new StringContent(JsonConvert.SerializeObject(recepyDto, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                }),
                Encoding.UTF8, "application/json"));
            return await TransformResponse<RecepyDTO>(response);
        }

        public async Task<HttpResult<RecepyDTO>> Put(RecepyDTO recepyDto)
        {
            var response = await _httpClient.PutAsync(_apiBaseUrl + "/api/Recepy",
                new StringContent(JsonConvert.SerializeObject(recepyDto, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                }),
                Encoding.UTF8, "application/json"));
            return await TransformResponse<RecepyDTO>(response);
        }
    }
}
