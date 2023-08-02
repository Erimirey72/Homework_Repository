public class Group
{
    public string Name { get; }
    public Cathedra Cathedra { get; private set; }
    private Student[] students;
    private Lesson[] lessons;
    private int studentCount;
    private int lessonCount;

    public Group(string name, Cathedra cathedra)
    {
        Name = name;
        Cathedra = cathedra;
        students = new Student[10];
        lessons = new Lesson[10];
        studentCount = 0;
        lessonCount = 0;
    }

    public void SetCathedra(Cathedra cathedra)
    {
        Cathedra = cathedra;
    }

    public void AddStudent(Student student)
    {
        if (studentCount < students.Length && !ArrayContains(students, student))
        {
            students[studentCount] = student;
            student.SetGroup(this);
            studentCount++;
        }
    }

    public void RemoveStudent(Student student)
    {
        if (ArrayContains(students, student))
        {
            int studentIndex = Array.IndexOf(students, student);
            students[studentIndex] = null;
            student.SetGroup(null);
            ShiftArrayElements(students, studentIndex);
            studentCount--;
        }
    }

    public void AddLesson(Lesson lesson)
    {
        if (lessonCount < lessons.Length && !ArrayContains(lessons, lesson))
        {
            lessons[lessonCount] = lesson;
            lesson.AddGroup(this);
            lessonCount++;
        }
    }

    public void RemoveLesson(Lesson lesson)
    {
        if (ArrayContains(lessons, lesson))
        {
            int lessonIndex = Array.IndexOf(lessons, lesson);
            lessons[lessonIndex] = null;
            lesson.RemoveGroup(this);
            ShiftArrayElements(lessons, lessonIndex);
            lessonCount--;
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
