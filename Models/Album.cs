using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C.R.E.A.M.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        
        [Required(ErrorMessage ="Title Is Not Valid")]
        public string Title { get; set; }

        [Range(1, 100, ErrorMessage ="Nope")]
        public decimal Price { get; set; }

        public string Description { get; set; } = "Good Album";
        public string AlbumUrl { get; set; }

        [Range(1950, 2021)]
        public int ReleaseYear { get; set; }

        public Artist Artist { get; set; }
        public int ArtistId { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }

        public void UpdateAlbum(Album newAlbum)
        {
            Title = newAlbum.Title;
            Price = newAlbum.Price;
            ReleaseYear = newAlbum.ReleaseYear;
            ArtistId = newAlbum.ArtistId;
            Description = newAlbum.Description;
            GenreId = newAlbum.GenreId;
            AlbumUrl = newAlbum.AlbumUrl;
        }
    }
}