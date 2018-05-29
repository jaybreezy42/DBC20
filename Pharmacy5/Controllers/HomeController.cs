using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Pharmacy5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Pharmacy5.Controllers
{
    [RequireHttps]
    
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public void GetDetails()
        {
            var contet = new ApplicationDbContext();
            ViewBag.employees = contet.Users;
            
            using (var context = new ApplicationDbContext())
            {
               
                if (User.Identity.IsAuthenticated)
                {
                    var name = User.Identity.Name;
                    var ID = User.Identity.GetUserId();

                    //var role = UserManager.GetRoles(ID);
                    var role = UserManager.GetRoles(ID).FirstOrDefault();
                    var userProfile = context.Users.Where(x => x.Id.Equals(ID)).FirstOrDefault();
                    var NumberOfUsers = context.Users.Count();
                    TempData["NOU"] = NumberOfUsers;
                    ViewBag.Username = userProfile.UserName;
                    ViewBag.ProfilePic = "/Images/" + userProfile.ImageUrl;
                    ViewBag.UserRole = role;
                    //ViewBag.UserRole = role[0];
                }
            }
        }

        public ActionResult Home()
        {
            ViewBag.SearchResult = "";
            return View();
        }
        [HttpPost]
        public ActionResult Home(string txtSearch)
        {
            ViewBag.SearchResult = db.drugs.Where(m => m.BrandName.Contains(txtSearch) || m.GenericName.Contains(txtSearch)).AsEnumerable();
            return View();
        }
        
        public async Task<ActionResult> Home2()
        {
            GetDetails();
            if (TempData.ContainsKey("LogFailed"))
            {
                ViewBag.logfailed = TempData["LogFailed"];
                TempData["LogFailed"] = "";
            }
            if (TempData.ContainsKey("ModelError"))
            {
                ViewBag.ModelError = TempData["ModelError"];
                TempData["ModelError"] = "";
            }
            string Pending = "Pending";
            //var a = ViewBag.logfailed;
            var resul = await db.transactions.Where(m => m.Status.Equals(Pending)).ToListAsync();
            ViewBag.SearchResult = "";
            ViewBag.Pending = resul.AsEnumerable();
            ViewBag.active = "";
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Employee,admin")]
        public async Task<ActionResult> Home2(SearchHistory searchHistory)
        {
            GetDetails();
            var result = await db.drugs.Where(m => m.BrandName.Contains(searchHistory.searchitem.Trim()) || m.GenericName.Contains(searchHistory.searchitem.Trim())).ToListAsync();
            string Pending = "Pending";
            var resul = await db.transactions.Where(m => m.Status.Equals(Pending)).ToListAsync();
            ViewBag.SearchResult = result.AsEnumerable();
            ViewBag.Pending = resul.AsEnumerable();
            ViewBag.active = 0;
            if (!Request.IsAuthenticated)
            {
                ViewBag.logfailed = "Invalid login attempt.";
                return View();
            }

            return View();
        }

        [Authorize(Roles = "Employee,admin")]
        public ActionResult AddtoTrans()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Employee,admin")]
        public async Task<ActionResult> AddtoTrans(transaction model)
        {
            if (ModelState.IsValid)
            {
                var transaction = new transaction { transactionID = Guid.NewGuid() };
                //transaction.transactionID = Guid.NewGuid();
                Guid DrugID = Guid.Parse(Request.Form["DrugID"]);
                transaction.drugs.Add(new drug { DrugID = DrugID });
                db.transactions.Add(transaction);

                await db.SaveChangesAsync();
                return RedirectToAction("Home2");
            }
            return View();
        }
        [Authorize(Roles = "Employee,admin")]
        public async Task<ActionResult> UpdateTrans([Bind(Include = "transactionID")]transaction transaction)
        {
            List<string> product = new List<string>();
            int nums = (Request.QueryString.Count - 1) / 3;
            string DateOfTrans = DateTime.Now + "";
            string clientname = Request.QueryString["clientname"];
            string clientphone = Request.QueryString["clientphone"];

            for (int i = 0; i < nums; i++)
            {
                string cho = "cho" + i;
                string chku = "chk" + i;
                string quant = "Quant" + i;
                Guid DrugID = Guid.Parse(Request.QueryString[chku]);
                Guid transactionID = Guid.Parse(Request.QueryString[cho]);
                int Quantity = int.Parse(Request.QueryString[quant]);
                
                if (ModelState.IsValid)
                {
                    var clientinfo = new clientinfo();
                    clientinfo.clientID = Guid.NewGuid();
                    clientinfo.clientname = clientname;
                    clientinfo.clientphone = clientphone;
                    (await db.transactions.FindAsync(transactionID)).clientID  = clientinfo.clientID;
                    (await db.transactions.FindAsync(transactionID)).DateOfTrans = DateOfTrans;
                    (await db.transactions.FindAsync(transactionID)).Quantity = Quantity;
                    (await db.transactions.FindAsync(transactionID)).Status = "Sold";
                    db.Clientinfos.Add(clientinfo);
                    db.Entry(await db.transactions.FindAsync(transactionID)).State = EntityState.Modified;
                    var a = await db.SaveChangesAsync();
                    
                    

                }

            }
            string from = "LizMat Pharmacy";
            string to = "+233"+clientphone.Substring(1);
            string prod = "";
            foreach (var item in product)
            {
                prod += "" + item + "";
            }
            string Total = Request.QueryString["total"];
            string message = "Transaction Receipt. Date:"+ DateOfTrans+" Products: "+prod+" Total Price: "+ Total +"" ;
            
            //transaction.SendTextMessage(from, to, prod);
            return RedirectToAction("Home2");
            
        }
        
        [Authorize(Roles = "Employee,admin")]
        public JsonResult GetSearchValue(string search)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            //var allsearch = new List<DrugAutoComplete>();
            var allsearch = db.drugs.Where(m => m.BrandName.Contains(search) || m.GenericName.Contains(search))
                .Select(m => new
                {
                    DrugID = m.DrugID.ToString(),
                    BrandName = m.BrandName,
                    GenericName = m.GenericName,
                    Dose = m.Dose,
                    DoseName = m.DoseName
                }).ToList();
            //foreach (var item in results)
            //{
            //allsearch.Add(item);
            //}
            //allsearch.Add(results)
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [Authorize(Roles = "admin")]
      
        public ActionResult AdminPanel()
        {
            GetDetails();            
            return View();
        }
          
        [HttpPost]
        [Authorize(Roles = "Employee,admin")]
        public async Task<ActionResult> DeleteTransaction([Bind(Include = "transactionID")]transaction transaction)
        {
            //Guid transactionID = Guid.Parse(Request.Form["transactionID"]);
            if (ModelState.IsValid)
            { /*(transactionID)).transactionID = transactionID;*/
              //( await db.transactions.FindAsync(transactionID)).transactionID
              //var trans = await db.transactions.FindAsync(model.transactionID);
                Guid ID = transaction.transactionID;
                transaction = await db.transactions.FindAsync(ID);
                //db.transactions.SqlQuery("DELETE FROM transactions WHERE transactionID ='@transactionID'",transactionID);
                db.transactions.Remove(transaction);
                //db.Entry(await db.transactions.FindAsync(transactionID)).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Home2");
            }
            return RedirectToAction("Home2");
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Inventory()
        {
            var date = DateTime.Now.Date.ToShortDateString();
            try
            {
                var datequery = db.transactions.Where(m => m.DateOfTrans.Contains(date)).Count();
                       
                if (datequery != 0 )
                {
                    //gets the total amount received for transactions made per day    
                    var today = await db.transactions.Where(m => m.DateOfTrans.Contains(date)).Select(m => m.TotalAmount).SumAsync();
                    var todaycash = Math.Round(today, 2);
                    ViewBag.todaycash = todaycash;
                    //gets the number of transaction made per day
                    ViewBag.Tnumber = await db.transactions.Where(m => m.DateOfTrans.Contains(date)).Select(m => m.transactionID).CountAsync();
                }
                else
                {
                    ViewBag.todaycash = "";
                    ViewBag.Tnumber = "";
                }
               



                //gets the number of transaction made through out app life cycle
                var transactionQuery = await db.transactions.CountAsync();
                if (transactionQuery != 0)
                {
                    var total = await db.transactions.Select(m => m.TotalAmount).SumAsync();
                    var totalcash = Math.Round(total, 2).ToString();
                    ViewBag.totalcash = totalcash;
                    //gets details of the latest transaction made
                    var transactions = await db.transactions.OrderByDescending(m => m.DateOfTrans).ToListAsync();
                    ViewBag.Transaction = transactions.AsEnumerable();
                }
                else
                {
                    ViewBag.totalcash = "";
                    ViewBag.Transaction = "";
                }




                //gets details of the drugs and their current stock value
                var Bermah = await db.drugs.Join(db.mainstocks, d => d.DrugID, f => f.DrugID, (d, f) => d).CountAsync();
                if (Bermah != 0)
                {
                    var robotDogs = await db.drugs.Join(db.mainstocks, d => d.DrugID, f => f.DrugID, (d, f) => d).ToListAsync();
                    ViewBag.productDetails = robotDogs.AsEnumerable();
                }
                else
                {
                    ViewBag.productDetails = "";
                }
                
                
               
                DateTime dates = DateTime.Now;
                var dat = TimeSpan.FromDays(2);
                //string query = "SELECT COUNT(BrandName) AS ExpDrug,BrandName,(ExpireDate -GETDATE()) FROM drugs WHERE (ExpireDate -GETDATE()) < 180 group by ExpireDate,BrandName;";
                //ViewBag.notes = await db.drugs.SqlQuery("SELECT COUNT(BrandName) AS ExpDrug,BrandName,(ExpireDate -GETDATE()) FROM drugs WHERE (ExpireDate -GETDATE()) < 180 group by ExpireDate,BrandName;").ToListAsync();

                //TimeSpan daye = dates.Subtract(DateTime.Parse(dat+""));
                //var mom = daye.Subtract(days);
                //creating a drugID list to hold list of drugs due to expire
                List<Guid> DrugIDList = new List<Guid>();

                //Get list of all drug from db to be used in the loop to find expiring ones
                var DrugList = db.drugs.ToList();
                var DrugEnum = DrugList.AsEnumerable();
                //loop searches through DrugEnum to find expiring drugs and add their drugID to the DrugIDList created earlier
                foreach (var item in DrugEnum)
                {
                    var ExpireDateTimeSpan = item.ExpireDate - DateTime.Now;
                    if (ExpireDateTimeSpan != null)
                    {
                        if (ExpireDateTimeSpan - TimeSpan.FromDays(180) < TimeSpan.FromDays(0))
                        {
                            DrugIDList.Add(item.DrugID);
                        }
                    }


                }
                //returns number of drugs due to expire by counting drugIDs in the DrugIDList to view
                var count = DrugIDList.Count();
                if (count!=0)
                {
                    ViewBag.DueToExpireDrugs = DrugIDList.Count();
                    //converts the DrugIDList to and Enumerable which will be looped to get details of drugs by their id
                    var DrugIDListEnum = DrugIDList.AsEnumerable();
                    List<drug> drugs = new List<drug>();
                    foreach (var item in DrugIDListEnum)
                    {
                        var DrugID = item;
                        var drug = await db.drugs.Where(m => m.DrugID.Equals(DrugID)).FirstOrDefaultAsync();

                        drugs.Add(drug);
                    }
                    //return "list" of drugs that are due to expire to view
                    ViewBag.ExpireDrugs = drugs.AsEnumerable();
                }
                else
                {
                    ViewBag.DueToExpireDrugs = "";
                    ViewBag.ExpireDrugs = "";
                }
               
                //getting info on drugs that are running out of stock with a minimum of 10 products
                int threshold = 10;
                var DueOutOfStock = await db.drugs.Join(db.mainstocks, d => d.DrugID, f => f.DrugID, (d, f) => d).Where(m => m.mainstock.QuantityInStock - threshold <= 0).ToListAsync();
                var NumOfDueOutOfStock = await db.mainstocks.Where(m => m.QuantityInStock - threshold <= 0).CountAsync();
                
                if (NumOfDueOutOfStock != 0)
                {
                    ViewBag.outofstock = DueOutOfStock.AsEnumerable();
                    ViewBag.outnum = NumOfDueOutOfStock.ToString(); 
                }
                else
                {
                    ViewBag.outofstock = "";
                    ViewBag.outnum = "";
                }
                


            }
            catch (Exception e)
            {
                var exception = e.Message;
            }
            finally
            {
                
            }
            //returning number of employees to view
            if (TempData["NOU"] != null)
            {
                ViewBag.NOU = TempData["NOU"];
            }
            
            return View();

        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ReStock([Bind(Include = "DrugID")]mainstock mainstock)
        {
            if (ModelState.IsValid)
            {
                int QuantityReload = int.Parse(Request.Form["QuantityReload"]);
                Guid DrugID = Guid.Parse(Request.Form["StockID"]);
                (await db.mainstocks.FindAsync(DrugID)).QuantityInStock += QuantityReload;
                return RedirectToAction("Inventory");
            }
            return HttpNotFound();
        }
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }


    }
}