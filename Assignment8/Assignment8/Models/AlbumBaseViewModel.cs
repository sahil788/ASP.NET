using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Models
{
   
        public class AlbumBaseViewModel
        {
            [Key]
            public int Id { get; set; }
            public string Coordinator { get; set; }

            [Required, StringLength(50)]
            [Display(Name = "Album Name")]
            public string Name { get; set; }

            [Display(Name = "Release Date")]
            [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
            public DateTime ReleaseDate { get; set; }

            [Display(Name = "Album image (cover art)")]
            public string UrlAlbum { get; set; }

            [Display(Name = "Album's primary genre")]
            public string Genre { get; set; }

        }
        public class AlbumWithArtistAndTrackViewModel : AlbumBaseViewModel
        {
            public AlbumWithArtistAndTrackViewModel()
            {
                Artists = new List<Artist>();
                Tracks = new List<Track>();
            }

            [Display(Name = "Artist(s) on this album")]
            public IEnumerable<Artist> Artists { get; set; }

            [Display(Name = "Track(s) on this album")]
            public IEnumerable<Track> Tracks { get; set; }

            [Display(Name = "Number of tracks on this album")]
            public int TracksCount { get; set; }

            [Display(Name = "Number of artists on this album")]
            public int ArtistsCount { get; set; }
        }

        public class AlbumAddViewModel
        {
            public AlbumAddViewModel()
            {
                ReleaseDate = DateTime.Today;
                ArtistIds = new List<int>();
                TrackIds = new List<int>();
            }
            [Required, StringLength(50)]
            [Display(Name = "Album Name")]
            public string Name { get; set; }

            [Display(Name = "Release Date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime ReleaseDate { get; set; }

            [Display(Name = "Album's primary genre")]
            public string Genre { get; set; }
            [Display(Name = "Album image (cover art)")]
            public string UrlAlbum { get; set; }

            public string Coordinator { get; set; }

            [Required]
            public IEnumerable<int> ArtistIds { get; set; }

            public IEnumerable<int> TrackIds { get; set; }

        }

        public class AlbumAddFormViewModel : AlbumAddViewModel
        {
            public AlbumAddFormViewModel()
            {

            }
            [Display(Name = "Album's primary genre")]
            public SelectList GenreList { get; set; }

            [Display(Name = "All artists")]
            public MultiSelectList ArtistList { get; set; }

            [Display(Name = "All tracks")]
            public MultiSelectList TrackList { get; set; }
            public string ArtistName { get; set; }

        }

    }
