using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy5.Models
{
    public class DrugAutoComplete
    {
        [Key]
        public Guid DrugID { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
    }
}