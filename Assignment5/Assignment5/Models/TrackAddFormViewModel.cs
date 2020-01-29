using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class TrackAddFormViewModel
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
        [Range(0, Int32.MaxValue)]
        public int Milliseconds { get; set; }

        [DisplayName("Unit Price")]
        [Range(0, Int32.MaxValue)]
        public decimal UnitPrice { get; set; }

        [DisplayName("Album List")]
        public SelectList AlbumList { get; set; }

        [DisplayName("MediaType List")]
        public SelectList MediaTypeList { get; set; }
    }
}