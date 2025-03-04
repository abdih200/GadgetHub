using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;

namespace GadgetHub.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IGadgetRepository repository;

        public NavController(IGadgetRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            var categories = repository.Gadgets
                .Select(g => g.Category)
                .Distinct()
                .OrderBy(c => c);

            return PartialView(categories);
        }
    }
}