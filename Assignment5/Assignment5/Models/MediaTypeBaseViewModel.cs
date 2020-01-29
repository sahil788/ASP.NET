using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Models
{
    public class MediaTypeBaseViewModel
    {
        [Key]
        [DisplayName("MediaType Id")]
        public int MediaTypeId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }
    }
}