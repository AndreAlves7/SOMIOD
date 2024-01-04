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


    //MESSAGE BROKER 
    
    [HttpGet]
    public ActionResult GetSubscriptions(string applicationName, string containerName)
    {
        var application = applications.FirstOrDefault(a => a.Name == applicationName);
        var container = application?.Containers.FirstOrDefault(c => c.Name == containerName);

        if (container == null)
        {
            return HttpNotFound();
        }

        var subscriptions = container.Subscriptions;

        // Assuming you have a view named SubscriptionIndex for rendering subscriptions
        return View("SubscriptionIndex", subscriptions);
    }

    [HttpPost]
    public ActionResult CreateSubscription(string applicationName, string containerName, SubscriptionModel subscription)
    {
        var application = applications.FirstOrDefault(a => a.Name == applicationName);
        var container = application?.Containers.FirstOrDefault(c => c.Name == containerName);

        if (container == null)
        {
            return HttpNotFound();
        }

        // Implement logic to create a new subscription
        subscription.Id = subscriptionIdCounter++;
        subscription.CreationDt = DateTime.Now;

        container.Subscriptions.Add(subscription);

        return RedirectToAction("GetSubscriptions", new { applicationName, containerName });
    }

    [HttpGet]
    public ActionResult GetNotification(int subscriptionId)
    {
        var subscription = subscriptions.FirstOrDefault(s => s.Id == subscriptionId);

        if (subscription == null)
        {
            return HttpNotFound();
        }

        // Simulate a data record for demonstration purposes
        var dataRecord = new DataModel { Id = 1, Content = "Sample Data", CreationDt = DateTime.Now };

        // Simulate firing a notification
        if (subscription.Event == "Both" || subscription.Event == "Creation")
        {
            Notify(subscription, dataRecord, "Creation");
        }

        // Simulate firing a deletion notification
        if (subscription.Event == "Both" || subscription.Event == "Deletion")
        {
            Notify(subscription, dataRecord, "Deletion");
        }

        return Content("Notification fired successfully.");
    }

    private void Notify(SubscriptionModel subscription, DataModel data, string eventType)
    {
        // Implement notification logic based on the endpoint and eventType
        // You can use libraries like MQTTnet or HttpClient for MQTT and HTTP notifications
        // Here, we're just printing a message for demonstration purposes
        Console.WriteLine($"Firing {eventType} notification to {subscription.Endpoint} with data: {data}");
    }
}