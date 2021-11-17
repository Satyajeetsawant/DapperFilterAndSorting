using EmployeeDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDapper.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employee
        private EmployeesRepository repository;

        public EmployeesController()
        {
            repository = new EmployeesRepository();
        }

        // GET: Employee
        public ActionResult Index(RequestModel request)
        {
            if (request.OrderBy == null)
            {
                request = new RequestModel
                {
                    Search = request.Search,
                    OrderBy = "name",
                    IsDescending = false
                };
            }
            ViewBag.Request = request;
            return View(repository.GetAll(request));
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View(repository.Get(id));
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee, bool editAfterSaving = false)
        {
            if (ModelState.IsValid)
            {
                var lastInsertedId = repository.Create(employee);
                if (lastInsertedId > 0)
                {
                    TempData["Message"] = "Record added successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to add record";
                }
                if (editAfterSaving)
                {
                    return RedirectToAction("Edit", new { Id = lastInsertedId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int Id)
        {
            return View(repository.Get(Id));
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var recordAffected = repository.Update(employee);
                if (recordAffected > 0)
                {
                    TempData["Message"] = "Record updated successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to update record";
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repository.Get(id));
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(Employee employee)
        {
            var recordAffected = repository.Delete(employee.Id);
            if (recordAffected > 0)
            {
                TempData["Message"] = "Record deleted successfully";
            }
            else
            {
                TempData["Error"] = "Failed to delete record";
            }
            return RedirectToAction("Index");
        }
    }
}