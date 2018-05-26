using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy5.Models
{
    public class order
    {
        public Guid OrderID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string DrugDetails { get; set; }
    }
}