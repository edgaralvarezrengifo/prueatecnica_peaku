using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pruebatecnicapeaku.Shared.Entities;

namespace pruebatecnicapeaku.Client.Repositories
{
    public interface IRepository
    {
        List<ServiceProvider> GetServiceproviders();
        List<Service> GetServices();
        Task<HttpResponseWrapper<object>> Delete(string url);
        Task<HttpResponseWrapper<T>> Get<T>(string url);
        Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar);
        Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar);
    }
}
