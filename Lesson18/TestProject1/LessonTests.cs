using Xunit;
using Moq;

public class LessonTests
{
    [Fact]
    public void SetTeacher_Should_Set_Teacher_Property()
    {
        // Arrange
        var teacherMock = new Mock<Teacher>("John Doe");
        var roomMock = new Mock<Room>("Room 101");
        var lesson = new Lesson("Math", teacherMock.Object, roomMock.Object);

        var newTeacherMock = new Mock<Teacher>("Jane Smith");

        // Act
        lesson.SetTeacher(newTeacherMock.Object);

        // Assert
        Assert.Equal(newTeacherMock.Object, lesson.Teacher);
    }

    [Fact]
    public void SetRoom_Should_Set_Room_Property()
    {
        // Arrange
        var teacherMock = new Mock<Teacher>("John Doe");
        var roomMock = new Mock<Room>("Room 101");
        var lesson = new Lesson("Math", teacherMock.Object, roomMock.Object);

        var newRoomMock = new Mock<Room>("Room 102");

        // Act
        lesson.SetRoom(newRoomMock.Object);

        // Assert
        Assert.Equal(newRoomMock.Object, lesson.Room);
    }
}
