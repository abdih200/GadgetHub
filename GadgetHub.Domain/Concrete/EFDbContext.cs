using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GadgetHub.Domain.Entities;
using GadgetHub.Domain.Abstract;

namespace GadgetHub.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        // ✅ Add this constructor to apply the custom DB initializer with seed data
        public EFDbContext()
        {
            // This will recreate the database if the model changes AND seed initial gadgets
            Database.SetInitializer(new GadgetDbInitializer());
        }

        public DbSet<Gadget> Gadgets { get; set; }
    }
}
