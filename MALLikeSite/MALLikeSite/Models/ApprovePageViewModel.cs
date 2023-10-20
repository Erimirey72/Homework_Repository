using Models;

namespace MALLikeSite.Models
{
    public class ApprovePageViewModel
    {
        public string? RequestId { get; set; }

        public List<Title> Item1 { get; set; }
        public List<Staff> Item2 { get; set; }
        public List<Character> Item3 { get; set; }
    }
}
