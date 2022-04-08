using EducationCenter_cw2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationCenter_cw2.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            var repository = new StudentRepository();
            var students = repository.GetAll();
            return View(students);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Create
        public ActionResult Create(student emp)
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var repository = new StudentRepository();
            try
            {
                repository.Insert(emp);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            var repository = new StudentRepository();
            var emp = repository.GetById(id);
            var supervisors = repository.GetAll();
            ViewBag.SupervisorsList = supervisors;
            return View(emp);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(Student emp)
        {
            var repository = new StudentRepository();
            try
            {
                repository.Update(emp);

                return RedirectToAction("Index");
            }
            catch(Exeption ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var repo = new StudentRepository();
                repo.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}
