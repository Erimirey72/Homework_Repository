﻿using Models;

namespace BusinessLogic.Interfaces
{
    public interface ITitleService
    {
        IEnumerable<Title> GetAll();
        Guid Create(Title title);
        IEnumerable<Title> GetByReleaseDate(DateTime releaseDate);
        IEnumerable<Title> GetByName(string name);
        IEnumerable<Title> GetByGenre(string genre);
        Title GetById(Guid id);
        Title Edit(Title title);
        void DeleteById(Guid id);
        public Title Aprove(Title title);
        public Title Vote(Title title);
    }
}