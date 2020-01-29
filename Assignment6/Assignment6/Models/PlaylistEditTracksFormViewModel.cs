﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment6.Models
{
    public class PlaylistEditTracksFormViewModel
    {
        [Key]
        [DisplayName("Playlist Id")]
        public int PlaylistId { get; set; }

        [DisplayName("Playlist Name")]
        [Required, StringLength(120)]
        public string Name { get; set; }
        public int TracksCount { get; set; }

        [Display(Name = "Tracks")]
        public MultiSelectList  TrackList { get; set; }

        public IEnumerable<TrackBaseViewModel> TracksOnPlaylist { get; set; }
    }
}