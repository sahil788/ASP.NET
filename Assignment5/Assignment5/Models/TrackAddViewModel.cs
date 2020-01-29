using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Models
{
    public class TrackAddViewModel 
    {
        public TrackAddViewModel()
        {
            Milliseconds = 0;
            UnitPrice = 0.00m;
        }
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

        [Range(1, Int32.MaxValue)]
        [DisplayName("Album Id")]
        public int AlbumId { get; set; }

        [Range(1, Int32.MaxValue)]
        [DisplayName("MediaType Id")]
        public int MediaTypeId { get; set; }

    }
}