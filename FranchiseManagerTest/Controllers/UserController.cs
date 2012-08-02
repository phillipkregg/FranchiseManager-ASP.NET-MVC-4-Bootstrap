using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FranchiseManagerTest.Models;

namespace FranchiseManagerTest.Controllers
{   
    public class UserController : Controller
    {
		private readonly IFranchiseSetRepository franchisesetRepository;
		private readonly IUserRepository userRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public UserController() : this(new FranchiseSetRepository(), new UserRepository())
        {
        }

        public UserController(IFranchiseSetRepository franchisesetRepository, IUserRepository userRepository)
        {
			this.franchisesetRepository = franchisesetRepository;
			this.userRepository = userRepository;
        }

        //
        // GET: /User/

        public ViewResult Index()
        {
            return View(userRepository.AllIncluding(user => user.FranchiseSet));
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(int id)
        {
            return View(userRepository.Find(id));
        }

        //
        // GET: /User/Create

        public PartialViewResult Create(int FranchiseSetId)
        {
			ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
            var user = new User { FranchiseSetId = FranchiseSetId };
            return PartialView(user);
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index", "FranchiseSet", new { id = user.FranchiseSetId } );
            } else {
				ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
				return View();
			}
        }
        
        //
        // GET: /User/Edit/5
 
        public PartialViewResult Edit(int id)
        {
			ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
            var user = userRepository.Find(id);
             return PartialView(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index", "FranchiseSet", new { id = user.FranchiseSetId } );
            } else {
				ViewBag.PossibleFranchiseSets = franchisesetRepository.All;
				return View();
			}
        }

        //
        // GET: /User/Delete/5
 
        public PartialViewResult Delete(int id)
        {
            return PartialView(userRepository.Find(id));
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            userRepository.Delete(id);
            userRepository.Save();

            return RedirectToAction("Index", "FranchiseSet" );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                franchisesetRepository.Dispose();
                userRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

