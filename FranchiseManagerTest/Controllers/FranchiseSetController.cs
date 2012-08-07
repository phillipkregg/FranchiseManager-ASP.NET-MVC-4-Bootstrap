using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FranchiseManagerTest.Models;
using System.Configuration;
using FranchiseManagerTest.ViewModels;
using System.Data;
using System.Data.Entity;

namespace FranchiseManagerTest.Controllers
{   
	//[Authorize(Roles="Administrator")]
	public class FranchiseSetController : Controller
	{
		private FranchiseManagerContext db = new FranchiseManagerContext();		

		private readonly IFranchiseSetRepository franchisesetRepository;
		private readonly IFeatureRepository featureRepository;

		// If you are using Dependency Injection, you can delete the following constructor
		public FranchiseSetController() : this(new FranchiseSetRepository(), new FeatureRepository())
		{
		}

		public FranchiseSetController(IFranchiseSetRepository franchisesetRepository, IFeatureRepository featureRepository)
		{
			this.franchisesetRepository = franchisesetRepository;
			this.featureRepository = featureRepository;
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
			ViewBag.PossibleFeatures = featureRepository.All;
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

		public ActionResult Edit(int id = 0)
		{
			ViewBag.PossibleFeatures = featureRepository.All;

			FranchiseSet franchiseSet = db.FranchiseSets
			   .Include(i => i.Features)               
			   .Where(i => i.FranchiseSetID == id)
			   .Single();
			PopulateAssignedFeatureData(franchiseSet);
			return PartialView(franchiseSet);
		}

		//
		// POST: /FranchiseSet/Edit/5

		[HttpPost]
		public ActionResult Edit(int id, FormCollection formCollection, string[] selectedFeatures)
		{
			var franchiseSetToUpdate = db.FranchiseSets
				.Include(i => i.Features)
				.Where(i => i.FranchiseSetID == id)
				.Single();

			if (TryUpdateModel(franchiseSetToUpdate, "", null, new string[] { "Features" }))
			{

                try
                {
                    UpdateFranchiseSetFeatures(selectedFeatures, franchiseSetToUpdate);

                    db.Entry(franchiseSetToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    //Log the error (add a variable name after DataException)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
				
			}
			
			PopulateAssignedFeatureData(franchiseSetToUpdate);
			return View(franchiseSetToUpdate);
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



        //*********** PRIVATE METHODS *************8

		private void PopulateAssignedFeatureData(FranchiseSet franchiseSet)
		{
			var allFeatures = db.Features;
			var franchiseSetFranchises = new HashSet<int>(franchiseSet.Features.Select(c => c.FeatureID));                
			var viewModel = new List<AssignedFeatureData>();
			foreach (var feature in allFeatures)
			{
				viewModel.Add(new AssignedFeatureData
				{
					FeatureID = feature.FeatureID,
					Name = feature.FeatureName,
					Assigned = franchiseSetFranchises.Contains(feature.FeatureID)
					
				});
			}
			ViewBag.Features = viewModel;
		}


		private void UpdateFranchiseSetFeatures(string[] selectedFeatures, FranchiseSet franchiseSetToUpdate)
		{
			if (selectedFeatures == null)
			{
				franchiseSetToUpdate.Features = new List<Feature>();                
				return;
			}

			var selectedFeaturesHS = new HashSet<string>(selectedFeatures);
			var franchiseSetFeatures = new HashSet<int>
				(franchiseSetToUpdate.Features.Select(c => c.FeatureID));                
			foreach (var feature in db.Features)
			{
				if (selectedFeaturesHS.Contains(feature.FeatureID.ToString()))
				{
					if (!franchiseSetFeatures.Contains(feature.FeatureID))
					{
						franchiseSetToUpdate.Features.Add(feature);                        
					}
				}
				else
				{
					if (franchiseSetFeatures.Contains(feature.FeatureID))
					{
						franchiseSetToUpdate.Features.Remove(feature);
						
					}
				}
			}
		}






	}
}

