public class TeacherTests
{
    [Fact]
    public void SetCathedra_WhenCathedraIsSet_ShouldSetCathedra()
    {
        // Arrange
        Teacher teacher = new Teacher("John Doe");
        Cathedra cathedra = new Cathedra("Computer Science");

        // Act
        teacher.SetCathedra(cathedra);

        // Assert
        Assert.Equal(cathedra, teacher.Cathedra);
    }
}
