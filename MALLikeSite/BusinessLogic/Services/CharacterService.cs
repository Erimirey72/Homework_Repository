using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BusinessLogic.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ApplicationDbContext _application;

        public CharacterService(ApplicationDbContext application)
        {
            _application = application;
        }
        public Guid Create(Character character)
        {
            _application.Characters.Add(character);

            var result = _application.SaveChanges();

            return character.Id;
        }

        public void DeleteById(Guid id)
        {
            var existingItem = _application.Characters.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                throw new ArgumentException("No such id exists");
            }

            _application.Characters.Remove(existingItem);
            _application.SaveChanges();
        }

        public Character Edit(Character character)
        {
            var existingCharacter = _application.Characters.FirstOrDefault(x => x.Id == character.Id);
            if (existingCharacter == null)
            {
                throw new ArgumentException("No such id exists");
            }

            existingCharacter.Name = character.Name;
            existingCharacter.Description = character.Description;
            existingCharacter.Titles = character.Titles;
            existingCharacter.VoiceActor = character.VoiceActor;
            existingCharacter.IsAproved = false;

            _application.SaveChanges();

            return existingCharacter;
        }

        public Character Aprove(Character character)
        {
            var existingCharacter = _application.Characters.FirstOrDefault(x => x.Id == character.Id);
            if (existingCharacter == null)
            {
                throw new ArgumentException("No such id exists");
            }
            existingCharacter.IsAproved = true;

            _application.SaveChanges();

            return existingCharacter;
        }

        public IEnumerable<Character> GetAll()
        {
            return _application.Characters.ToList();

        }

        public Character GetById(Guid id)
        {
            var result = _application.Characters.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentException("No such id exists");
            }

            return result;
        }

        public IEnumerable<Character> GetByName(string name)
        {
            var baseQuery = _application.Characters.AsNoTracking();
            if (baseQuery == null)
            {
                baseQuery = baseQuery.Where(x => x.Name == name);
            }

            return baseQuery.ToList();
        }
    }
}
