using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace somiod.Models
{
    public class ContainerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDt { get; set; }
        public int Parent { get; set; }

        public List<DataModel> Data { get; set; }
        public List<SubscriptionModel> Subscriptions { get; set; }
    }
}