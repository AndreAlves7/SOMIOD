using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace somiod.Models
{
    public class DataModel
    {
        public int Id { get; set; }
        public string Name { get; set; } //Importante - não está no enunciado
        public string Content { get; set; }
        public DateTime CreationDt { get; set; }
        public int Parent { get; set; }
    }
}