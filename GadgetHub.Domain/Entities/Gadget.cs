namespace GadgetHub.Domain.Entities
{
    public class Gadget
    {
        public int GadgetId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
