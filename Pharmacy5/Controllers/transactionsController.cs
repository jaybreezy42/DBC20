using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pharmacy5.Models;

namespace Pharmacy5.Controllers
{
    public class transactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: transactions
        public async Task<ActionResult> Index()
        {
            var transactions = db.transactions.Include(t => t.clientinfo);
            return View(await transactions.ToListAsync());
        }

        // GET: transactions/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = await db.transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: transactions/Create
        public ActionResult Create()
        {
            ViewBag.clientID = new SelectList(db.Clientinfos, "clientID", "clientname");
            return View();
        }

        // POST: transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "transactionID,Quantity,TotalAmount,Status,DateOfTrans,clientID")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.transactionID = Guid.NewGuid();
                Guid DrugID = Guid.Parse(Request.Form["drugID"]);
                transaction.drugs.Add(new drug { DrugID = DrugID });
                db.transactions.Add(transaction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.clientID = new SelectList(db.Clientinfos, "clientID", "clientname", transaction.clientID);
            return View(transaction);
        }

        // GET: transactions/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = await db.transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientID = new SelectList(db.Clientinfos, "clientID", "clientname", transaction.clientID);
            return View(transaction);
        }

        // POST: transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "transactionID,Quantity,TotalAmount,Status,DateOfTrans,clientID")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.clientID = new SelectList(db.Clientinfos, "clientID", "clientname", transaction.clientID);
            return View(transaction);
        }

        // GET: transactions/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = await db.transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            transaction transaction = await db.transactions.FindAsync(id);
            db.transactions.Remove(transaction);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
