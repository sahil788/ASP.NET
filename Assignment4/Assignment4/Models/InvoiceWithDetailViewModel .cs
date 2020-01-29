using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment4.Models
{
    public class InvoiceWithDetailViewModel:InvoiceBaseViewModel
    {
        [Required]
        [StringLength(40)]
        public string CustomerFirstName { get; set; }

        [Required]
        [StringLength(40)]
        public string CustomerLastName { get; set; }

        [StringLength(70)]
        public string CustomerAddress { get; set; }

        [StringLength(40)]
        public string CustomerState { get; set; }

        [Required]
        [StringLength(40)]
        public string CustomerEmployeeFirstName { get; set; }

        [Required]
        [StringLength(40)]
        public string CustomerEmployeeLastName { get; set; }

        public IEnumerable<InvoiceLineWithDetailViewModel> InvoiceLines { get; set; }



    }
}