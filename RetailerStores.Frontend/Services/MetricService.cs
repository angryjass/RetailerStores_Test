using RetailerStores.Frontend.Interfaces;
using RetailerStores.Frontend.Models.Dto;
using RetailerStores.Frontend.Extensions;
using System;

namespace RetailerStores.Frontend.Services
{
    public class MetricService : BaseService, IMetricService
    {
        private const string CONTROLLER_PATH = "Metrics";

        public MetricService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<StockMetricDto> GetStockAccuracy()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/{CONTROLLER_PATH}/getStockAccuracy");
            return await _httpClient.GetResponse<StockMetricDto>(httpRequest);
        }

        public async Task<StockMetricDto> GetStockMeanAge()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/{CONTROLLER_PATH}/getStockMeanAge");
            return await _httpClient.GetResponse<StockMetricDto>(httpRequest);
        }

        public async Task<StockMetricDto> GetStockOnFloorAvailability()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/{CONTROLLER_PATH}/getStockOnFloorAvailability");
            return await _httpClient.GetResponse<StockMetricDto>(httpRequest);
        }

        public async Task<StockMetricDto> GetTotalStock()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/{CONTROLLER_PATH}/getTotalStock");
            return await _httpClient.GetResponse<StockMetricDto>(httpRequest);
        }
    }
}
