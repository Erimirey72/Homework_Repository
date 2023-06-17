public class Library
{
    private Book[] books;
    private Member[] members;
    private Librarian[] librarians;

    public Library()
    {
        books = new Book[0];
        members = new Member[0];
        librarians = new Librarian[0];
    }

    public void HireNewLibrarian()
    {
        Console.WriteLine("New librarian hired successfully.");
    }

    public void FireLibrarian()
    {
        Console.WriteLine("Librarian fired successfully.");
    }
}
