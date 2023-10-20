using BusinessLogic.Interfaces;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Globalization;
using System.Xml.Linq;
using BusinessLogic;

namespace BusinessLogic.Services
{
    public class TitleService : ITitleService
    {
        private readonly ApplicationDbContext _application;

        public TitleService(ApplicationDbContext application)
        {
            _application = application;
        }
        public Guid Create(Title title)
        {
            _application.Titles.Add(title);

            _application.SaveChanges();

            return title.Id;
        }

        public void DeleteById(Guid id)
        {
            var existingItem = _application.Titles.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                throw new ArgumentException("No such id exists");
            }

            _application.Titles.Remove(existingItem);
            _application.SaveChanges();
        }

        public Title Edit(Title title)
        {
            var existingTitle = _application.Titles.FirstOrDefault(x => x.Id == title.Id);
            if (existingTitle == null)
            {
                throw new ArgumentException("No such id exists");
            }

            existingTitle.Name = title.Name;
            existingTitle.Genre = title.Genre;
            existingTitle.Description = title.Description;
            existingTitle.ReleaseDate = title.ReleaseDate;
            existingTitle.Staffs = title.Staffs;
            existingTitle.Characters = title.Characters;
            existingTitle.IsApproved = false;

            _application.SaveChanges();

            return existingTitle;
        }

        public Title Approve(Guid titleId)
        {
            var existingTitle = _application.Titles.FirstOrDefault(x => x.Id == titleId);
            if (existingTitle == null)
            {
                throw new ArgumentException("No such id exists");
            }
            existingTitle.IsApproved = true;

            _application.SaveChanges();

            return existingTitle;
        }

        public Title Vote(Title title)
        {

            var existingTitle = _application.Titles.FirstOrDefault(x => x.Id == title.Id);
            if (existingTitle == null)
            {
                throw new ArgumentException("No such id exists");
            }
            existingTitle.UserRating = title.UserRating;

            _application.SaveChanges();

            return existingTitle;
        }

        public IEnumerable<Title> GetAll()
        {
            return _application.Titles.ToList();

        }

        public IEnumerable<Title> GetByGenre(string genre)
        {
            var baseQuery = _application.Titles.AsNoTracking();
            if (baseQuery == null)
            {
                baseQuery = baseQuery.Where(x => x.Genre == genre);
            }

            return baseQuery.ToList();
        }

        public Title GetById(Guid id)
        {
            var result = _application.Titles.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentException("No such id exists");
            }

            return result;
        }

        public IEnumerable<Title> GetByName(string name)
        {
            return _application.Titles.Where(x => x.Name == name).AsNoTracking().ToList();
        }

        public IEnumerable<Title> GetByReleaseDate(DateTime releaseDate)
        {
            var baseQuery = _application.Titles.AsNoTracking();
            if (baseQuery == null)
            {
                baseQuery = baseQuery.Where(x => x.ReleaseDate == releaseDate);
            }

            return baseQuery.ToList();
        }
        public List<Title> GetApproved()
        {
            return _application.Titles.Where(t => t.IsApproved).ToList();
        }
        public List<Title> GetUnapproved()
        {
            return _application.Titles.Where(t => t.IsApproved == false).ToList();
        }
    }
}
