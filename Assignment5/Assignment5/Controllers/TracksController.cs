using Assignment5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();
        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                var obj = m.TrackGetById(id.GetValueOrDefault());
                if (obj == null)
                    return HttpNotFound();
                else
                    return View(obj);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            var form = new TrackAddFormViewModel();
            form.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");
            form.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");
            return View(form);
        }

        // POST: Tracks/Create
        [HttpPost]
        public ActionResult Create(TrackAddViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            try
            {
                
                 var addedItem = m.TrackAdd(obj);
                    
                    if(addedItem == null)
                    {
                        return View(obj);
                    }
                    else
                    {
                        return RedirectToAction("Details", new { id = addedItem.TrackId });
                    }
                
                
              
                // TODO: Add insert logic here

                
            }
            catch
            {
                return View(obj);
            }
        }

        // GET: Tracks/Edit/5
      /*  public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tracks/Edit/5
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

        // GET: Tracks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tracks/Delete/5
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
        }*/
    }
}
