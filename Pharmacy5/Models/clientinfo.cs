using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy5.Models
{
    public class clientinfo
    {
        [Key]
        public Guid clientID { get; set; }

        public string clientname { get; set; }

        public string clientphone { get; set; }
    }
}