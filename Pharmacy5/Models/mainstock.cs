using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pharmacy5.Models
{
    public class mainstock
    {
        [ForeignKey("drug")]
        [Key]
        public Guid DrugID { get; set; }
        //public Guid DrugID { get; set; }
        public int QuantityInStock { get; set; }
        public virtual drug drug { get; set; }

    }
}