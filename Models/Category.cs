using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoletoGen2.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}