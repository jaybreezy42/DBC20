using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Web;


namespace Pharmacy5.Models
{
    public class transaction
    {
        [Key]
        public Guid transactionID { get; set; }
            
        [RegularExpression("[0-9]+", ErrorMessage = "Wrong Input Format.")]
        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
       
        public string Status { get; set; }
        public string DateOfTrans { get; set; }
        public clientinfo clientinfo { get; set; }
        public Guid clientID { get; set; }
             
        public transaction()
        {
            this.drugs = new HashSet<drug>();
        }
        public virtual ICollection<drug> drugs { get; set; }

        public void SendTextMessage(string from, string to, string message)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SMS smsID = new SMS();
            Guid SmsID = smsID.SMSID;
            string userName = db.SMs.Find(SmsID).UserName;
            string password = db.SMs.Find(SmsID).Password;
            bool RegisteredDelievery = true;
            try
            {
                string url = "https://api.hubtel.com/v1/messages/send?From=" + from + "&To=" + to + "&Content=" + message + "&ClientId=" + userName + "&ClientSecret=" + password + "&RegisteredDelivery=" + RegisteredDelievery + "";
                WebClient client = new WebClient();
                Uri uri = new Uri(url);
                client.DownloadStringAsync(uri);
                

            }
            catch (Exception ex)
            {

            }
        }

    }
}