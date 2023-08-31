using Models;

namespace Lesson25.Models
{
    public class IndexViewModel
    {
        public string? RequestId { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
