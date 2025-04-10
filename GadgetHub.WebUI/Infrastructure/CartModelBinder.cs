using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.WebUI.Models;
using GadgetHub.Domain.Entities;



namespace GadgetHub.WebUI.Infrastructure
{
    public class CartModelBinder : IModelBinder
    {
        private const string SessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Retrieve cart from session
            Cart cart = (Cart)controllerContext.HttpContext.Session[SessionKey];

            // If no cart exists in session, create a new one
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[SessionKey] = cart;
            }

            return cart;
        }
    }
}
