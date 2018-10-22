using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyEx.Exceptions
{
    public class NoInputEnteredException : ApplicationException
    {
        public string Currency { get; set; }

        public NoInputEnteredException(string currency) : base($"This currency, {currency} is not valid")
        {
            Currency = currency;
        }
    }
}