using Models;

public class CharacterPageViewModel
{
    public string? RequestId { get; set; }

    public IEnumerable<Character> Characters { get; set; }
}