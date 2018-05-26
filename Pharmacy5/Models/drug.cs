using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using System.Web;


namespace Pharmacy5.Models
{
    public class drug
    {
        

        [Key]
        public Guid DrugID { get; set; }
        public string GenericName { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        [RegularExpression("[A-Za-z0-9 ]+", ErrorMessage = "Must contain only alphanumeric characters")]
        public string BrandName { get; set; }
        public DateTime? ExpireDate { get; set; }
        //[Required(ErrorMessage = "This is a required field")]
        ////making sure only numbers and no negative values are accepted
        //[RegularExpression("[0-9]+", ErrorMessage = "Wrong Input Format")]
        public string Dose { get; set; }
        public string DoseName { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [RegularExpression("[0-9.]+", ErrorMessage = "Wrong Input Format.")]
        //making sure numbers and no negative values are accepted
        public float SellingUnitPrice { get; set; }
        public string ImgUrl { get; set; }
        public string  BarCode { get; set; }

        public virtual mainstock mainstock { get; set; }
        public drug()
        {
            this.transactions = new HashSet<transaction>();
        }
        public virtual ICollection<transaction> transactions { get; set; }

    }
}