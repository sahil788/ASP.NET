using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Assignment6.Models
{
    public class PlaylistWithTracksViewModel :PlaylistBaseViewModel
    {
        [DisplayName("Tracks in Playlist")]
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}