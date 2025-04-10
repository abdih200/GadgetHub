using System.Linq;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using GadgetHub.WebUI.Models;


namespace GadgetHub.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IGadgetRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IGadgetRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }

        // Add to Cart
        public RedirectToRouteResult AddToCart(Cart cart, int gadgetId, string returnUrl)
        {
            var gadget = repository.Gadgets.FirstOrDefault(g => g.GadgetId == gadgetId);
            if (gadget != null)
            {
                cart.AddItem(gadget.GadgetId, gadget.Name, gadget.Price);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        // Remove from Cart
        public RedirectToRouteResult RemoveFromCart(Cart cart, int gadgetId, string returnUrl)
        {
            cart.RemoveItem(gadgetId);
            return RedirectToAction("Index", new { returnUrl });
        }

        // View Cart
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        // Cart Summary (used in layout)
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        // GET: Checkout
        [HttpGet]
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        // POST: Checkout
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed", shippingDetails);
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}

