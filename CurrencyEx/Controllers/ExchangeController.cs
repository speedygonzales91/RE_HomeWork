using CurrencyEx.Exceptions;
using CurrencyEx.Models;
using CurrencyEx.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyEx.Controllers
{
    public class ExchangeController : Controller
    {
        // GET: Exchange
        public ActionResult Index()
        {
            var rates = new RateCollectionModel();

            var vm = new ExchangeViewModel()
            {
                Currencies = rates.GetLatestCurrencyRate().Select(x => x.Currency).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public string Index(ExchangeViewModel viewModel)
        {
            var rates = new RateCollectionModel();

            if (rates.GetLatestRate(viewModel.ToCurrency) == null)
                throw new NoInputEnteredException(viewModel.ToCurrency);


            if (rates.GetLatestRate(viewModel.FromCurrency) == null)
                throw new NoInputEnteredException(viewModel.FromCurrency);

            viewModel.Result = rates.GetLatestRate(viewModel.ToCurrency) /
                rates.GetLatestRate(viewModel.FromCurrency) *
                viewModel.FromAmount;

            return String.Format("{0} {1} is {2:0.00} {3}", viewModel.FromAmount, viewModel.FromCurrency, viewModel.Result, viewModel.ToCurrency);   
        }
    }
}