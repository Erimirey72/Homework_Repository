class Program
{
    static void Main(string[] args)
    {
        Cathedra cathedra = new Cathedra("Computer Science");
        Group group = new Group("Group A", cathedra);
        Lesson lesson = new Lesson("Math", new Teacher("John Doe"), new Room("Room 101"));
        Student student = new Student("Jean Doe");

        group.AddStudent(student);
        Console.WriteLine($"Student {student.Name} added to {group.Name}");
        group.RemoveStudent(student);
        Console.WriteLine($"Student {student.Name} removed from {group.Name}");
        group.AddLesson(lesson);
        Console.WriteLine($"Lesson {lesson.Name} added to {group.Name}");
        group.RemoveLesson(lesson);
        Console.WriteLine($"Lesson {lesson.Name} removed from {group.Name}");
        lesson.AddGroup(group);
        Console.WriteLine($"Group {group.Name} added to {lesson.Name}");
        lesson.RemoveGroup(group);
        Console.WriteLine($"Group {group.Name} removed from {lesson.Name}");
        cathedra.AddGroup(group);
        Console.WriteLine($"Group {group.Name} added to {cathedra.Name}");
        cathedra.RemoveGroup(group);
        Console.WriteLine($"Group {group.Name} removed from {cathedra.Name}");

        Teacher teacher = new Teacher("Jane Smith");
        cathedra.AddTeacher(teacher);
        Console.WriteLine($"Teacher {teacher.Name} added to {cathedra.Name}");
        cathedra.RemoveTeacher(teacher);
        Console.WriteLine($"Teacher {teacher.Name} removed from {cathedra.Name}");
    }
}

