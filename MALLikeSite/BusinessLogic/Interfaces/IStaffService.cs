using Models;

namespace BusinessLogic.Interfaces
{
    public interface IStaffService
    {
        IEnumerable<Staff> GetAll();
        Guid Create(Staff staff);
        IEnumerable<Staff> GetByName(string name);
        Staff GetById(Guid id);
        Staff Edit(Staff staff);
        void DeleteById(Guid id);
        public Staff Approve(Guid staffId);
        public List<Staff> GetApproved();
        public List<Staff> GetUnapproved();
    }
}