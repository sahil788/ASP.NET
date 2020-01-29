using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace Assignment5.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        

        [DisplayName("Media Type")]
        public MediaTypeBaseViewModel MediaType { get; set; }

        [DisplayName("Album title")]
        public String AlbumTitle { get; set; }

        [DisplayName("Artist name")]
        public String AlbumArtistName { get; set; }
    }
}