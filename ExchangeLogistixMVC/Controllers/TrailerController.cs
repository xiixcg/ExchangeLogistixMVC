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
        private TrailerDBContext oTrailerDB = new TrailerDBContext();
		
        // GET: Trailer
        public ActionResult Index()
        {
			if (!Request.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}

            return View(oTrailerDB.Trailer.ToList());
        }

        // GET: Trailer/Details/5
        public ActionResult Details(int? pnID)
        {
			if (!Request.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			if (pnID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trailer oCurrentTrailer = oTrailerDB.Trailer.Find(pnID);
            if (oCurrentTrailer == null)
            {
                return HttpNotFound();
            }
            return View(oCurrentTrailer);
        }

        // GET: Trailer/Create
        public ActionResult Create()
        {
			if (!Request.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
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
				oTrailerDB.Trailer.Add(poTrailer);
				oTrailerDB.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poTrailer);
        }

		//Get: Trailer/MyTrailer
		public ActionResult MyTrailer(string psID)
		{
			if (!Request.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			if (psID == null || psID == "")
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			IEnumerable<Trailer> oTrailerQuery =
			from oTrailer in oTrailerDB.Trailer.ToList()
			//where oTrailer.UserID == psID
			where oTrailer.CurrentLoadETA >= DateTime.Now
			select oTrailer;

			return View(oTrailerQuery);
		}

		//Get: Trailer/PastTrailer
		public ActionResult PastTrailer(string psID)
		{
			if (!Request.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			if (psID == null || psID == "")
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			IEnumerable<Trailer> oTrailerQuery =
			from oTrailer in oTrailerDB.Trailer.ToList()
				//where oTrailer.UserID == psID
			where oTrailer.CurrentLoadETA < DateTime.Now
			select oTrailer;

			return View(oTrailerQuery);
		}

		//Get: Trailer/ShowMatch
		public ActionResult ShowMatch(int? pnID)
		{
			if (!Request.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			if (pnID == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Trailer oCurrentTrailer = oTrailerDB.Trailer.Find(pnID);
			if (oCurrentTrailer == null)
			{
				return HttpNotFound();
			}
			IEnumerable<Trailer> oTrailerQuery = 
			from oTrailer in oTrailerDB.Trailer.ToList() 
			where oTrailer.NextLoadLocation == oCurrentTrailer.CurrentLoadDestination
			where oTrailer.CurrentLoadETA == oCurrentTrailer.CurrentLoadETA
			where oTrailer.UserID != oCurrentTrailer.UserID
			select oTrailer;

			return View(oTrailerQuery);
		}

		// GET: Trailer/Edit/5
		public ActionResult Edit(int? pnID)
        {
			if (!Request.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			if (pnID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trailer oCurrentTrailer = oTrailerDB.Trailer.Find(pnID);
            if (oCurrentTrailer == null)
            {
                return HttpNotFound();
            }
            return View(oCurrentTrailer);
        }

        // POST: Trailer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhoneNumber,ChasisSize,LoadSize,NextLoadLocation,CurrentLoadDestination,CurrentLoadETA")] Trailer poTrailer)
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
				oTrailerDB.Entry(poTrailer).State = EntityState.Modified;
				oTrailerDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poTrailer);
        }

        // GET: Trailer/Delete/5
        public ActionResult Delete(int? pnID)
        {
			if (!Request.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			if (pnID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trailer oCurrentTrailer = oTrailerDB.Trailer.Find(pnID);
            if (oCurrentTrailer == null)
            {
                return HttpNotFound();
            }
            return View(oCurrentTrailer);
        }

        // POST: Trailer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int pnID)
        {
            Trailer oCurrentTrailer = oTrailerDB.Trailer.Find(pnID);
			oTrailerDB.Trailer.Remove(oCurrentTrailer);
			oTrailerDB.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool pbDisposing)
        {
            if (pbDisposing)
            {
				oTrailerDB.Dispose();
            }
            base.Dispose(pbDisposing);
        }
    }
}
