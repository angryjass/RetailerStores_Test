using ChartJSCore.Helpers;
using ChartJSCore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetailerStores.Frontend.Interfaces;
using RetailerStores.Frontend.Models.Dto;

namespace RetailerStores.Frontend.Pages
{
    public class MetricsModel : PageModel
    {
        private readonly IMetricService _metricService;

        public MetricsModel(IMetricService metricService)
        {
            _metricService = metricService;
        }

        public Chart TotalStockChart { get; private set; } = new();
        public Chart StockAccuracyChart { get; private set; } = new();
        public Chart StockOnFloorAvailabilityChart { get; private set; } = new();
        public Chart StockMeanAgeChart { get; private set; } = new();

        public async Task OnGet()
        {
            TotalStockChart = CreateChart(await _metricService.GetTotalStock(), "Total Stock");
            StockAccuracyChart = CreateChart(await _metricService.GetStockAccuracy(), "Stock Accuracy");
            StockOnFloorAvailabilityChart = CreateChart(await _metricService.GetStockOnFloorAvailability(), "Stock On-Floor-Availability");
            StockMeanAgeChart = CreateChart(await _metricService.GetStockMeanAge(), "Stock Mean Age");

            
        }

        private Chart CreateChart(StockMetricDto dto, string name)
        {
            Chart chart = new Chart();

            Data data = new Data();
            data.Labels = new List<string>() { "min", "avg", "max" };

            LineDataset dataset = new LineDataset()
            {
                Label = name,
                Data = dto.Values.Select(a => ((double?)a)).ToList(),
                Type = Enums.ChartType.Bar,
            };

            data.Datasets = new List<Dataset>
            {
                dataset
            };

            chart.Data = data;

            return chart;
        }
    }
}