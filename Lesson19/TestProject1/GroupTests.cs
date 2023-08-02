public class GroupTests
{
    [Fact]
    public void AddStudent_WhenStudentIsAdded_ShouldSetGroup()
    {
        // Arrange
        Cathedra cathedra = new Cathedra("Computer Science");
        Group group = new Group("Group A", cathedra);
        Student student = new Student("John Doe");

        // Act
        group.AddStudent(student);

        // Assert
        Assert.Equal(group, student.Group);
    }

    [Fact]
    public void RemoveStudent_WhenStudentIsRemoved_ShouldResetGroup()
    {
        // Arrange
        Cathedra cathedra = new Cathedra("Computer Science");
        Group group = new Group("Group A", cathedra);
        Student student = new Student("John Doe");

        // Act
        group.AddStudent(student);
        group.RemoveStudent(student);

        // Assert
        Assert.Null(student.Group);
    }
}

