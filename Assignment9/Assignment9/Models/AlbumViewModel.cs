using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Models
{
    public class AlbumBaseViewModel
    {
        public AlbumBaseViewModel()
        {
            ReleaseDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Album Name")]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album cover art")]
        public string UrlAlbum { get; set; }

        [Required]
        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }

        [Required, StringLength(200)]
        public string Coordinator { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Album description")]
        public string Depiction { get; set; }
    }

    public class AlbumWithDetailsViewModel : AlbumBaseViewModel
    {
        public AlbumWithDetailsViewModel()
        {
            Tracks = new List<TrackBaseViewModel>();
            Artists = new List<ArtistBaseViewModel>();
        }

        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
        public IEnumerable<ArtistBaseViewModel> Artists { get; set; }

        [Display(Name = "Tracks Count")]
        public int TracksCount { get; set; }

    }

    public class AlbumAddViewModel
    {
        public AlbumAddViewModel()
        {
            ReleaseDate = DateTime.Now;
        }
        [Required, StringLength(100)]
        [Display(Name = "Album Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album cover art")]
        public string UrlAlbum { get; set; }

        [Required]
        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }

        public string Coordinator { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Album description")]
        public string Depiction { get; set; }

        //For Display purposes
        public int ArtistId { get; set; }

        public string ArtistName { get; set; }

        [Display(Name = "Artist photo")]
        public string ArtistPhoto { get; set; }
    }

    public class AlbumAddFormViewModel : AlbumAddViewModel
    {
        [Display(Name = "Album's primary genre")]
        public SelectList GenreList { get; set; }

    }

    public class AlbumEditFormViewModel : AlbumAddFormViewModel
    {
        [Key]
        public int Id { get; set; }

    }
}