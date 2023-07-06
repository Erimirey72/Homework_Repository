public class Program
{
    static void Main(string[] args)
    {
        Calendar calendar = new Calendar();
        bool isReadOnly = true;

        while (true)
        {
            Console.WriteLine("Calendar Menu:");
            Console.WriteLine("1. View Rooms");
            Console.WriteLine("2. Book Meeting");
            Console.WriteLine("3. View Booked Meetings for Room");
            Console.WriteLine("4. Toggle Mode (Read-only / Read-Write)");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            Console.Write("Enter your choice: ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        calendar.ViewRooms();
                        break;
                    case 2:
                        if (!isReadOnly)
                        {
                            calendar.BookMeeting();
                        }
                        else
                        {
                            Console.WriteLine("Read-only mode. Cannot book a meeting.");
                        }
                        break;
                    case 3:
                        calendar.ViewBookedMeetingsForRoom();
                        break;
                    case 4:
                        isReadOnly = !isReadOnly;
                        Console.WriteLine($"Mode switched to {(isReadOnly ? "Read-only" : "Read-Write")}.");
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a valid integer.");
            }
        }
    }
}
