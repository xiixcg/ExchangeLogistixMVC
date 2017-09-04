using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExchangeLogistixMVC.Models;
using Microsoft.AspNet.Identity;

namespace ExchangeLogistixMVC.Controllers
{
    public class TrailerController : Controller
    {
        private TrailerDBContext db = new TrailerDBContext();

        // GET: Trailer
        public ActionResult Index()
        {
			var oCurrentUserID = User.Identity.GetUserId();
            return View(db.Trailer.ToList());
        }

        // GET: Trailer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trailer trailer = db.Trailer.Find(id);
            if (trailer == null)
            {
                return HttpNotFound();
            }
            return View(trailer);
        }

        // GET: Trailer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trailer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,PhoneNumber,ChasisSize,LoadSize,NextLoadLocation,CurrentLoadDestination,CurrentLoadETA")] Trailer trailer)
        {
            if (ModelState.IsValid)
            {
                db.Trailer.Add(trailer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trailer);
        }

        // GET: Trailer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trailer trailer = db.Trailer.Find(id);
            if (trailer == null)
            {
                return HttpNotFound();
            }
            return View(trailer);
        }

        // POST: Trailer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,PhoneNumber,ChasisSize,LoadSize,NextLoadLocation,CurrentLoadDestination,CurrentLoadETA")] Trailer trailer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trailer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trailer);
        }

        // GET: Trailer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trailer trailer = db.Trailer.Find(id);
            if (trailer == null)
            {
                return HttpNotFound();
            }
            return View(trailer);
        }

        // POST: Trailer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trailer trailer = db.Trailer.Find(id);
            db.Trailer.Remove(trailer);
            db.SaveChanges();
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
