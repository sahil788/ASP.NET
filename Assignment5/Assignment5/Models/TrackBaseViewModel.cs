using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Models
{
    public class TrackBaseViewModel 
    {
        [Key]
        [DisplayName("Track Id")]
        public int TrackId { get; set; }
        [Required]
        [StringLength(200)]
        [DisplayName("Track Name")]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [DisplayName("Length (ms)")]
        public int Milliseconds { get; set; }

        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }




    }
}