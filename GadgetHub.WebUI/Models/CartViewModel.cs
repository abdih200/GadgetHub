using GadgetHub.WebUI.Models;
using GadgetHub.Domain.Entities;


namespace GadgetHub.WebUI.Models
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
