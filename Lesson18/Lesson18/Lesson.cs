public class Lesson
{
    public string Name { get; }
    private Group[] groups;
    public Teacher Teacher { get; private set; }
    public Room Room { get; private set; }
    private int groupCount;

    public Lesson(string name, Teacher teacher, Room room)
    {
        Name = name;
        groups = new Group[10];
        Teacher = teacher;
        Room = room;
        groupCount = 0;
    }

    public void SetTeacher(Teacher teacher)
    {
        Teacher = teacher;
    }

    public void SetRoom(Room room)
    {
        Room = room;
    }

    public void AddGroup(Group group)
    {
        if (groupCount < groups.Length && !ArrayContains(groups, group))
        {
            groups[groupCount] = group;
            groupCount++;
        }
    }

    public void RemoveGroup(Group group)
    {
        if (ArrayContains(groups, group))
        {
            int groupIndex = Array.IndexOf(groups, group);
            groups[groupIndex] = null;
            ShiftArrayElements(groups, groupIndex);
            groupCount--;
        }
    }

    private bool ArrayContains<T>(T[] array, T item)
    {
        foreach (var element in array)
        {
            if (element != null && element.Equals(item))
                return true;
        }
        return false;
    }

    private void ShiftArrayElements<T>(T[] array, int startIndex)
    {
        for (int i = startIndex; i < array.Length - 1; i++)
        {
            array[i] = array[i + 1];
        }
        array[array.Length - 1] = default(T);
    }
}