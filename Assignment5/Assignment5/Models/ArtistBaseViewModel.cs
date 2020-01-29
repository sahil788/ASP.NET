using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Models
{
    public class ArtistBaseViewModel
    {
        [Key]
        [DisplayName("Artist Id")]
        public int ArtistId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }
    }
}