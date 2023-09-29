using Models;
using System.Security.Policy;

public class CharacterViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Title> Titles { get; set; }
    public List<Staff> VoiceActor { get; set; }
    public string Description { get; set; }

    public bool IsAproved;
}