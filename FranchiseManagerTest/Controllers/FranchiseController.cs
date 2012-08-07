using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FranchiseManagerTest.Models;

namespace FranchiseManagerTest.Controllers
{   
	public class FranchiseController : Controller
	{
		private readonly IFranchiseSetRepository franchisesetRepository;
		private readonly IFranchiseRepository franchiseRepository;

		// If you are using Dependency Injection, you can delete the following constructor
		public FranchiseController() : this(new FranchiseSetRepository(), new FranchiseRepository())
		{
		}

		public FranchiseController(IFranchiseSetRepository franchisesetRepository, IFranchiseRepository franchiseRepository)
		{
			this.franchisesetRepository = franchisesetRepository;
			this.franchiseRepository = franchiseRepository;
		}

		//
		// GET: /Franchise/

		public ViewResult Index()
		{
           
			return View(franchiseRepository.AllIncluding(franchise => franchise.FranchiseSet));
		}

		//
		// GET: /Franchise/Details/5

		public ViewResult Details(int id)
		{            
			return View(franchiseRepository.Find(id));
		}

		//
		// GET: /Franchise/Create

		public PartialViewResult Create(int FranchiseSetId)
		{
			ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
			var franchise = new Franchise { FranchiseSetId = FranchiseSetId };
			return PartialView(franchise);
		} 

		//
		// POST: /Franchise/Create

		[HttpPost]
		public ActionResult Create(Franchise franchise)
		{
			if (ModelState.IsValid) {
				franchiseRepository.InsertOrUpdate(franchise);
				franchiseRepository.Save();
                return RedirectToAction("Index", "FranchiseSet", new { id = franchise.FranchiseSetId });
			} else {
				ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
				return View();
			}
		}
		
		//
		// GET: /Franchise/Edit/5
 
		public PartialViewResult Edit(int id)
		{
			ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
            var franchise = franchiseRepository.Find(id);
			 return PartialView(franchise);
		}

		//
		// POST: /Franchise/Edit/5

		[HttpPost]
		public ActionResult Edit(Franchise franchise)
		{
			if (ModelState.IsValid) {
				franchiseRepository.InsertOrUpdate(franchise);
				franchiseRepository.Save();
                return RedirectToAction("Index", "FranchiseSet", new { id = franchise.FranchiseSetId });
			} else {
				ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
				return View();
			}
		}

		//
		// GET: /Franchise/Delete/5
 
		public PartialViewResult Delete(int id)
		{            
			return PartialView(franchiseRepository.Find(id));
		}

		//
		// POST: /Franchise/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			franchiseRepository.Delete(id);
			franchiseRepository.Save();

			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				franchisesetRepository.Dispose();
				franchiseRepository.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}

