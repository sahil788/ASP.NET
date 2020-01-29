using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class LoadDataController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();


        public ActionResult Index()
        {
            return View();
        }

        // GET: LoadData /RoleClaims
        public ActionResult RoleClaim()
        {
            if (m.LoadData())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        // GET: LoadData /Genre
        public ActionResult Genre()
        {
            if (m.LoadDataGenre())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }


        // GET: LoadData /Artist
        public ActionResult Artist()
        {
            if (m.LoadDataArtist())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        // GET: LoadData /Album
        public ActionResult Album()
        {
            if (m.LoadDataAlbum())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        // GET: LoadData /Track
        public ActionResult Track()
        {
            if (m.LoadDataTrack())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }
    }
}