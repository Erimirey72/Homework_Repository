using System;
using System.IO;

class Program
{
    private const string CsvFilePath = "phonebook.csv";

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Phone Book");
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. Search Contact");
            Console.WriteLine("3. Edit Contact");
            Console.WriteLine("4. Delete Contact");
            Console.WriteLine("5. Show All Contacts");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            string choice;

            try
            {
                choice = Console.ReadLine();
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the input. Please try again.");
                Console.WriteLine($"Error: {e.Message}");
                continue;
            }

            switch (choice)
            {
                case "1":
                    AddContact();
                    break;
                case "2":
                    SearchContact();
                    break;
                case "3":
                    EditContact();
                    break;
                case "4":
                    DeleteContact();
                    break;
                case "5":
                    ShowAllContacts();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
    static void RaiseError(string error)
    {
        throw new Exception(error);
    }

    static void AddContact()
    {
        Console.WriteLine("Add Contact");

        string firstName;
        string lastName;
        string phoneNumber;

        Console.Write("First Name: ");
        firstName = Console.ReadLine();
        if (firstName == "")
        {
            Console.WriteLine("First name cant be empty!");
            return;
        }
        if (firstName.Length > 20)
        {
            Console.WriteLine("First name too long!");
            return;
        }

        Console.Write("Last Name: ");
        lastName = Console.ReadLine();
        if (lastName == "")
        {
            Console.WriteLine("Last name cant be empty!");
            return;
        }
        if (lastName.Length > 30)
        {
            Console.WriteLine("Last name too long!");
            return;
        }

        Console.Write("Phone Number (without country code): ");
        phoneNumber = Console.ReadLine();
        if (phoneNumber == "")
        {
            Console.WriteLine("Phone number cant be empty!");
            return;
        }
        if (phoneNumber.Length > 10)
        {
            Console.WriteLine("Phone number too long!");
            return;
        }

        if (ContactExists(firstName, lastName, phoneNumber))
        {
            Console.WriteLine("Contact already exists.");
            return;
        }

        string id = GetNewContactID();

        PhoneBookContact contact = new PhoneBookContact();
        contact.FirstName = firstName;
        contact.LastName = lastName;
        contact.PhoneNumber = phoneNumber;
        contact.ID = id;

        WriteContactToCsv(contact);
        Console.WriteLine("Contact added successfully.");
    }

    static void SearchContact()
    {
        Console.WriteLine("Search Contact");
        string searchQuery;

        try
        {
            Console.Write("Search Query: ");
            searchQuery = Console.ReadLine();
        }
        catch (IOException e)
        {
            RaiseError("Field cant be empty!");
            return;
        }

        PhoneBookContact[] contacts = ReadContactsFromCsv();

        PhoneBookContact[] searchResults = SearchContacts(contacts, searchQuery);

        if (searchResults.Length > 0)
        {
            Console.WriteLine("Search Results:");
            ShowContacts(searchResults);
        }
        else
        {
            Console.WriteLine("No matching contacts found.");
        }
    }

    static void EditContact()
    {
        Console.WriteLine("Edit Contact");
        string idToEdit;

        try
        {
            Console.Write("Enter ID of the contact to edit: ");
            idToEdit = Console.ReadLine();
        }
        catch (IOException e)
        {
            RaiseError("Field cant be empty!");
            return;
        }

        PhoneBookContact[] contacts = ReadContactsFromCsv();

        PhoneBookContact contactToEdit = null;
        foreach (var contact in contacts)
        {
            if (contact.ID.Equals(idToEdit, StringComparison.OrdinalIgnoreCase))
            {
                contactToEdit = contact;
                break;
            }
        }

        if (contactToEdit != null)
        {
            Console.WriteLine("Contact Details:");
            Console.WriteLine($"ID: {contactToEdit.ID}, Name: {contactToEdit.FirstName} {contactToEdit.LastName}, Phone: {contactToEdit.PhoneNumber}");

            string newFirstName;
            string newLastName;
            string newPhoneNumber;

            Console.Write("New First Name (Leave empty to keep current): ");
            newFirstName = Console.ReadLine();

            Console.Write("New Last Name (Leave empty to keep current): ");
            newLastName = Console.ReadLine();

            Console.Write("New Phone Number (Leave empty to keep current): ");
            newPhoneNumber = Console.ReadLine();

            if (!string.IsNullOrEmpty(newFirstName))
            {
                contactToEdit.FirstName = newFirstName;
            }

            if (!string.IsNullOrEmpty(newLastName))
            {
                contactToEdit.LastName = newLastName;
            }

            if (!string.IsNullOrEmpty(newPhoneNumber))
            {
                contactToEdit.PhoneNumber = newPhoneNumber;
            }

            UpdateContactInCsv(contacts);

            Console.WriteLine("Contact updated successfully.");
        }
        else
        {
            Console.WriteLine("Contact not found.");
        }
    }

    static void DeleteContact()
    {
        Console.WriteLine("Delete Contact");
        string idToDelete;

        try
        {
            Console.Write("Enter ID of the contact to delete: ");
            idToDelete = Console.ReadLine();
        }
        catch (IOException e)
        {
            RaiseError("Field cant be empty!");
            return;
        }

        PhoneBookContact[] contacts = ReadContactsFromCsv();

        PhoneBookContact contactToDelete = null;
        foreach (var contact in contacts)
        {
            if (contact.ID.Equals(idToDelete, StringComparison.OrdinalIgnoreCase))
            {
                contactToDelete = contact;
                break;
            }
        }

        if (contactToDelete != null)
        {
            Console.WriteLine("Contact Details:");
            Console.WriteLine($"ID: {contactToDelete.ID}, Name: {contactToDelete.FirstName} {contactToDelete.LastName}, Phone: {contactToDelete.PhoneNumber}");

            string confirmation;

            try
            {
                Console.Write("Are you sure you want to delete this contact? (Y/N): ");
                confirmation = Console.ReadLine();
            }
            catch (IOException e)
            {
                RaiseError("Use only Y or N!");
                return;
            }

            if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                contacts = RemoveContactFromArray(contacts, contactToDelete);
                UpdateContactInCsv(contacts);
                Console.WriteLine("Contact deleted successfully.");
            }
        }
        else
        {
            Console.WriteLine("Contact not found.");
        }
    }

    static void ShowAllContacts()
    {
        Console.WriteLine("All Contacts:");
        PhoneBookContact[] contacts = ReadContactsFromCsv();
        ShowContacts(contacts);
    }

    static void ShowContacts(PhoneBookContact[] contacts)
    {
        Console.WriteLine("ID\tFirst Name\tLast Name\tPhone Number");
        foreach (var contact in contacts)
        {
            Console.WriteLine($"{contact.ID}\t{contact.FirstName}\t\t{contact.LastName}\t\t{contact.PhoneNumber}");
        }
    }

    static bool ContactExists(string firstName, string lastName, string phoneNumber)
    {
        PhoneBookContact[] contacts = ReadContactsFromCsv();

        foreach (var contact in contacts)
        {
            if (contact.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                contact.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) &&
                contact.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    static string GetNewContactID()
    {
        PhoneBookContact[] contacts = ReadContactsFromCsv();

        if (contacts.Length > 0)
        {
            int maxID = 0;

            foreach (var contact in contacts)
            {
                int id;
                if (int.TryParse(contact.ID, out id))
                {
                    if (id > maxID)
                    {
                        maxID = id;
                    }
                }
            }

            return (maxID + 1).ToString("D2");
        }
        else
        {
            return "01";
        }
    }

    static PhoneBookContact[] ReadContactsFromCsv()
    {
        if (File.Exists(CsvFilePath))
        {
            string[] lines = File.ReadAllLines(CsvFilePath);
            PhoneBookContact[] contacts = new PhoneBookContact[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');

                PhoneBookContact contact = new PhoneBookContact();
                contact.ID = fields[0];
                contact.FirstName = fields[1];
                contact.LastName = fields[2];
                contact.PhoneNumber = fields[3];

                contacts[i] = contact;
            }

            return contacts;
        }
        else
        {
            return new PhoneBookContact[0];
        }
    }

    static void WriteContactToCsv(PhoneBookContact contact)
    {
        string line = $"{contact.ID},{contact.FirstName},{contact.LastName},{contact.PhoneNumber}";

        if (!File.Exists(CsvFilePath))
        {
            using (StreamWriter writer = new StreamWriter(CsvFilePath))
            {
                writer.WriteLine(line);
            }
        }
        else
        {
            string[] lines = File.ReadAllLines(CsvFilePath);
            Array.Resize(ref lines, lines.Length + 1);
            lines[lines.Length - 1] = line;
            Array.Sort(lines);
            File.WriteAllLines(CsvFilePath, lines);
        }
    }

    static void UpdateContactInCsv(PhoneBookContact[] contacts)
    {
        string[] lines = new string[contacts.Length];
        for (int i = 0; i < contacts.Length; i++)
        {
            lines[i] = $"{contacts[i].ID},{contacts[i].FirstName},{contacts[i].LastName},{contacts[i].PhoneNumber}";
        }

        File.WriteAllLines(CsvFilePath, lines);
    }

    static PhoneBookContact[] SearchContacts(PhoneBookContact[] contacts, string searchQuery)
    {
        searchQuery = searchQuery.ToLower();

        PhoneBookContact[] searchResults = new PhoneBookContact[0];

        foreach (var contact in contacts)
        {
            if (contact.FirstName.ToLower().Contains(searchQuery) ||
                contact.LastName.ToLower().Contains(searchQuery) ||
                contact.PhoneNumber.ToLower().Contains(searchQuery))
            {
                Array.Resize(ref searchResults, searchResults.Length + 1);
                searchResults[searchResults.Length - 1] = contact;
            }
        }

        return searchResults;
    }

    static PhoneBookContact[] RemoveContactFromArray(PhoneBookContact[] contacts, PhoneBookContact contactToRemove)
    {
        PhoneBookContact[] updatedContacts = new PhoneBookContact[contacts.Length - 1];

        int index = 0;
        foreach (var contact in contacts)
        {
            if (!contact.Equals(contactToRemove))
            {
                updatedContacts[index] = contact;
                index++;
            }
        }

        return updatedContacts;
    }
}
