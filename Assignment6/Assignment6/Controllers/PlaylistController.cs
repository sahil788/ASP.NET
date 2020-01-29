using Assignment6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment6.Controllers
{
    public class PlaylistController : Controller
    {
        private Manager m = new Manager();
        // GET: Playlist
        public ActionResult Index()
        {
            return View(m.PlaylistGetAll());
        }

        // GET: Playlist/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.PlaylistGetByIdWithDetail(id.GetValueOrDefault());
            if(o==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
            
        }

        // GET: Playlist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Playlist/Create
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
        }

        // GET: Playlist/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.PlaylistGetByIdWithDetail(id.GetValueOrDefault());
            if(o==null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = m.mapper.Map<PlaylistEditTracksFormViewModel>(o);
                var selectedValues = o.Tracks.Select(jd => jd.TrackId);
                form.TrackList = new MultiSelectList(items: m.TrackGetAll(), dataValueField: "TrackId", dataTextField: "NameShort", selectedValues: selectedValues);
                List<TrackBaseViewModel> d = new List<TrackBaseViewModel>();
                foreach(var item in o.Tracks)
                {
                    d.Add(item);
                    
                }
                form.TracksOnPlaylist= d;
                return View(form);

            }
        }



        // POST: Playlist/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracksViewModel item)
        {
            
                if(!ModelState.IsValid)
                {
                    return RedirectToAction("edit", new { id = item.Id });
                }

                if(id.GetValueOrDefault()!= item.Id)
                {
                    return RedirectToAction("index");
                }

                var editedItem = m.PlaylistEditTracks(item);

                if(editedItem==null)
                {
                    return RedirectToAction("edit", new { id = item.Id });
                }
                else
                {
                return RedirectToAction("Details", new { id = item.Id });
                }
        }

        // GET: Playlist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Playlist/Delete/5
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
