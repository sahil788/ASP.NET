using Assignment1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    
    public class PhonesController : Controller
    {
        private List<Phone> Phones;

        public PhonesController()
        {
            Phones = new List<Phone>();
            Phone iphone = new Phone();

            iphone.id = 1;
            iphone.PhoneName = "iphone X";
            iphone.Manufacturer = "Apple";
            iphone.DateReleased = new DateTime(2017, 11, 3);
            iphone.MSRP = 1300;
            iphone.ScreenSize = 5.8;
            Phones.Add(iphone);

            var galaxy = new Phone
            {
                id = 2,
                PhoneName = "Galaxy Note 8",
                Manufacturer = "Samsung",
                DateReleased = new DateTime(2017, 8, 1),
                MSRP = 749,
                ScreenSize = 5.7
            };
            Phones.Add(galaxy);

            Phones.Add(new Phone
            {
                id = 3,
                PhoneName = "Pixel 2",
                Manufacturer = "Google",
                DateReleased = new DateTime(2017, 10, 19),
                MSRP = 900,
                ScreenSize = 6
            });
        }
        // GET: Phones
        public ActionResult Index()
        {
            return View(Phones);
        }

        // GET: Phones/Details/5
        public ActionResult Details(int id)
        {
            int index = id - 1;
            Phone phone = Phones[index];
         
            return View(phone);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            return View(new Phone());
        }

        // POST: Phones/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var newPhone = new Phone();
                newPhone.id = Phones.Count;
                newPhone.PhoneName = collection["PhoneName"];
                newPhone.Manufacturer = collection["Manufacturer"];
                newPhone.DateReleased = Convert.ToDateTime(collection["DateReleased"]);

                int msrp;
                double ss;
                if (int.TryParse(collection["MSRP"], out msrp))
                    newPhone.MSRP = msrp;
                if (double.TryParse(collection["ScreenSize"], out ss))
                    newPhone.ScreenSize = ss;
                Phones.Add(newPhone);                return View("Details", newPhone);

                // return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //    // GET: Phones/Edit/5
        //    public ActionResult Edit(int id)
        //    {
        //        return View();
        //    }

        //    // POST: Phones/Edit/5
        //    [HttpPost]
        //    public ActionResult Edit(int id, FormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add update logic here

        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: Phones/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        return View();
        //    }

        //    // POST: Phones/Delete/5
        //    [HttpPost]
        //    public ActionResult Delete(int id, FormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add delete logic here

        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        
    }
}
