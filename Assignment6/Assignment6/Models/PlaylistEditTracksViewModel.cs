using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment6.Models
{
    public class PlaylistEditTracksViewModel
    {
        public PlaylistEditTracksViewModel()
        {
            TracksIds = new List<int>();
        }

        [Key]
        [DisplayName("Playlist Id")]
        public int Id { get; set; }

        public IEnumerable<int> TracksIds { get; set; }
    }
}