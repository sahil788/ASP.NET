using Assignment9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class AlbumsController : Controller
    {
        private Manager m = new Manager();
        // GET: Albums
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.AlbumGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
               
                return View(o);
            }
        }

        // GET: Albums/Create
      /*  public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
        [Route("Albums/{id}/TrackAdd")]
        public ActionResult TrackAdd(int? id)
        {
            var o = m.AlbumGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new TrackAddFormViewModel();
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                form.AlbumName = o.Name;
                form.AlbumId = o.Id;
                form.AlbumCover = o.UrlAlbum;
                return View(form);
            }
        }

       //[Authorize(Roles = "Clerk")]
        // POST: Albums/id/TrackAdd
        [Route("Albums/{id}/TrackAdd")]
        [HttpPost]
        public ActionResult TrackAdd(TrackAddWithMediaViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.TrackAdd(newItem);

            if (addedItem == null)
            {
                //return the object to view
                var form = m.mapper.Map<TrackAddWithMediaViewModel, TrackAddFormViewModel>(newItem);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(form);
            }
            else
            {
                return RedirectToAction("Details", "Tracks", new { id = addedItem.Id });
            }
        }
        // GET: Albums/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Albums/Edit/5
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

        // GET: Albums/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Albums/Delete/5
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
