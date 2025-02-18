using CurrencyExchange.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Controllers
{
    public class CurrencyController : Controller
    {


        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Currencies = await _currencyService.GetCurrenciesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            var convertedAmount = await _currencyService.ConvertCurrencyAsync(amount, fromCurrency, toCurrency);
            if (convertedAmount == null)
            {
                ViewBag.Error = "Failed to retrieve conversion rate.";
            }
            else
            {
                ViewBag.ConvertedAmount = convertedAmount;
            }

            ViewBag.Currencies = await _currencyService.GetCurrenciesAsync();
            return View("Index");
        }

    }
}
