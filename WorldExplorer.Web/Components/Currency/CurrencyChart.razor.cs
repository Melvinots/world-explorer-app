using ApexCharts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WorldExplorer.Web.Integrations.Frankfurter.Dtos;
using WorldExplorer.Web.Services.Currency;
using WorldExplorer.Web.Services.Theme;

namespace WorldExplorer.Web.Components.Currency
{
    public partial class CurrencyChart : IDisposable
    {
        [Inject] private ICurrencyService CurrencyService { get; set; } = default!;
        [Inject] private IThemeService ThemeService { get; set; } = default!;
        [Inject] private ILogger<CurrencyChart> Logger { get; set; } = default!;

        private ApexChart<RateDataPoint>? _apexChart;

        private List<RateDataPoint> _dataPoints = new();
        private bool _isLoadingChart;
        private bool _hasLoaded;
        private bool _disposed;

        private int _latestRequestId;

        public string FromCurrency { get; private set; } = "USD";
        public string ToCurrency { get; private set; } = "PHP";

        private readonly ApexChartOptions<RateDataPoint> _chartOptions = new()
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
            Grid = new Grid { BorderColor = "var(--card-border)" }
        };

        protected override void OnInitialized()
        {
            SetThemeMode();
            ThemeService.OnThemeChanged += OnThemeChangedHandler;
        }

        public async Task LoadAsync(string from, string to, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(from))
                throw new ArgumentException("Currency code is required.", nameof(from));

            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentException("Currency code is required.", nameof(to));

            var fromCode = from.Trim().ToUpperInvariant();
            var toCode = to.Trim().ToUpperInvariant();
            var requestId = ++_latestRequestId;

            FromCurrency = fromCode;
            ToCurrency = toCode;
            _isLoadingChart = true;
            Refresh();

            try
            {
                var startDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1));
                var result = await CurrencyService.GetHistoricalRatesAsync(
                    fromCode, toCode, startDate, cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();
                if (requestId != _latestRequestId) return; // superseded or cleared

                _dataPoints = result?.ToDataPoints(toCode) ?? new();
                _hasLoaded = true;
            }
            catch (Exception ex) when (ex is not OperationCanceledException && requestId == _latestRequestId)
            {
                _dataPoints = new();
                throw;
            }
            finally
            {
                if (requestId == _latestRequestId)
                {
                    _isLoadingChart = false;
                    Refresh();
                }
            }
        }

        public void Clear()
        {
            _latestRequestId++;
            _dataPoints = new();
            _isLoadingChart = false;
            _hasLoaded = false;
            Refresh();
        }

        private void OnThemeChangedHandler() => _ = InvokeAsync(ApplyThemeAsync);

        private async Task ApplyThemeAsync()
        {
            if (_disposed) return;

            try
            {
                SetThemeMode();
                StateHasChanged();

                if (_apexChart is not null)
                    await _apexChart.UpdateOptionsAsync(true, false, false);
            }
            catch (JSDisconnectedException)
            {
                // The user's connection closed while the theme was changing.
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, "Could not apply the theme to the currency chart.");
            }
        }

        private void SetThemeMode()
        {
            _chartOptions.Theme = new Theme
            {
                Mode = ThemeService.IsDarkMode ? Mode.Dark : Mode.Light
            };
        }

        private void Refresh()
        {
            if (!_disposed) StateHasChanged();
        }

        public void Dispose()
        {
            _disposed = true;
            ThemeService.OnThemeChanged -= OnThemeChangedHandler;
        }
    }
}