using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Shared.RestClients;
using Newtonsoft.Json;

namespace EasyChef.Contracts.Shared.RestClients
{
    public interface ICategoryRestClient
    {
        Task<HttpResult> Delete(long id);
        Task<HttpResult<CategoryDTO>> Get(long id);
        Task<HttpResult<CategoryDTO>> Post(CategoryDTO categoryDto);
        Task<HttpResult<CategoryDTO>> Put(CategoryDTO categoryDto);
        Task<HttpResult<IList<CategoryDTO>>> List();
    }

    public class CategoryRestClient : RestClient, ICategoryRestClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public CategoryRestClient(HttpClient httpClient, string apiBaseUrl)
        {
            this._httpClient = httpClient;
            this._apiBaseUrl = apiBaseUrl;
        }

        public async Task<HttpResult> Delete(long id) {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + "/api/Category" + id);
            return TransformResponse(response);
        }

        public async Task<HttpResult<CategoryDTO>> Get(long id) {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "/api/Category" + id);
            return await TransformResponse<CategoryDTO>(response);
        }

        public async Task<HttpResult<CategoryDTO>> Post(CategoryDTO categoryDto) {
            var response = await _httpClient.PostAsync(_apiBaseUrl + "/api/Category", new StringContent(JsonConvert.SerializeObject(categoryDto), Encoding.UTF8, "application/json"));
            return await TransformResponse<CategoryDTO>(response);
        }

        public async Task<HttpResult<CategoryDTO>> Put(CategoryDTO categoryDto) {
            var response = await _httpClient.PutAsync(_apiBaseUrl + "/api/Category", new StringContent(JsonConvert.SerializeObject(categoryDto), Encoding.UTF8, "application/json"));
            return await TransformResponse<CategoryDTO>(response);
        }

        public async Task<HttpResult<IList<CategoryDTO>>> List()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "/api/Category/List");
            return await TransformResponse<IList<CategoryDTO>>(response);
        }
    }
}

