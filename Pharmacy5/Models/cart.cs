using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy5.Models
{
    public class cart
    {
        [Key]
        
        public Guid ItemID { get; set; }
        //public int UserID { get; set; }
        public string  GenericName { get; set; }
        public string BrandName { get; set; }
        public float UnitPrice { get; set; }
        

    }
}