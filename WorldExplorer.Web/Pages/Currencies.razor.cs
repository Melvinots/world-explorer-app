using ApexCharts;
using MudBlazor;
using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Services.Currency;
using WorldExplorer.Web.Services.Theme;
using System.Threading.Tasks;

namespace WorldExplorer.Web.Pages
{
    public partial class Currencies : IDisposable
    {
        #region Injects
        [Inject] private ICurrencyService CurrencyService { get; set; } = default!;
        [Inject] private IThemeService ThemeService { get; set; } = default!;
        #endregion

        #region Fields
        private List<CurrencyModel> _currencies = new();
        private string _fromCurrency = "USD";
        private string _toCurrency = "PHP";
        private decimal _amount = 1;
        private List<RateDataPoint> _dataPoints = new();
        private decimal _baseRate = 0;
        private ConversionResult? _conversionResult = new ConversionResult
        {
            Amount = 1,
            Base = "USD",
            Date = "--/--",
            Rates = new Dictionary<string, decimal> { { "PHP", 0 } }
        };
        #endregion

        #region State
        private bool _isLoadingCurrencies = true;
        private bool _isConverting = true;
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

        #region Lifecycle
        protected override async Task OnInitializedAsync()
        {
            ThemeService.OnThemeChanged += StateHasChanged;
            _currencies = await CurrencyService.GetCurrenciesAsync();
            await ConvertAsync();
            _isLoadingCurrencies = false;
        }

        public void Dispose()
        {
            ThemeService.OnThemeChanged -= StateHasChanged;
        }
        #endregion

        #region Actions
        private async Task ConvertAsync()
        {
            _isConverting = true;

            if (_fromCurrency == _toCurrency)
            {
                await Task.Delay(700);
                _conversionResult = new ConversionResult
                {
                    Amount = _amount,
                    Base = _fromCurrency,
                    Date = DateTime.Today.ToString("yyyy-MM-dd"),
                    Rates = new Dictionary<string, decimal> { { _toCurrency, _amount } }
                };
                _baseRate = 1;
                _isConverting = false;
                return;
            }

            await Task.Delay(300);
            var result = await CurrencyService.ConvertAsync(_fromCurrency, _toCurrency, 1);

            if (result is not null)
            {
                _baseRate = result.Rates[_toCurrency];
                result.Rates[_toCurrency] = Math.Round(_baseRate * _amount, 4);
                _conversionResult = result;
            }

            _isConverting = false;
            await LoadChartAsync();
        }

        private void SwapCurrencies()
        {
            if (_fromCurrency == _toCurrency)
                return;

            (_fromCurrency, _toCurrency) = (_toCurrency, _fromCurrency);
            ResetConversion();
        }
        #endregion

        #region Helpers
        private void RecalculateAsync()
        {
            if (_baseRate == 0 || _conversionResult is null)
                return;

            _conversionResult.Rates[_toCurrency] = Math.Round(_baseRate * _amount, 4);
        }

        private void ResetConversion()
        {
            _conversionResult = new ConversionResult
            {
                Amount = _amount,
                Base = _fromCurrency,
                Date = "--/--",
                Rates = new Dictionary<string, decimal> { { _toCurrency, 0 } }
            };
            _baseRate = 0;
            _dataPoints = new();
        }

        private async Task LoadChartAsync()
        {
            _isLoadingChart = true;
            var startDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-1));
            var result = await CurrencyService.GetHistoricalRatesAsync(_fromCurrency, _toCurrency, startDate);
            _dataPoints = result?.ToDataPoints(_toCurrency) ?? new();
            _isLoadingChart = false;
        }
        #endregion

        #region Theme Management
        private MudTheme _myTheme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Background = "#ffffff",
                Surface = "#ffffff",
                TextPrimary = "#2a2d3e",
                TextSecondary = "#4a4f6a",
                TextDisabled = "#9da3b4",
                Primary = "#1a7fd4",
                Divider = "#e0e4f0",
                DrawerBackground = "#f8f9ff",
                AppbarBackground = "rgba(255, 255, 255, 0.75)",
                ActionDefault = "#4a4f6a",
            },
            PaletteDark = new PaletteDark()
            {
                Background = "#1e2130",
                Surface = "#252837",
                TextPrimary = "#c8cbe0",
                TextSecondary = "#8b90a8",
                TextDisabled = "#5a5f78",
                Primary = "#3b9eff",
                Divider = "#313449",
                DrawerBackground = "#181b27",
                AppbarBackground = "rgba(30, 33, 48, 0.75)",
                ActionDefault = "#8b90a8",
            }
        };
        #endregion
    }
}