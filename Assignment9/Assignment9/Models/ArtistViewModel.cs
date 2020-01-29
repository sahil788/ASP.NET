using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Models
{
    public class ArtistBaseViewModel
    {
        public ArtistBaseViewModel()
        {
            BirthOrStartDate = DateTime.Now.AddYears(-20);
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth date or start date")]
        public DateTime BirthOrStartDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Artist photo")]
        public string UrlArtist { get; set; }

        [Required]
        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Artist profile")]
        public string Portrayal { get; set; }

    }

    public class ArtistWithDetailsViewModel : ArtistBaseViewModel
    {
        public ArtistWithDetailsViewModel()
        {
            Albums = new List<AlbumBaseViewModel>();
            MediaItems = new List<MediaItemBaseViewModel>();
        }
        public IEnumerable<AlbumBaseViewModel> Albums { get; set; }

        [Display(Name = "Albums Count")]
        public int AlbumsCount { get; set; }

        public IEnumerable<MediaItemBaseViewModel> MediaItems { get; set; }
    }

    public class ArtistAddViewModel
    {
        public ArtistAddViewModel()
        {
            BirthOrStartDate = DateTime.Now.AddYears(-20);
        }

        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth date or start date")]
        public DateTime BirthOrStartDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "URL to artist photo")]
        public string UrlArtist { get; set; }

        [Required]
        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Artist profile")]
        public string Portrayal { get; set; }

    }

    public class ArtistAddFormViewModel : ArtistAddViewModel
    {
        [Display(Name = "Artist's primary genre")]
        public SelectList GenreList { get; set; }
    }

    public class ArtistEditFormViewModel : ArtistAddFormViewModel
    {
        [Key]
        public int Id { get; set; }

    }
}