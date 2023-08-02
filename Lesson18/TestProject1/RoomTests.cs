public class RoomTests
{
    [Fact]
    public void SetLesson_WhenLessonIsSet_ShouldSetLesson()
    {
        // Arrange
        Room room = new Room("Room 101");
        Lesson lesson = new Lesson("Math", new Teacher("John Doe"), room);

        // Act
        room.SetLesson(lesson);

        // Assert
        Assert.Equal(lesson, room.Lesson);
    }
}
