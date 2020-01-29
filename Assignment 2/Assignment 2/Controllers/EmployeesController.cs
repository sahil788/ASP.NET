using Assignment_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_2.Controllers
{

    public class EmployeesController : Controller
    {
        private Manager m = new Manager();
        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            var obj = m.EmployeeGetById(id.GetValueOrDefault());
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(obj);
            }
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View(new EmployeeAddViewModel());
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeAddViewModel newItem)
        {
            if(!ModelState.IsValid)
            {
                return View(newItem);
            }
            try
            {
                var addedItem = m.EmployeeAdd(newItem);                if (addedItem == null)
                {
                    return View(newItem);
                }
                else
                {
                    return RedirectToAction("Details", new { id = addedItem.EmployeeId });
                }
                // TODO: Add insert logic here
            }
            catch
            {
                return View(newItem);
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
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

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
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
