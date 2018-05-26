using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy5.Models
{
    public class SearchHistory
    {[Key]
        public int searchID { get; set; }
        public string searchitem { get; set; }
    }
}