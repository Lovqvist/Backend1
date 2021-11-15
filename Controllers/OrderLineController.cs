using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend1_2.Controllers
{
    public class OrderLineController : Controller
    {
        // GET: OrderLineController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderLineController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderLineController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderLineController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderLineController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderLineController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderLineController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderLineController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
