using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy5.Models
{
    public class stockhistory
    {
        [Key]
        public Guid HistoryID { get; set; }
       
        public float Quantity { get; set; }
        public string QuantityName { get; set; }
        public DateTime DateReceived { get; set; }
        public Guid DrugID { get; set; }
        public drug drug { get; set; }

    }
}