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
    public class SMSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SMS
        public async Task<ActionResult> Index()
        {
            return View(await db.SMs.ToListAsync());
        }

        // GET: SMS/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = await db.SMs.FindAsync(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }

        // GET: SMS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SMSID,UserName,Password")] SMS sMS)
        {
            if (ModelState.IsValid)
            {
                sMS.SMSID = Guid.NewGuid();
                db.SMs.Add(sMS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sMS);
        }

        // GET: SMS/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = await db.SMs.FindAsync(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }

        // POST: SMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SMSID,UserName,Password")] SMS sMS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sMS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sMS);
        }

        // GET: SMS/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = await db.SMs.FindAsync(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }

        // POST: SMS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            SMS sMS = await db.SMs.FindAsync(id);
            db.SMs.Remove(sMS);
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
