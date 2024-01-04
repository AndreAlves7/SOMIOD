using System;
using System.Collections.Generic;

namespace somiod.Models
{
    public class ApplicationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDt { get; set; }

        public List<ContainerModel> Containers { get; set; }
        public List<ApplicationModel> Applications { get; set; }
    }
}