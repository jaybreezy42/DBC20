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

namespace Pharmacy5.Views
{
    [Authorize(Roles ="Admin")]
    public class drugs1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: drugs1
        public async Task<ActionResult> Index()
        {
            return View(await db.drugs.ToListAsync());
        }

        // GET: drugs1/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            drug drug = await db.drugs.FindAsync(id);
            if (drug == null)
            {
                return HttpNotFound();
            }
            return View(drug);
        }

        // GET: drugs1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: drugs1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DrugID,GenericName,BrandName,Dose,DoseName,SellingUnitPrice,ImgUrl,ExpireDate,BarCode")] drug drug)
        {
            if (ModelState.IsValid)
            {
                drug.DrugID = Guid.NewGuid();
                var Drug = drug.DrugID;
                var mainstock = new mainstock();
                mainstock.DrugID = Drug;
                //mainstock.DrugID = Drug;
                mainstock.QuantityInStock = 0;
                db.mainstocks.Add(mainstock);
                db.drugs.Add(drug);
                await db.SaveChangesAsync();
                return Redirect("/Home/Inventory/");
            }

            return HttpNotFound();
        }

        // GET: drugs1/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            drug drug = await db.drugs.FindAsync(id);
            if (drug == null)
            {
                return HttpNotFound();
            }
            return View(drug);
        }

        // POST: drugs1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DrugID,GenericName,BrandName,Dose,DoseName,SellingUnitPrice,ImgUrl")] drug drug)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drug).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(drug);
        }

        // GET: drugs1/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            drug drug = await db.drugs.FindAsync(id);
            if (drug == null)
            {
                return HttpNotFound();
            }
            return View(drug);
        }

        // POST: drugs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            drug drug = await db.drugs.FindAsync(id);
            db.drugs.Remove(drug);
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
