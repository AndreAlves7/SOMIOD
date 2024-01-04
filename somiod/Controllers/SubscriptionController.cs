using somiod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace somiod.Controllers
{
    public class SubscriptionController : Controller
    {
        private static List<SubscriptionModel> subscriptions = new List<SubscriptionModel>();
        private static int subscriptionIdCounter = 1;

        public ActionResult Index()
        {
            return View(subscriptions);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SubscriptionModel subscription)
        {
            subscription.Id = subscriptionIdCounter++;
            subscription.CreationDt = DateTime.Now;

            subscriptions.Add(subscription);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var subscription = subscriptions.Find(s => s.Id == id);

            if (subscription == null)
            {
                return HttpNotFound();
            }

            return View(subscription);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var subscription = subscriptions.Find(s => s.Id == id);

            if (subscription == null)
            {
                return HttpNotFound();
            }

            return View(subscription);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var subscription = subscriptions.Find(s => s.Id == id);

            if (subscription == null)
            {
                return HttpNotFound();
            }

            subscriptions.Remove(subscription);

            return RedirectToAction("Index");
        }
    }
}