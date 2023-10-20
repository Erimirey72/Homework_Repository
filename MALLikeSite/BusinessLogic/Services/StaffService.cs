using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BusinessLogic.Services
{
    public class StaffService : IStaffService
    {
        private readonly ApplicationDbContext _application;

        public StaffService(ApplicationDbContext application)
        {
            _application = application;
        }
        public Guid Create(Staff staff)
        {
            _application.Staffs.Add(staff);

            var result = _application.SaveChanges();

            return staff.Id;
        }

        public void DeleteById(Guid id)
        {
            var existingItem = _application.Staffs.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                throw new ArgumentException("No such id exists");
            }

            _application.Staffs.Remove(existingItem);
            _application.SaveChanges();
        }

        public Staff Edit(Staff staff)
        {
            var existingStaff = _application.Staffs.FirstOrDefault(x => x.Id == staff.Id);
            if (existingStaff == null)
            {
                throw new ArgumentException("No such id exists");
            }

            existingStaff.Name = staff.Name;
            existingStaff.Description = staff.Description;
            existingStaff.Titles = staff.Titles;
            existingStaff.IsApproved = false;

            _application.SaveChanges();

            return existingStaff;
        }

        public Staff Approve(Guid staffId)
        {
            var existingStaff = _application.Staffs.FirstOrDefault(x => x.Id == staffId);
            if (existingStaff == null)
            {
                throw new ArgumentException("No such id exists");
            }
            existingStaff.IsApproved = true;

            _application.SaveChanges();

            return existingStaff;
        }

        public IEnumerable<Staff> GetAll()
        {
            return _application.Staffs.ToList();
        }

        public Staff GetById(Guid id)
        {
            var result = _application.Staffs.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentException("No such id exists");
            }

            return result;
        }

        public IEnumerable<Staff> GetByName(string name)
        {
            var baseQuery = _application.Staffs.AsNoTracking();
            if (baseQuery == null)
            {
                baseQuery = baseQuery.Where(x => x.Name == name);
            }

            return baseQuery.ToList();
        }
        public List<Staff> GetApproved()
        {
            return _application.Staffs.Where(t => t.IsApproved).ToList();
        }
        public List<Staff> GetUnapproved()
        {
            return _application.Staffs.Where(t => t.IsApproved == false).ToList();
        }
    }
}
