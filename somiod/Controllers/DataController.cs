using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace somiod.Controllers
{
    public class DataController : Controller
    {
        private static List<DataModel> dataRecords = new List<DataModel>();
        private static int dataIdCounter = 1;

        public ActionResult Index()
        {
            return View(dataRecords);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DataModel data)
        {
            data.Id = dataIdCounter++;
            data.CreationDt = DateTime.Now;

            dataRecords.Add(data);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var data = dataRecords.Find(d => d.Id == id);

            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var data = dataRecords.Find(d => d.Id == id);

            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var data = dataRecords.Find(d => d.Id == id);

            if (data == null)
            {
                return HttpNotFound();
            }

            dataRecords.Remove(data);

            return RedirectToAction("Index");
        }
    }
}