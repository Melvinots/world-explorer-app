using ApexCharts;
using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Services.Currency;

namespace WorldExplorer.Web.Components.Currency
{
    public partial class CurrencyChart
    {
        #region Injects
        [Inject] private ICurrencyService CurrencyService { get; set; } = default!;
        #endregion

        #region Parameters
        [Parameter] public string FromCurrency { get; set; } = "USD";
        [Parameter] public string ToCurrency { get; set; } = "PHP";
        #endregion

        #region Fields
        private List<RateDataPoint> _dataPoints = new();
        #endregion

        #region State
        private bool _isLoadingChart = false;
        #endregion

        #region Chart Options
        private ApexChartOptions<RateDataPoint> _chartOptions = new()
        {
            Chart = new Chart
            {
                Toolbar = new Toolbar { Show = false },
                Zoom = new Zoom { Enabled = false },
                Background = "transparent",
                Width = "100%",
                ForeColor = "var(--text-muted)",
            },
            Stroke = new Stroke { Curve = Curve.Smooth, Width = new List<int> { 2 } },
            Tooltip = new Tooltip { X = new TooltipX { Format = "dd MMM yyyy" } },
            Xaxis = new XAxis
            {
                Type = XAxisType.Datetime,
                Labels = new XAxisLabels { Style = new AxisLabelStyle { Colors = "var(--text-muted)" } }
            },
            Yaxis = new List<YAxis>
            {
                new YAxis
                {
                    Labels = new YAxisLabels { Style = new AxisLabelStyle { Colors = "var(--text-muted)" } }
                }
            },
            Grid = new Grid { BorderColor = "var(--card-border)" },
            Theme = new Theme { Mode = Mode.Dark }
        };
        #endregion

        #region Public Methods
        public async Task LoadAsync(string fromCurrency, string toCurrency)
        {
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;
            _isLoadingChart = true;
            StateHasChanged();

            var startDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-1));
            var result = await CurrencyService.GetHistoricalRatesAsync(fromCurrency, toCurrency, startDate);

            _dataPoints = result?.ToDataPoints(toCurrency) ?? new();
            _isLoadingChart = false;
            StateHasChanged();
        }

        public void Clear()
        {
            _dataPoints = new();
            StateHasChanged();
        }
        #endregion
    }
}