using Models;

namespace BusinessLogic.Interfaces
{
    public interface ICharacterService
    {
        IEnumerable<Character> GetAll();
        Guid Create(Character character);
        IEnumerable<Character> GetByName(string name);
        Character GetById(Guid id);
        Character Edit(Character character);
        void DeleteById(Guid id);
        public Character Aprove(Character character);
    }
}