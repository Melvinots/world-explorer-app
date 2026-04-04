using ApexCharts;
using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Services.Currency;

namespace WorldExplorer.Web.Pages
{
    public partial class Currencies
    {
        [Inject] private ICurrencyService CurrencyService { get; set; } = default!;

        private List<CurrencyModel> _currencies = new();
        //private string _fromCurrency = "USD";
        //private string _toCurrency = "PHP";
        //private decimal _amount = 1;
        //private ConversionResult? _conversionResult;
        //private List<RateDataPoint> _dataPoints = new();
        //private string _selectedRange = "1Y";

        //private bool _isLoadingCurrencies = true;
        //private bool _isConverting = false;
        //private bool _isLoadingChart = false;

        private string _fromCurrency = "USD";
        private string _toCurrency = "PHP";
        private decimal _amount = 1;

        private ConversionResult? _conversionResult = new ConversionResult
        {
            Amount = 1,
            Base = "USD",
            Date = "2024-01-01",
            Rates = new Dictionary<string, decimal> { { "PHP", 56.50m } }
        };

        private List<RateDataPoint> _dataPoints = new()
    {
        new RateDataPoint { Date = new DateOnly(2024, 1, 1),  Rate = 55.10m },
        new RateDataPoint { Date = new DateOnly(2024, 2, 1),  Rate = 55.85m },
        new RateDataPoint { Date = new DateOnly(2024, 3, 1),  Rate = 56.20m },
        new RateDataPoint { Date = new DateOnly(2024, 4, 1),  Rate = 56.75m },
        new RateDataPoint { Date = new DateOnly(2024, 5, 1),  Rate = 57.10m },
        new RateDataPoint { Date = new DateOnly(2024, 6, 1),  Rate = 56.90m },
        new RateDataPoint { Date = new DateOnly(2024, 7, 1),  Rate = 57.50m },
        new RateDataPoint { Date = new DateOnly(2024, 8, 1),  Rate = 57.80m },
        new RateDataPoint { Date = new DateOnly(2024, 9, 1),  Rate = 58.10m },
        new RateDataPoint { Date = new DateOnly(2024, 10, 1), Rate = 57.90m },
        new RateDataPoint { Date = new DateOnly(2024, 11, 1), Rate = 58.40m },
        new RateDataPoint { Date = new DateOnly(2024, 12, 1), Rate = 58.75m },
    };

        private string _selectedRange = "1Y";
        private bool _isLoadingCurrencies = false;
        private bool _isConverting = false;
        private bool _isLoadingChart = false;

        private readonly Dictionary<string, DateOnly> _dateRanges = new()
        {
            { "1M", DateOnly.FromDateTime(DateTime.Today.AddMonths(-1)) },
            { "3M", DateOnly.FromDateTime(DateTime.Today.AddMonths(-3)) },
            { "6M", DateOnly.FromDateTime(DateTime.Today.AddMonths(-6)) },
            { "1Y", DateOnly.FromDateTime(DateTime.Today.AddYears(-1)) },
            { "5Y", DateOnly.FromDateTime(DateTime.Today.AddYears(-5)) },
        };

        private ApexChartOptions<RateDataPoint> _chartOptions = new()
        {
            Chart = new Chart
            {
                Toolbar = new Toolbar { Show = false },
                Zoom = new Zoom { Enabled = false },
                Width = "100%",
            },
            Stroke = new Stroke { Curve = Curve.Smooth, Width = new List<int> { 2 } },
            Tooltip = new Tooltip { X = new TooltipX { Format = "dd MMM yyyy" } },
            Xaxis = new XAxis { Type = XAxisType.Datetime },
        };

        protected override async Task OnInitializedAsync()
        {
            _currencies = await CurrencyService.GetCurrenciesAsync();
            _isLoadingCurrencies = false;
        }

        private async Task ConvertAsync()
        {
            _isConverting = true;
            _conversionResult = await CurrencyService.ConvertAsync(_fromCurrency, _toCurrency, _amount);
            _isConverting = false;

            await LoadChartAsync();
        }

        private async Task LoadChartAsync()
        {
            _isLoadingChart = true;
            var startDate = _dateRanges[_selectedRange];
            var result = await CurrencyService.GetHistoricalRatesAsync(_fromCurrency, _toCurrency, startDate);
            _dataPoints = result?.ToDataPoints(_toCurrency) ?? new();
            _isLoadingChart = false;
        }

        private async Task SelectRangeAsync(string range)
        {
            _selectedRange = range;
            if (_dataPoints.Any())
                await LoadChartAsync();
        }

        private void SwapCurrencies()
        {
            (_fromCurrency, _toCurrency) = (_toCurrency, _fromCurrency);
            _conversionResult = null;
            _dataPoints = new();
        }
    }
}