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
using Microsoft.AspNet.Identity.EntityFramework;

namespace ExchangeLogistixMVC.Controllers
{
    public class TrailerController : Controller
    {
        private TrailerDBContext oDB = new TrailerDBContext();

        // GET: Trailer
        public ActionResult Index()
        {
            return View(oDB.Trailer.ToList());
        }

        // GET: Trailer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trailer trailer = oDB.Trailer.Find(id);
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
        public ActionResult Create([Bind(Include = "Id,PhoneNumber,ChasisSize,LoadSize,NextLoadLocation,CurrentLoadDestination,CurrentLoadETA")] Trailer poTrailer)
		{
			string sUserID = User.Identity.GetUserId();

			if (!sUserID.Equals(null) && !sUserID.Equals(""))
			{
				poTrailer.UserID = sUserID;
			}

			if (poTrailer.PhoneNumber.Equals(null) && poTrailer.PhoneNumber.Equals(""))
			{
				var oManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
				var oCurrentUser = oManager.FindById(sUserID);

				if (!oCurrentUser.Equals(null))
				{
					poTrailer.PhoneNumber = oCurrentUser.PhoneNumber;
				}
			}

			if (ModelState.IsValid)
            {
				oDB.Trailer.Add(poTrailer);
				oDB.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poTrailer);
        }

        // GET: Trailer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trailer trailer = oDB.Trailer.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,PhoneNumber,ChasisSize,LoadSize,NextLoadLocation,CurrentLoadDestination,CurrentLoadETA")] Trailer trailer)
        {
            if (ModelState.IsValid)
            {
				oDB.Entry(trailer).State = EntityState.Modified;
				oDB.SaveChanges();
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
            Trailer trailer = oDB.Trailer.Find(id);
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
            Trailer trailer = oDB.Trailer.Find(id);
			oDB.Trailer.Remove(trailer);
			oDB.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				oDB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
