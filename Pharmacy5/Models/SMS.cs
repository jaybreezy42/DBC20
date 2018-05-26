using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy5.Models
{
    public class SMS
    {
        [Key]
        public Guid SMSID { get; set; }
        public string  UserName { get; set; }
        public string  Password { get; set; }
    }
}