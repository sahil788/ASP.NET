using Assignment8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {
        Manager m = new Manager();
        // GET: Artist
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.ArtistGetByIdWithAlbum(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }

        // GET: Artist/Create

        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            var form = new ArtistAddFormViewModel();
            form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
            return View(form);
        }

        // POST: Artist/Create
       [Authorize(Roles = "Executive")]
        [HttpPost]
        public ActionResult Create(ArtistAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.ArtistAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedItem.Id });
            }
        }

      [Authorize(Roles = "Coordinator")]
      [Route("artist/{id}/addalbum")]
        public ActionResult AddAlbum(int? id)
        {

            var o = m.ArtistGetByIdWithAlbum(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new AlbumAddFormViewModel();
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                form.ArtistName = o.Name;
                var selectedValues = new List<int> { o.Id };
                form.ArtistList = new MultiSelectList(m.ArtistGetAll(), "Id", "Name", selectedValues);
                form.TrackList = new MultiSelectList(m.TrackGetAll(), "Id", "Name");
                return View(form);

            }
        }

        [Authorize(Roles = "Coordinator")]
        [Route("artist/{id}/addalbum")]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.AlbumAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", "Album", new { id = addedItem.Id });
            }
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Artist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
