using System.Text.Json.Serialization;

namespace CurrencyExchange.Models
{
    public class CurrencyConversionResponse
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("base")]
        public string BaseCurrency { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
