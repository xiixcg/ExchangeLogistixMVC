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
        private ApplicationDbContext oApplicationDBContext = new ApplicationDbContext();
		
        // GET: Trailer
        public ActionResult Index(string sortOrder)
        {		
			if (!Request.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}

			ViewBag.DateSortParm = String.IsNullOrEmpty (sortOrder) ? "date_desc" : "";
			IQueryable<Trailer> oTrailers = from oTrailer in oApplicationDBContext.Trailers
							select oTrailer;
			switch (sortOrder)
			{
				case "date_desc":
					oTrailers = oTrailers.OrderByDescending (oTrailer => oTrailer.CurrentLoadETA);
					break;
				default:
					oTrailers = oTrailers.OrderBy (oTrailer => oTrailer.CurrentLoadETA);
					break;
			}
				
				return View(oTrailers.ToList());
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
            Trailer oCurrentTrailer = oApplicationDBContext.Trailers.Find(pnID);
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
        public ActionResult Create([Bind(Include = "PhoneNumber,ChasisSize,LoadSize,NextLoadLocation,CurrentLoadDestination,CurrentLoadETA")] Trailer poTrailer)
		{
			try
			{
				if (isAbleToSetUserIDAsApplicationUserID(poTrailer) && isAbleToSetPhonNumber(poTrailer) && ModelState.IsValid)
				{
					oApplicationDBContext.Trailers.Add(poTrailer);
					oApplicationDBContext.SaveChanges();
					return RedirectToAction("MyTrailer");
				}
			}
			catch (DataException oError)
			{
				ModelState.AddModelError("", "Unable to save changes, Please try again and contact your system administrator if problem persists.");
			}
			return View(poTrailer);
        }

		//Get: Trailer/MyTrailer
		public ActionResult MyTrailer(string sortOrder)
		{
			if (!Request.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			
			ApplicationUser oCurrentUser = getCurrentUser();
			IEnumerable<Trailer> oTrailerQuery =
			from oTrailer in oCurrentUser.Trailers
			where oTrailer.CurrentLoadETA >= DateTime.Now
			select oTrailer;
			
			return View(oTrailerQuery.ToList());
		}

		//Get: Trailer/PastTrailer
		public ActionResult PastTrailer()
		{
			if (!Request.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}

			ApplicationUser oCurrentUser = getCurrentUser();
			IEnumerable<Trailer> oTrailerQuery =
			from oTrailer in oCurrentUser.Trailers
			where oTrailer.CurrentLoadETA < DateTime.Now
			select oTrailer;

			return View(oTrailerQuery.ToList());
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
			Trailer oCurrentTrailer = oApplicationDBContext.Trailers.Find(pnID);
			if (oCurrentTrailer == null)
			{
				return HttpNotFound();
			}
			IEnumerable<Trailer> oTrailerQuery = 
			from oTrailer in oApplicationDBContext.Trailers 
			where oTrailer.NextLoadLocation == oCurrentTrailer.CurrentLoadDestination
			where oTrailer.CurrentLoadETA == oCurrentTrailer.CurrentLoadETA
			where oTrailer.ApplicationUserID != oCurrentTrailer.ApplicationUserID
			select oTrailer;

			return View(oTrailerQuery.ToList());
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
            Trailer oCurrentTrailer = oApplicationDBContext.Trailers.Find(pnID);
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
        public ActionResult Edit([Bind(Include = "TrailerID, ApplicationUserID, PhoneNumber,ChasisSize,LoadSize,NextLoadLocation,CurrentLoadDestination,CurrentLoadETA")] Trailer poTrailer)
        {
			try
			{
				if (isAbleToSetPhonNumber(poTrailer) && ModelState.IsValid)
				{
					oApplicationDBContext.Entry(poTrailer).State = EntityState.Modified;
					oApplicationDBContext.SaveChanges();
					return RedirectToAction("MyTrailer");
				}
			}
			catch (DataException oError)
			{
				ModelState.AddModelError("", "Unable to save changes, Please try again and contact your system administrator if problem persists.");
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
            Trailer oCurrentTrailer = oApplicationDBContext.Trailers.Find(pnID);
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
            Trailer oCurrentTrailer = oApplicationDBContext.Trailers.Find(pnID);
			oApplicationDBContext.Trailers.Remove(oCurrentTrailer);
			oApplicationDBContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool pbDisposing)
        {
            if (pbDisposing)
            {
				oApplicationDBContext.Dispose();
            }
            base.Dispose(pbDisposing);
        }

		private bool isAbleToSetUserIDAsApplicationUserID(Trailer poTrailer)
		{
			string sUserID = User.Identity.GetUserId();
			if (!sUserID.Equals(null) && !sUserID.Equals(""))
			{
				poTrailer.ApplicationUserID = sUserID;
				return true;
			}
			return false;
		}

		private bool isAbleToSetPhonNumber(Trailer poTrailer)
		{
			if (!isNullOrWhiteSpace(poTrailer.PhoneNumber))
			{
				return true;
			}

			ApplicationUser oCurrentUser = getCurrentUser();
			if (isSomething(oCurrentUser))
			{
				poTrailer.PhoneNumber = oCurrentUser.PhoneNumber;
				return true;
			}			

			return false;
		}

		private ApplicationUser getCurrentUser()
		{
			string sUserID = User.Identity.GetUserId();
			var oManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

			if (oManager != null && !isNullOrWhiteSpace(sUserID))
			{
				return oManager.FindById(sUserID);
			}			
			return null;
		}

		private bool isNullOrWhiteSpace(string psString)
		{
			if (psString == null)
			{
				return true;
			}

			if (psString == "")
			{
				return true;
			}

			return false;
		}

		private bool isSomething(object poObject)
		{
			if (poObject != null)
			{
				return true;
			}

			return false;
		}
    }
}
