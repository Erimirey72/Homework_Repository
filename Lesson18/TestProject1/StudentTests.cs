public class StudentTests
{
    [Fact]
    public void SetGroup_WhenGroupIsSet_ShouldSetGroup()
    {
        // Arrange
        Student student = new Student("John Doe");
        Group group = new Group("Group A", new Cathedra("Computer Science"));

        // Act
        student.SetGroup(group);

        // Assert
        Assert.Equal(group, student.Group);
    }
}