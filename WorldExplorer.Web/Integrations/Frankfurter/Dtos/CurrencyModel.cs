namespace WorldExplorer.Web.Integrations.Frankfurter.Dtos
{
    public class CurrencyModel
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public override string ToString() => $"{Code} - {Name}";
    }

    public class RateDataPoint
    {
        public DateOnly Date { get; set; }
        public decimal Rate { get; set; }
    }

    public class CurrencyPair
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
    }
}
