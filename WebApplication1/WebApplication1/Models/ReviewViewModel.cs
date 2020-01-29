using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ReviewViewModel
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required, StringLength(500)]
        public string Description { get; set; }
    }
}