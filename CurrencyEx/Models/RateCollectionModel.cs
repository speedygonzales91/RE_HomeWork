using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CurrencyEx.Models
{
    public class RateCollectionModel
    {
        Dictionary<DateTime, List<CurrencyRate>> Rates { get; } = new Dictionary<DateTime, List<CurrencyRate>>();
            
        public RateCollectionModel()
        {
            var doc = XDocument.Load("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml");
            var cubes = doc.Elements().Elements().Where(c => c.Name.LocalName == "Cube");

            foreach (var cube in cubes.Elements()) 
            {
                var date =Convert.ToDateTime(cube.Attribute("time").Value);
                var currenciesRates = new List<CurrencyRate>();
                foreach (var c in cube.Elements())
                {
                    currenciesRates.Add(new CurrencyRate() { Currency = c.Attribute("currency").Value, Rate = double.Parse(c.Attribute("rate").Value, CultureInfo.InvariantCulture) });
                }
                this.Rates.Add(date, currenciesRates);
            }
        }

        public List<CurrencyRate> GetLatestCurrencyRate()
        {
            //Legutolsó nap kiszedése
            var latestDate = Rates.Keys.Max();
            return Rates[latestDate];
        }

        public double? GetLatestRate(string currency)
        {
            return GetLatestCurrencyRate().Single(x => x.Currency==currency).Rate;
        }

        public Dictionary<DateTime, double?> GetRatesForCurrency(string currency)
        {
            Dictionary<DateTime, double?> currencyHistory = new Dictionary<DateTime, double?>();

            foreach (var item in Rates)
            {
                currencyHistory.Add(item.Key, item.Value.Single(x => x.Currency == currency).Rate);
            }

            return currencyHistory;
        }
    }
}