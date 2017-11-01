using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Shared.RestClients;
using Newtonsoft.Json;

namespace EasyChef.Contracts.Shared.RestClients
{
    public interface IProductRestClient
    {
        Task<HttpResult> Delete(long id);
        Task<HttpResult<ProductDTO>> Get(long id);
        Task<HttpResult<ProductDTO>> Post(ProductDTO productDto);
        Task<HttpResult<ProductDTO>> Put(ProductDTO productDto);
        Task<HttpResult<ProductDTO>> GetBySku(string sku);
    }

    public class ProductRestClient : RestClient, IProductRestClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ProductRestClient(HttpClient httpClient, string apiBaseUrl)
        {
            this._httpClient = httpClient;
            this._apiBaseUrl = apiBaseUrl;
        }

        public async Task<HttpResult> Delete(long id)
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + "/api/Product" + id);
            return TransformResponse(response);
        }

        public async Task<HttpResult<ProductDTO>> Get(long id)
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "/api/Product" + id);
            return await TransformResponse<ProductDTO>(response);
        }

        public async Task<HttpResult<ProductDTO>> Post(ProductDTO productDto)
        {
            var response = await _httpClient.PostAsync(_apiBaseUrl + "/api/Product",
                new StringContent(JsonConvert.SerializeObject(productDto, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                }),
                Encoding.UTF8, "application/json"));
            return await TransformResponse<ProductDTO>(response);
        }

        public async Task<HttpResult<ProductDTO>> Put(ProductDTO productDto)
        {
            var response = await _httpClient.PutAsync(_apiBaseUrl + "/api/Product",
                new StringContent(JsonConvert.SerializeObject(productDto, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                }),
                Encoding.UTF8, "application/json"));
            return await TransformResponse<ProductDTO>(response);
        }

        public async Task<HttpResult<ProductDTO>> GetBySku(string sku)
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "/api/Product/GetBySku/" + sku);
            return await TransformResponse<ProductDTO>(response);
        }
    }
}

