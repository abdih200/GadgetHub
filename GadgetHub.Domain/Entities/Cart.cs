using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GadgetHub.Domain.Entities;


namespace GadgetHub.Domain.Entities
{
    public class Cart
    {
        private List<CartItem> lineCollection = new List<CartItem>();

        public void AddItem(int gadgetId, string name, decimal price, int quantity = 1)
        {
            var item = lineCollection.FirstOrDefault(g => g.GadgetId == gadgetId);
            if (item == null)
            {
                lineCollection.Add(new CartItem
                {
                    GadgetId = gadgetId,
                    GadgetName = name,
                    Price = price,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItem(int gadgetId)
        {
            lineCollection.RemoveAll(l => l.GadgetId == gadgetId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartItem> Items => lineCollection;
    }

    public class CartItem
    {
        public int GadgetId { get; set; }
        public string GadgetName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } 
    }
}

