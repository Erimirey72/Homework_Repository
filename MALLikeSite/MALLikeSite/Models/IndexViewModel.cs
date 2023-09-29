using Models;

namespace MALLikeSite.Models
{
    public class IndexViewModel
    {
        public string? RequestId { get; set; }

        public IEnumerable<Title> Titles { get; set; }
    }
}
