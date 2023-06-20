using RetailerStores.Frontend.Extensions;
using RetailerStores.Frontend.Interfaces;
using RetailerStores.Frontend.Models;
using RetailerStores.Frontend.Models.Dto;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace RetailerStores.Frontend.Services
{
    public class StoreService : BaseService, IStoreService
    {
        private const string CONTROLLER_PATH = "Stores";
        public StoreService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<StoreDto> Create(CreateStoreModel model)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"/{CONTROLLER_PATH}");
            var json = JsonSerializer.Serialize(model, DefaultOptions);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.GetResponse<StoreDto>(httpRequest);
        }

        public async Task<StoreExtendedDto> Get(Guid id)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/{CONTROLLER_PATH}?id={id}");

            return await _httpClient.GetResponse<StoreExtendedDto>(httpRequest);
        }

        public async Task<List<StoreDto>> GetAll()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/{CONTROLLER_PATH}/list");

            return await _httpClient.GetResponse<List<StoreDto>>(httpRequest);
        }

        public async Task<StoreDto> Update(UpdateStoreModel model)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"/{CONTROLLER_PATH}");
            var json = JsonSerializer.Serialize(model, DefaultOptions);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.GetResponse<StoreDto>(httpRequest);
        }
    }
}
