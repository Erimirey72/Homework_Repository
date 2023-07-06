public class Calendar : ICalendar
{
    private List<Room> rooms;

    public Calendar()
    {
        rooms = new List<Room>();

        for (int i = 1; i <= 10; i++)
        {
            rooms.Add(new Room($"Room {i}"));
        }
    }

    public void ViewRooms()
    {
        Console.WriteLine("Rooms:");

        foreach (var room in rooms)
        {
            Console.WriteLine($"Room Number: {room.Name}");
        }
    }

    public void BookMeeting()
    {
        Console.Write("Enter the room number: ");
        string roomName = Console.ReadLine();

        Room room = rooms.Find(r => r.Name == roomName);

        if (room != null)
        {
            Console.Write("Enter the meeting title: ");
            string title = Console.ReadLine();

            Console.Write("Enter the meeting start time (yyyy-MM-dd HH:mm): ");
            DateTime startTime;
            if (DateTime.TryParse(Console.ReadLine(), out startTime))
            {
                Console.Write("Enter the meeting end time (yyyy-MM-dd HH:mm): ");
                DateTime endTime;
                if (DateTime.TryParse(Console.ReadLine(), out endTime))
                {
                    room.Meetings.Add(new Meeting(title, startTime, endTime));
                    Console.WriteLine("Meeting booked successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid end time format.");
                }
            }
            else
            {
                Console.WriteLine("Invalid start time format.");
            }
        }
        else
        {
            Console.WriteLine("Room not found.");
        }
    }

    public void ViewBookedMeetingsForRoom()
    {
        Console.Write("Enter the room number: ");
        string roomName = Console.ReadLine();

        Room room = rooms.Find(r => r.Name == roomName);

        if (room != null)
        {
            Console.WriteLine($"Booked Meetings for Room {roomName}:");
            foreach (var meeting in room.Meetings)
            {
                Console.WriteLine($"Title: {meeting.Title}");
                Console.WriteLine($"Start Time: {meeting.StartTime}");
                Console.WriteLine($"End Time: {meeting.EndTime}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Room not found.");
        }
    }
}
