using System.Linq;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
    public class GadgetController : Controller
    {
        private IGadgetRepository repository;
        public int PageSize = 5; 

        public GadgetController(IGadgetRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            var gadgets = repository.Gadgets
                .Where(g => category == null || g.Category == category)
                .OrderBy(g => g.GadgetId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            var model = new GadgetListViewModel
            {
                Gadgets = gadgets,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        repository.Gadgets.Count() :
                        repository.Gadgets.Count(g => g.Category == category)
                }
            };

            ViewBag.CurrentCategory = category;
            return View(model);
        }
    }
}
