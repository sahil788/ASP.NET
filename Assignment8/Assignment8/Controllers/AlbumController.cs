using Assignment8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    [Authorize]
    public class AlbumController : Controller
    {
        Manager m = new Manager();

        // GET: Album
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Album/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.AlbumGetByIdWithDetails(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }

        }

        // GET: Album/Create
        [Authorize(Roles = "Clerk")]
        [Route("album/{id}/addtrack")]
        public ActionResult AddTrack(int? id)
        {
            var a = m.AlbumGetByIdWithDetails(id.GetValueOrDefault());
            if (a == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new TrackAddFormViewModel();
                form.AlbumName = a.Name;
                form.AlbumId = a.Id;
                form.UrlAlbumCover = a.UrlAlbum;
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(form);
            }
        }

        // POST: Album/Create
        [Authorize(Roles = "Clerk")]
        [Route("album/{id}/addtrack")]
        [HttpPost]
        public ActionResult AddTrack(TrackAddViewModel newItem,int id)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.TrackAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", "Track", new { id = addedItem.Id });
            }
        }

        // GET: Album/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Album/Edit/5
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

        // GET: Album/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Album/Delete/5
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
