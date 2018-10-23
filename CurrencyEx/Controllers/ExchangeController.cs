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
        public ActionResult Index(ExchangeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var rates = new RateCollectionModel();

                if (rates.GetLatestRate(viewModel.ToCurrency) == null)
                    throw new NoInputEnteredException(viewModel.ToCurrency);


                if (rates.GetLatestRate(viewModel.FromCurrency) == null)
                    throw new NoInputEnteredException(viewModel.FromCurrency);

                viewModel.Result = rates.GetLatestRate(viewModel.ToCurrency) /
                    rates.GetLatestRate(viewModel.FromCurrency) *
                    viewModel.FromAmount;

                var content = String.Format("{0} {1} is {2:0.00} {3}", viewModel.FromAmount, viewModel.FromCurrency, viewModel.Result, viewModel.ToCurrency);

                return Content(content);
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                         .SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage));
                return Content(message);
            }
        }
    }
}