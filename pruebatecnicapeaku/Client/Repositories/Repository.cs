using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pruebatecnicapeaku.Shared.Entities;
using System.Net.Http;
using System.Text;
using System.Text.Json;


namespace pruebatecnicapeaku.Client.Repositories
{
    public class Repository : IRepository
    {

        private readonly HttpClient httpClient;

        public Repository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private JsonSerializerOptions JSONDefaultOption =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var responseHTTP = await httpClient.GetAsync(url);

            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await DeserializeResponse<T>(responseHTTP, JSONDefaultOption);
                return new HttpResponseWrapper<T>(response, false, responseHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, true, responseHTTP);
            }
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T send)
        {
            var sendJSON = JsonSerializer.Serialize(send);
            var enviarContent = new StringContent(sendJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T send)
        {
            var sendJSON = JsonSerializer.Serialize(send);
            var sendContent = new StringContent(sendJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PutAsync(url, sendContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T send)
        {
            var sendJSON = JsonSerializer.Serialize(send);
            var sendContent = new StringContent(sendJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, sendContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializeResponse<TResponse>(responseHttp, JSONDefaultOption);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHTTP = await httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !responseHTTP.IsSuccessStatusCode, responseHTTP);
        }

        public List<ServiceProvider> GetServiceproviders()
        {
            return new List<ServiceProvider>() {
                new ServiceProvider(){Id=1,Email="test1@gmail.com",Name="Test1",Nit=890312771,Creationdate=DateTime.Now},
                new ServiceProvider(){Id=2,Email="test2@gmail.com",Name="Test2",Nit=890312772,Creationdate=DateTime.Now},
                new ServiceProvider(){Id=3,Email="test3@gmail.com",Name="Test3",Nit=890312773,Creationdate=DateTime.Now},
                new ServiceProvider(){Id=4,Email="test4@gmail.com",Name="Test4",Nit=890312774,Creationdate=DateTime.Now}
            };
        }
        public List<Service> GetServices()
        {
            return new List<Service>()
            {
                new Service(){Id=1,IdServiceProvider=1,Name="Service test1",Country="Colombia",price=2000}
            };
        }



    }
}
