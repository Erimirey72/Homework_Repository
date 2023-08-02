public class Cathedra
{
    public string Name { get; }
    private Group[] groups;
    private Teacher[] teachers;
    private int groupCount;
    private int teacherCount;

    public Cathedra(string name)
    {
        Name = name;
        groups = new Group[10];
        teachers = new Teacher[10];
        groupCount = 0;
        teacherCount = 0;
    }

    public void AddGroup(Group group)
    {
        if (groupCount < groups.Length && !ArrayContains(groups, group))
        {
            groups[groupCount] = group;
            group.SetCathedra(this);
            groupCount++;
        }
    }

    public void RemoveGroup(Group group)
    {
        if (ArrayContains(groups, group))
        {
            int groupIndex = Array.IndexOf(groups, group);
            groups[groupIndex] = null;
            group.SetCathedra(null);
            ShiftArrayElements(groups, groupIndex);
            groupCount--;
        }
    }

    public void AddTeacher(Teacher teacher)
    {
        if (teacherCount < teachers.Length && !ArrayContains(teachers, teacher))
        {
            teachers[teacherCount] = teacher;
            teacher.SetCathedra(this);
            teacherCount++;
        }
    }

    public void RemoveTeacher(Teacher teacher)
    {
        if (ArrayContains(teachers, teacher))
        {
            int teacherIndex = Array.IndexOf(teachers, teacher);
            teachers[teacherIndex] = null;
            teacher.SetCathedra(null);
            ShiftArrayElements(teachers, teacherIndex);
            teacherCount--;
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

