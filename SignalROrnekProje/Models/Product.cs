using Microsoft.EntityFrameworkCore;

namespace SignalROrnekProje.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        [Precision(18,2)]
        public decimal Price { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; } = default!;
    }
}
