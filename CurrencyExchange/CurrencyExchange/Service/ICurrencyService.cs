using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Service
{
    public interface ICurrencyService
    {
        Task<List<string>> GetCurrenciesAsync();
        Task<decimal?> ConvertCurrencyAsync(decimal amount, string fromCurrency, string toCurrency);
    }
}
