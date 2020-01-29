using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
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
            if (id != null)
                return View(m.EmployeeGetById(id.GetValueOrDefault()));
            else return HttpNotFound();
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View(new EmployeeAddViewModel());
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeAddViewModel e)
        {
            try
            {
                if(e!= null)
                {
                    var obj = m.EmployeeAdd(e);
                    if (obj != null)
                    {
                        return RedirectToAction("Details/" + obj.EmployeeId);
                    }
                }
                else
                {
                    return HttpNotFound();
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
