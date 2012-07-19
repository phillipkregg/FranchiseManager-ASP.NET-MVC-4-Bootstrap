using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FranchiseManagerTest.Models;
using System.Configuration;

namespace FranchiseManagerTest.Controllers
{   
	public class FranchiseSetController : Controller
	{
        

		private readonly IFranchiseSetRepository franchisesetRepository;

		// If you are using Dependency Injection, you can delete the following constructor
		public FranchiseSetController() : this(new FranchiseSetRepository())
		{
		}

		public FranchiseSetController(IFranchiseSetRepository franchisesetRepository)
		{
			this.franchisesetRepository = franchisesetRepository;
		}

		//
		// GET: /FranchiseSet/

		public ViewResult Index()
		{
            
            var Name = System.Configuration.ConfigurationManager.AppSettings.Get("name");
            ViewBag.Name = Name;
            
			return View(franchisesetRepository.AllIncluding(franchiseset => franchiseset.Franchises));
		}

		//
		// GET: /FranchiseSet/Details/5

		public ActionResult Details(int id)
		{
			var model = franchisesetRepository.Find(id);
			return PartialView(model);
		}

		//
		// GET: /FranchiseSet/Create

		public ActionResult Create()
		{
			return View();
		} 

		//
		// POST: /FranchiseSet/Create

		[HttpPost]
		public ActionResult Create(FranchiseSet franchiseset)
		{
			if (ModelState.IsValid) {
				franchisesetRepository.InsertOrUpdate(franchiseset);
				franchisesetRepository.Save();
				return RedirectToAction("Index");
			} else {
				return View();
			}
		}
		
		//
		// GET: /FranchiseSet/Edit/5
 
		public ActionResult Edit(int id)
		{
			 return View(franchisesetRepository.Find(id));
		}

		//
		// POST: /FranchiseSet/Edit/5

		[HttpPost]
		public ActionResult Edit(FranchiseSet franchiseset)
		{
			if (ModelState.IsValid) {
				franchisesetRepository.InsertOrUpdate(franchiseset);
				franchisesetRepository.Save();
				return RedirectToAction("Index");
			} else {
				return View();
			}
		}

		//
		// GET: /FranchiseSet/Delete/5
 
		public ActionResult Delete(int id)
		{
			return View(franchisesetRepository.Find(id));
		}

		//
		// POST: /FranchiseSet/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			franchisesetRepository.Delete(id);
			franchisesetRepository.Save();

			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				franchisesetRepository.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}

