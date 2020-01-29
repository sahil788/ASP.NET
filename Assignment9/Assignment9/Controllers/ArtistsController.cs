using Assignment9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class ArtistsController : Controller
    {
        private Manager m = new Manager();
        // GET: Artists
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.ArtistWithMediaItemsGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

        // GET: Artists/Create
        public ActionResult Create()
        {
            var form = new ArtistAddFormViewModel();
            form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
            return View(form);
        }

        // POST: Artists/Create
        [HttpPost]
        public ActionResult Create(ArtistAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                //return the object to view
                return View(newItem);
            }

            // Process the input
            var addedItem = m.ArtistAdd(newItem);

            if (addedItem == null)
            {
                //return the object to view
                var form = m.mapper.Map<ArtistAddViewModel, ArtistAddFormViewModel>(newItem);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(form);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }

        [Route("Artists/{id}/AlbumAdd")]
        public ActionResult AlbumAdd(int? id)
        {
            var o = m.ArtistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new AlbumAddFormViewModel();
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                form.ArtistName = o.Name;
                form.ArtistId = o.Id;
                form.ArtistPhoto = o.UrlArtist;
                return View(form);
            }
        }
        [HttpPost, ValidateInput(false)]
        [Route("Artists/{id}/AlbumAdd")]
        public ActionResult AlbumAdd(AlbumAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)   
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.AlbumAdd(newItem);

            if (addedItem == null)
            {
                //return the object to view
                var form = m.mapper.Map<AlbumAddViewModel, AlbumAddFormViewModel>(newItem);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(form);
            }
            else
            {
                return RedirectToAction("Details", "Albums", new { id = addedItem.Id });
            }
        }

       /*/ [Route("Artists/{id}/MediaItemAdd")]
        public ActionResult MediaItemAdd(int? id)
        {
            var o = m.ArtistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new MediaItemAddFormViewModel();
                form.ArtistName = o.Name;
                form.ArtistId = o.Id;
                form.ArtistPhoto = o.UrlArtist;
                return View(form);
            }
        }

        [HttpPost]
        [Route("Artists/{id}/MediaItemAdd")]
        public ActionResult MediaItemAdd(MediaItemAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)    //this should not be necessary anymore as validation of inputs are done in the view - unobtrosive validation
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.MediaItemAdd(newItem);

            if (addedItem == null)
            {
                //return the object to view
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", "Artists", new { id = addedItem.Id });
            }
        }*/
        // GET: Artists/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artists/Edit/5
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

        // GET: Artists/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artists/Delete/5
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
