public class CathedraTests
{
    [Fact]
    public void AddGroup_WhenGroupIsAdded_ShouldSetCathedra()
    {
        // Arrange
        Cathedra cathedra = new Cathedra("Computer Science");
        Group group = new Group("Group A", cathedra);

        // Act
        cathedra.AddGroup(group);

        // Assert
        Assert.Equal(cathedra, group.Cathedra);
    }

    [Fact]
    public void RemoveGroup_WhenGroupIsRemoved_ShouldResetCathedra()
    {
        // Arrange
        Cathedra cathedra = new Cathedra("Computer Science");
        Group group = new Group("Group A", cathedra);

        // Act
        cathedra.AddGroup(group);
        cathedra.RemoveGroup(group);

        // Assert
        Assert.Null(group.Cathedra);
    }

    [Fact]
    public void AddTeacher_WhenTeacherIsAdded_ShouldSetCathedra()
    {
        // Arrange
        Cathedra cathedra = new Cathedra("Computer Science");
        Teacher teacher = new Teacher("John Doe");

        // Act
        cathedra.AddTeacher(teacher);

        // Assert
        Assert.Equal(cathedra, teacher.Cathedra);
    }

    [Fact]
    public void RemoveTeacher_WhenTeacherIsRemoved_ShouldResetCathedra()
    {
        // Arrange
        Cathedra cathedra = new Cathedra("Computer Science");
        Teacher teacher = new Teacher("John Doe");

        // Act
        cathedra.AddTeacher(teacher);
        cathedra.RemoveTeacher(teacher);

        // Assert
        Assert.Null(teacher.Cathedra);
    }
}

