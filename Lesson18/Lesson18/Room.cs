public class Room
{
    public string Name { get; }
    public Lesson Lesson { get; private set; }

    public Room(string name)
    {
        Name = name;
    }

    public void SetLesson(Lesson lesson)
    {
        Lesson = lesson;
    }
}
