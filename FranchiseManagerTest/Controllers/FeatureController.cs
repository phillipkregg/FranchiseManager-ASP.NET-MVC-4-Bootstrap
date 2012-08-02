using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FranchiseManagerTest.Models;

namespace FranchiseManagerTest.Controllers
{   
    public class FeatureController : Controller
    {
		private readonly IFranchiseSetRepository franchisesetRepository;
		private readonly IFeatureRepository featureRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public FeatureController() : this(new FranchiseSetRepository(), new FeatureRepository())
        {
        }

        public FeatureController(IFranchiseSetRepository franchisesetRepository, IFeatureRepository featureRepository)
        {
			this.franchisesetRepository = franchisesetRepository;
			this.featureRepository = featureRepository;
        }

        //
        // GET: /Feature/

        public ViewResult Index()
        {
            return View(featureRepository.AllIncluding(feature => feature.FranchiseSet));
        }

        //
        // GET: /Feature/Details/5

        public ViewResult Details(int id)
        {
            return View(featureRepository.Find(id));
        }

        //
        // GET: /Feature/Create

        public ActionResult Create()
        {
			ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
            return View();
        } 

        //
        // POST: /Feature/Create

        [HttpPost]
        public ActionResult Create(Feature feature)
        {
            if (ModelState.IsValid) {
                featureRepository.InsertOrUpdate(feature);
                featureRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Feature/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
             return View(featureRepository.Find(id));
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        public ActionResult Edit(Feature feature)
        {
            if (ModelState.IsValid) {
                featureRepository.InsertOrUpdate(feature);
                featureRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
				return View();
			}
        }

        //
        // GET: /Feature/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(featureRepository.Find(id));
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            featureRepository.Delete(id);
            featureRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                franchisesetRepository.Dispose();
                featureRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

