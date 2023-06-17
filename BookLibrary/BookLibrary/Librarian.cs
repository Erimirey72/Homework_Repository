public class Librarian
{
    public string Name { get; set; }
    public int ID { get; set; }
    public string Department { get; set; }

    public void AddBook()
    {
        Console.WriteLine("Book added successfully.");
    }

    public void RemoveBook()
    {
        Console.WriteLine("Book removed successfully.");
    }

    public void SearchBook()
    {
        Console.WriteLine("Book found.");
    }

    public void AddNewMember()
    {
        Console.WriteLine("New member added successfully.");
    }

    public void DeleteMember()
    {
        Console.WriteLine("Member deleted successfully.");
    }

    public void SearchMember()
    {
        Console.WriteLine("Member found.");
    }
}
