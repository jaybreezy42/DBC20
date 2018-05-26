using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy5.Models
{
    public class user
    {
        [Key]
        public Guid UserID { get; set; }
        public string  UserName { get; set; }
    }
}