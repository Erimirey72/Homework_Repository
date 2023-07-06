public class Room
{
    public string Name { get; set; }
    public List<Meeting> Meetings { get; set; }

    public Room(string name)
    {
        Name = name;
        Meetings = new List<Meeting>();
    }
}
