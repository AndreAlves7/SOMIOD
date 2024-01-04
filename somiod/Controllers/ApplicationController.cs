using somiod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace somiod.Controllers
{
    public class ApplicationController : Controller
    {
        private static List<ApplicationModel> applications = new List<ApplicationModel>();
        private static int applicationIdCounter = 1;

        public ActionResult Index()
        {
            return View(applications);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ApplicationModel application)
        {
            application.Id = applicationIdCounter++;
            application.CreationDt = DateTime.Now;

            applications.Add(application);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var application = applications.Find(a => a.Id == id);

            if (application == null)
            {
                return HttpNotFound();
            }

            return View(application);
        }

        [HttpPost]
        public ActionResult Edit(ApplicationModel updatedApplication)
        {
            var existingApplication = applications.Find(a => a.Id == updatedApplication.Id);

            if (existingApplication == null)
            {
                return HttpNotFound();
            }

            // TODO Update properties of existingApplication based on updatedApplication

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var application = applications.Find(a => a.Id == id);

            if (application == null)
            {
                return HttpNotFound();
            }

            return View(application);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var application = applications.Find(a => a.Id == id);

            if (application == null)
            {
                return HttpNotFound();
            }

            return View(application);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var application = applications.Find(a => a.Id == id);

            if (application == null)
            {
                return HttpNotFound();
            }

            applications.Remove(application);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult GetContainerData(string applicationName, string containerName)
        {
            var application = applications.FirstOrDefault(a => a.Name == applicationName);
            var container = application?.Containers.FirstOrDefault(c => c.Name == containerName);

            if (container == null)
            {
                return HttpNotFound();
            }

            var dataRecords = container.Data;

            // Assuming you have a view named ContainerDataIndex for rendering data records
            return View("ContainerDataIndex", dataRecords);
        }

        [HttpGet]
        public ActionResult Discover()
        {
            if (Request.Headers["somiod-discover"] == "application")
            {
                var applicationNames = applications.Select(app => app.Name).ToList();
                return Json(applicationNames, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound();
        }
    }
}
