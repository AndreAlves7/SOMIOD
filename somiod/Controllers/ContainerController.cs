using somiod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace somiod.Controllers
{
    public class ContainerController : Controller
    {
        private static List<ContainerModel> containers = new List<ContainerModel>();
        private static int containerIdCounter = 1;

        public ActionResult Index()
        {
            return View(containers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ContainerModel container)
        {
            container.Id = containerIdCounter++;
            container.CreationDt = DateTime.Now;

            containers.Add(container);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var container = containers.Find(c => c.Id == id);

            if (container == null)
            {
                return HttpNotFound();
            }

            return View(container);
        }

        [HttpPost]
        public ActionResult Edit(ContainerModel updatedContainer)
        {
            var existingContainer = containers.Find(c => c.Id == updatedContainer.Id);

            if (existingContainer == null)
            {
                return HttpNotFound();
            }

            // Update properties of existingContainer based on updatedContainer

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var container = containers.Find(c => c.Id == id);

            if (container == null)
            {
                return HttpNotFound();
            }

            return View(container);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var container = containers.Find(c => c.Id == id);

            if (container == null)
            {
                return HttpNotFound();
            }

            return View(container);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var container = containers.Find(c => c.Id == id);

            if (container == null)
            {
                return HttpNotFound();
            }

            containers.Remove(container);

            return RedirectToAction("Index");
        }
    }
}