using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.Concrete
{
    public class GadgetDbInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            var gadgets = new List<Gadget>
            {
                new Gadget { Name = "iPhone 15", Brand = "Apple", Category = "Smartphones", Price = 999, Description = "Latest Apple smartphone" },
                new Gadget { Name = "Galaxy S23", Brand = "Samsung", Category = "Smartphones", Price = 899, Description = "Flagship Samsung phone" },
                new Gadget { Name = "Pixel 7", Brand = "Google", Category = "Smartphones", Price = 799, Description = "Google’s top device" },
                new Gadget { Name = "MacBook Air", Brand = "Apple", Category = "Laptops", Price = 1099, Description = "Light and powerful laptop" },
                new Gadget { Name = "Galaxy Watch 6", Brand = "Samsung", Category = "Wearables", Price = 299, Description = "Smartwatch by Samsung" }
            };

            gadgets.ForEach(g => context.Gadgets.Add(g));
            context.SaveChanges();
            {
                System.Diagnostics.Debug.WriteLine("SEEDING GADGETS...");
            }
        }
    }
}
