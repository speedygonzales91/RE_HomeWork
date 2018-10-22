using CurrencyEx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyEx.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        public ActionResult Index()
        {
            var rates = new RateCollectionModel();
            var listOfCurrencies = rates.GetLatestCurrencyRate().Select(x => x.Currency);
            return View(listOfCurrencies);
        }


        public ActionResult CurrencyHistory(string currency)
        {
            var rates = new RateCollectionModel();
            var currHistory = rates.GetRatesForCurrency(currency).Reverse();

            Dictionary<string, double?> currencyModifiedDictionary = new Dictionary<string, double?>();

            foreach (var item in currHistory)
            {
                currencyModifiedDictionary.Add((item.Key - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString(), item.Value);
            }

            /*
             * By default, the ASP.NET MVC framework does not allow you to respond to an HTTP GET request with a JSON payload. If you need to send JSON in response to a GET, you'll need to explicitly allow the behavior by using JsonRequestBehavior.AllowGet as the second parameter to the Json method. However, there is a chance a malicious user can gain access to the JSON payload through a process known as JSON Hijacking. You do not want to return sensitive information using JSON in a GET request. For more details, see Phil's post at http://haacked.com/archive/2009/06/24/json-hijacking.aspx/ or this SO post.
             */
            return Json(currencyModifiedDictionary, JsonRequestBehavior.AllowGet);
        }
    }
}