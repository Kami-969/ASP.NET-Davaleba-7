using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Net;
using C.R.E.A.M.Models;
using Microsoft.Ajax.Utilities;
using C.R.E.A.M.Context;
using System.Data.Entity;

namespace C.R.E.A.M.Controllers
{
    public class StoreManageController : Controller
    {
        
        StoreContext _storeContext = new StoreContext();
        // GET: StoreManage
        public ActionResult Index()
        {
            var albums = _storeContext.Albums;

            return View(albums);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var album = _storeContext.Albums.Where(x => x.AlbumId == id).FirstOrDefault();

            if (album == null)
            {
                return HttpNotFound();
            }

            ViewBag.ArtistId =
               new SelectList(_storeContext.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId =
                new SelectList(_storeContext.Genres, "GenreId", "Name", album.GenreId);

            return View(album);
        }

        [HttpPost]
        public ActionResult Edit(Album updateAlbum)
        {
            if(ModelState.IsValid)
            {
                //var oldAlbum = _storeContext.Albums.Where(x=>x.AlbumId == updateAlbum.AlbumId).FirstOrDefault();

                //oldAlbum.UpdateAlbum(updateAlbum);

                _storeContext.Entry(updateAlbum).State = EntityState.Modified;

                _storeContext.SaveChanges();

                return RedirectToAction("index");
            }
            else
            {
                //List<KeyValuePair<string, ModelState>> errors = ModelState.Where(x => x.Value.Errors.Count > 0).ToList();
                //ViewBag.ErrorList = errors;

                ViewBag.ArtistId =
              new SelectList(_storeContext.Artists, "ArtistId", "Name", updateAlbum.ArtistId);
                ViewBag.GenreId =
                    new SelectList(_storeContext.Genres, "GenreId", "Name", updateAlbum.GenreId);

                return View();
            }


        }

        public ActionResult CreatAlbum()
        {
            var album = _storeContext.Albums.Where(x => x.AlbumId == 1).FirstOrDefault();

            ViewBag.ArtistId =
              new SelectList(_storeContext.Artists, "ArtistId", "Name", 1);

            ViewBag.GenreId =
                new SelectList(_storeContext.Genres, "GenreId", "Name", 1);

            return View(album);

        }


        [HttpPost]
        public ActionResult CreatAlbum(Album newalbum)
        {
            if(!ModelState.IsValid)
            {
                return CreatAlbum();
            }

            _storeContext.Albums.Add(newalbum);
            _storeContext.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult Search(string parameter)
        {
            var albums = _storeContext.Albums.Where(x => x.Title.ToLower().Contains(parameter.ToLower()));

            return View("index", albums);
        }

    }
}