public class Student
{
    public string Name { get; }
    public Group Group { get; private set; }

    public Student(string name)
    {
        Name = name;
    }

    public void SetGroup(Group group)
    {
        Group = group;
    }
}
