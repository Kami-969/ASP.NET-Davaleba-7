using C.R.E.A.M.Context;
using C.R.E.A.M.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace C.R.E.A.M
{
    public class FakeDatabase
    {
        private List<Album> _albums;
        private List<Genre> _genres;
        private List<Artist> _artists;

        public List<Album> Albums
        {
            get { return _albums; }
        }

        public List<Genre> Genres
        {
            get { return _genres; }
        }

        public List<Artist> artist
        {
            get { return _artists; }
        }

        public FakeDatabase()
        {
            initializeGenres();
            initializeArtists();
            initializeAlbums();
        }

        private void initializeArtists()
        {

            using (var context = new StoreContext())
            {
                context.Artists.AddOrUpdate(new[]
                {
                  new Artist
                  {

                   Name = "Pink Floyd", BasicInfo = "Eng", ImageUrl = "" 

                  }
                    });

                context.Artists.AddOrUpdate(new[]
                 {
                  new Artist
                  {
                      Name = "Daft Punk", BasicInfo = "Frn", ImageUrl = ""
                  }
                    });

                context.Artists.AddOrUpdate(new[]
                {
                  new Artist
                  {
                      Name = "KayaKata", BasicInfo = "Geo", ImageUrl = ""
                  }
                    });

                context.Artists.AddOrUpdate(new[]
                {
                  new Artist
                  {
                      Name = "Biggie", BasicInfo = "Usa", ImageUrl = ""
                  }
                    });

                context.Artists.AddOrUpdate(new[]
                {
                  new Artist
                  {
                      Name = "Lady Gaga", BasicInfo = "Eng", ImageUrl = ""
                  }
                    });

                context.Artists.AddOrUpdate(new[]
                {
                  new Artist
                  {
                     Name = "Skillex", BasicInfo = "Eng", ImageUrl = ""
                  }
                    });




                context.SaveChanges();
            }
        }

        private void initializeGenres()
        {

            using (var context = new StoreContext())
            {
                context.Genres.AddOrUpdate(new[]
                {
                  new Genre
                  {

                     Name = "Progresive Rock"

                  }
                    });

                context.Genres.AddOrUpdate(new[]
                 {
                  new Genre
                  {
                     Name = "Electronic"
                  }
                    });

                context.Genres.AddOrUpdate(new[]
                {
                  new Genre
                  {
                      Name = "Mumble"
                  }
                    });

                context.Genres.AddOrUpdate(new[]
                {
                  new Genre
                  {
                      Name = "Hip-Hop"
                  }
                    });

                context.Genres.AddOrUpdate(new[]
                {
                  new Genre
                  {
                     Name = "Pop Rock"
                  }
                    });

                context.SaveChanges();
            }
        }

       private void initializeAlbums()
        {

            using (var context = new StoreContext())
            {
                context.Albums.AddOrUpdate(new[]
                {
                  new Album
                  {
                     Title = "Dark Side Of The Moon",
                     Price = 20.99M,
                     Genre = context.Genres.Where(x => x.Name == "Progresive Rock").FirstOrDefault(),
                     Artist = context.Artists.Where(x => x.Name == "Pink Floyd").FirstOrDefault(),
                     AlbumUrl = "/App_Files/Images/1.jpg",
                     ReleaseYear = 1973

                  }
                    });

                context.Albums.AddOrUpdate(new[]
                {
                  new Album
                  {
                    Title = "Random Access Memmory",
                    Price = 15.99M,
                    Genre = context.Genres.Where(x => x.Name == "Electronic").FirstOrDefault(),
                    Artist = context.Artists.Where(x => x.Name == "Daft Punk").FirstOrDefault(),
                    AlbumUrl = "/App_Files/Images/2.jpg",
                    ReleaseYear = 2015     
                  }
                    });

                context.Albums.AddOrUpdate(new[]
                {
                  new Album
                  {
                    Title = "recess",
                    Price = 20.99M,
                    Genre = context.Genres.Where(x => x.Name == "Electronic").FirstOrDefault(),
                    Artist = context.Artists.Where(x => x.Name == "Skillex").FirstOrDefault(),
                    AlbumUrl = "/App_Files/Images/3.jpg",
                    ReleaseYear = 2015
                  }
                    });

                context.SaveChanges();
            }

        }

        public void UpdateAlbum(Album updateAlbum)
        {
            updateAlbum.Genre = Genres.Where(x => x.GenreId == updateAlbum.GenreId).FirstOrDefault();
            updateAlbum.Artist = artist.Where(x => x.ArtistId == updateAlbum.ArtistId).FirstOrDefault();

            _albums[updateAlbum.AlbumId - 1] = updateAlbum;
        }
        public void CreateAlbum(Album newAlbum)
        {
            newAlbum.Genre = Genres.Where(x => x.GenreId == newAlbum.GenreId).FirstOrDefault();
            newAlbum.Artist = artist.Where(x => x.ArtistId == newAlbum.ArtistId).FirstOrDefault();

            newAlbum.AlbumId = _albums.Max(x => x.AlbumId) + 1;
            _albums.Add(newAlbum);
        }

    }
}