using EasyChef.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyChef.Shared.RestClients
{
    public interface ICategoryRestClient
    {
        Task<HttpResult> Delete(long id);
        Task<HttpResult<Category>> Get(long id);
        Task<HttpResult<Category>> Post(Category Category);
        Task<HttpResult<Category>> Put(Category Category);
    }

    public class CategoryRestClient : RestClient, ICategoryRestClient
    {
        private HttpClient _httpClient;
        private string _apiBaseUrl;

        public CategoryRestClient(HttpClient httpClient, string apiBaseUrl)
        {
            this._httpClient = httpClient;
            this._apiBaseUrl = apiBaseUrl;
        }

        public async Task<HttpResult> Delete(long id) {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + "/api/Category" + id);
            return TransformResponse(response);
        }

        public async Task<HttpResult<Category>> Get(long id) {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "/api/Category" + id);
            return await TransformResponse<Category>(response);
        }

        public async Task<HttpResult<Category>> Post(Category Category) {
            var response = await _httpClient.PostAsync(_apiBaseUrl + "/api/Category", new StringContent(JsonConvert.SerializeObject(Category), Encoding.UTF8, "application/json"));
            return await TransformResponse<Category>(response);
        }

        public async Task<HttpResult<Category>> Put(Category Category) {
            var response = await _httpClient.PutAsync(_apiBaseUrl + "/api/Category", new StringContent(JsonConvert.SerializeObject(Category), Encoding.UTF8, "application/json"));
            return await TransformResponse<Category>(response);
        }
    }
}

