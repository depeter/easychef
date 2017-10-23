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
    public interface IShoppingCartRestClient
    {
        Task<HttpResult> Delete(long id);
        Task<HttpResult<ShoppingCart>> Get(long id);
        Task<HttpResult<IList<ShoppingCart>>> GetByUser(long userId);
        Task<HttpResult> Post(ShoppingCart shoppingCart);
        Task<HttpResult> Put(ShoppingCart shoppingCart);
    }

    public class ShoppingCartRestClient : RestClient, IShoppingCartRestClient
    {
        private HttpClient httpClient;
        private string apiBaseUrl;

        public ShoppingCartRestClient(HttpClient httpClient, string apiBaseUrl)
        {
            this.httpClient = httpClient;
            this.apiBaseUrl = apiBaseUrl;
        }

        public async Task<HttpResult> Delete(long id) {
            var response = await httpClient.DeleteAsync(apiBaseUrl + "/api/ShoppingCart" + id);
            return TransformResponse(response);
        }

        public async Task<HttpResult<ShoppingCart>> Get(long id) {
            var response = await httpClient.GetAsync(apiBaseUrl + "/api/ShoppingCart" + id);
            return await TransformResponse<ShoppingCart>(response);
        }

        public async Task<HttpResult<IList<ShoppingCart>>> GetByUser(long userId) {
            var response = await httpClient.GetAsync(apiBaseUrl + "/api/ShoppingCart/GetByUser/" + userId);
            return await TransformResponse<IList<ShoppingCart>>(response);
        }
        public async Task<HttpResult> Post(ShoppingCart shoppingCart) {
            var response = await httpClient.PostAsync(apiBaseUrl + "/api/ShoppingCart", new StringContent(JsonConvert.SerializeObject(shoppingCart), Encoding.UTF8, "application/json"));
            return await TransformResponse<ShoppingCart>(response);
        }
        public async Task<HttpResult> Put(ShoppingCart shoppingCart) {
            var response = await httpClient.PutAsync(apiBaseUrl + "/api/ShoppingCart", new StringContent(JsonConvert.SerializeObject(shoppingCart), Encoding.UTF8, "application/json"));
            return await TransformResponse<ShoppingCart>(response);
        }
    }

    public abstract class RestClient
    {
        protected HttpResult TransformResponse(HttpResponseMessage response)
        {
            return new HttpResult()
            {
                HttpStatus = response.StatusCode
            };
        }

        protected async Task<HttpResult<T>> TransformResponse<T>(HttpResponseMessage response)
        {
            return new HttpResult<T>()
            {
                HttpStatus = response.StatusCode,
                Content = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())
            };
        }
    }

    public class HttpResult
    {
        public HttpStatusCode HttpStatus { get; set; }
    }

    public class HttpResult<TResponse> : HttpResult
    {
        public TResponse Content { get; set; }
    }
}

