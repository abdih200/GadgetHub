using System.Linq;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IGadgetRepository repository;

        public AdminController(IGadgetRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View(repository.Gadgets);
        }
        public ViewResult Edit(int gadgetId)
        {
            Gadget gadget = repository.Gadgets.FirstOrDefault(g => g.GadgetId == gadgetId);
            return View(gadget);
        }

        [HttpPost]
        public ActionResult Edit(Gadget gadget)
        {
            if (ModelState.IsValid)
            {
                repository.SaveGadget(gadget);
                TempData["message"] = $"{gadget.Name} has been saved!";
                return RedirectToAction("Index");
            }
            return View(gadget); // If invalid, re-display form
        }
        public ViewResult Create()
        {
            return View("Edit", new Gadget());
        }
        public ActionResult Delete(int gadgetId)
        {
            Gadget deleted = repository.DeleteGadget(gadgetId);
            if (deleted != null)
            {
                TempData["message"] = $"{deleted.Name} was deleted!";
            }
            return RedirectToAction("Index");
        }
    }
}
