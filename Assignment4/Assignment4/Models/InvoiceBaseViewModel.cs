
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment4.Models
{
    public class InvoiceBaseViewModel
    {
        [Key]
        [DisplayName("Invoice number")]
        public int InvoiceId { get; set; }

        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }

        [DisplayName("Invoice date")]
        public DateTime InvoiceDate { get; set; }

        [DisplayName("Billing address")]
        [StringLength(70)]
        public string BillingAddress { get; set; }

        [DisplayName("Billing city")]
        [StringLength(40)]
        public string BillingCity { get; set; }

        [DisplayName("Billing state")]
        [StringLength(40)]
        public string BillingState { get; set; }

        [DisplayName("Billing country")]
        [StringLength(40)]
        public string BillingCountry { get; set; }

        [DisplayName("Postal code")]
        [StringLength(10)]
        public string BillingPostalCode { get; set; }

        [DisplayName("Invoice total")]
        public decimal Total { get; set; }


    }
}