using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace somiod.Models
{
    public class SubscriptionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDt { get; set; }
        public int Parent { get; set; }
        public string Event { get; set; }
        public string Endpoint { get; set; }
    }
}