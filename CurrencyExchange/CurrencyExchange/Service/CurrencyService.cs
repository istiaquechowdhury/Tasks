using System.Text.Json;


namespace CurrencyExchange.Service
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.frankfurter.app";

        public CurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> GetCurrenciesAsync()
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/currencies");
            if (!response.IsSuccessStatusCode) return new List<string>();

            var json = await response.Content.ReadAsStringAsync();
            var currencies = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            return currencies != null ? new List<string>(currencies.Keys) : new List<string>();
        }

        public async Task<decimal?> ConvertCurrencyAsync(decimal amount, string fromCurrency, string toCurrency)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/latest?amount={amount}&from={fromCurrency}&to={toCurrency}");

            if (!response.IsSuccessStatusCode)
                return null; // API request failed

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
                return null; // Empty response

            try
            {
                var result = JsonSerializer.Deserialize<CurrencyConversionResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result?.Rates == null || !result.Rates.ContainsKey(toCurrency))
                    return null; // Ensure Rates dictionary exists and contains the target currency

                return result.Rates[toCurrency];
            }
            catch (JsonException)
            {
                return null; // JSON parsing failed
            }
        }

        private class CurrencyConversionResponse
        {
            public Dictionary<string, decimal> Rates { get; set; } = new();
        }
    }
}
