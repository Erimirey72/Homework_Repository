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
        public Character Approve(Guid characterId);
        public List<Character> GetApproved();
        public List<Character> GetUnapproved();
    }
}