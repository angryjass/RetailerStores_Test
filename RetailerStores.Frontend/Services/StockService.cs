using RetailerStores.Frontend.Interfaces;
using RetailerStores.Frontend.Models;
using RetailerStores.Frontend.Models.Dto;
using System.Text.Json;
using System.Text;
using System.Text.Encodings.Web;
using RetailerStores.Frontend.Extensions;
using System.Reflection;

namespace RetailerStores.Frontend.Services
{
    public class StockService : BaseService, IStockService
    {
        private const string CONTROLLER_PATH = "Stocks";
        public StockService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<StockDto> Create(CreateStockModel model)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"/{CONTROLLER_PATH}");
            var json = JsonSerializer.Serialize(model, DefaultOptions);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.GetResponse<StockDto>(httpRequest);
        }

        public async Task<StockDto?> Get(Guid storeId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/{CONTROLLER_PATH}?storeId={storeId}");

            return await _httpClient.GetResponse<StockDto?>(httpRequest);
        }
    }
}
