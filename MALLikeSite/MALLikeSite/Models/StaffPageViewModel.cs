using Models;

namespace MALLikeSite.Models
{
    public class StaffPageViewModel
    {
        public string? RequestId { get; set; }

        public IEnumerable<Staff> Staffs { get; set; }
    }
}
