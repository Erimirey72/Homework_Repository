using System;

public class Program
{
    public static void Main(string[] args)
    {
        Library library = new Library();
        Librarian librarian = new Librarian();
        Member member = new Member();

        librarian.AddBook();
        librarian.RemoveBook();
        librarian.SearchBook();
        librarian.AddNewMember();
        librarian.DeleteMember();
        librarian.SearchMember();

        library.HireNewLibrarian();
        library.FireLibrarian();

        member.BorrowBook();
        member.ReturnBook();
        member.ViewBorrowingHistory();
        member.CheckBookStatus();
    }
}
