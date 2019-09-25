using System;
using System.ComponentModel.DataAnnotations;
// Schema allows us to add Display names
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }

// Without Display(Name = "foo") this would show the public column heading (ReleaseDate)
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }

// [Column(TypeName = "decimal(18, 2)")] makes input map to currency
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
