using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
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

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            var obj = m.EmployeeGetById(id.GetValueOrDefault());

            if(obj==null)
            {
                return HttpNotFound();
            }

            else
            {
                var formObj = m.mapper.Map<EmployeeBaseViewModel, EmployeeEditContactFormViewModel>(obj);
                return View(formObj);
            }
            
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, EmployeeEditContactViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Edit", new { id = model.EmployeeId });
                }

                if(id.GetValueOrDefault()!= model.EmployeeId)
                {
                    return RedirectToAction("Index");
                }

                var editedItem = m.EmployeeEditContactInfo(model);

                if(editedItem == null)
                {
                    return RedirectToAction("Edit", new { id = model.EmployeeId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
                // TODO: Add update logic here

                
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
