using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyEx.ViewModel
{
    public class ExchangeViewModel
    {
        public List<string> Currencies { get; set; }

        [Display(Name ="From")]
        [Required(ErrorMessage = "Please select Currency From")]
        public string FromCurrency { get; set; }

        [Required(ErrorMessage = "Enter Amount From")]
        public double FromAmount { get; set; }

        [Display(Name = "To")]
        [Required(ErrorMessage = "Please select Currency To")]
        public string ToCurrency { get; set; }

        public double? Result { get; set; }
    }
}