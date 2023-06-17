public class Member
{
    public string Name { get; set; }
    public int ID { get; set; }

    public void BorrowBook()
    {
        Console.WriteLine("Book borrowed successfully.");
    }

    public void ReturnBook()
    {
        Console.WriteLine("Book returned successfully.");
    }

    public void ViewBorrowingHistory()
    {
        Console.WriteLine("Borrowing history retrieved.");
    }

    public void CheckBookStatus()
    {
        Console.WriteLine("Book status checked.");
    }
}
