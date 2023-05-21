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
            string choice = Console.ReadLine();

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

    static void AddContact()
    {
        Console.WriteLine("Add Contact");
        Console.Write("First Name: ");
        string firstName = Console.ReadLine();
        if (firstName == "")
        {
            Console.WriteLine("First name cant be empty!");
            return;
        }

        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();
        if (lastName == "")
        {
            Console.WriteLine("Last name cant be empty!");
            return;
        }

        Console.Write("Phone Number: ");
        string phoneNumber = Console.ReadLine();
        if (phoneNumber == "")
        {
            Console.WriteLine("Phone number cant be empty!");
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
        Console.Write("Search Query: ");
        string searchQuery = Console.ReadLine();

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
        Console.Write("Enter ID of the contact to edit: ");
        string idToEdit = Console.ReadLine();

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

            Console.Write("New First Name (leave empty to keep existing): ");
            string newFirstName = Console.ReadLine();

            Console.Write("New Last Name (leave empty to keep existing): ");
            string newLastName = Console.ReadLine();

            Console.Write("New Phone Number (leave empty to keep existing): ");
            string newPhoneNumber = Console.ReadLine();

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
        Console.Write("Enter ID of the contact to delete: ");
        string idToDelete = Console.ReadLine();

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

            Console.Write("Are you sure you want to delete this contact? (Y/N): ");
            string confirmation = Console.ReadLine();

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

        int maxId = 0;
        foreach (var contact in contacts)
        {
            int id = int.Parse(contact.ID);
            if (id > maxId)
            {
                maxId = id;
            }
        }

        return (maxId + 1).ToString("D2");
    }

    static PhoneBookContact[] ReadContactsFromCsv()
    {
        if (!File.Exists(CsvFilePath))
        {
            return new PhoneBookContact[0];
        }

        string[] lines = File.ReadAllLines(CsvFilePath);

        PhoneBookContact[] contacts = new PhoneBookContact[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            PhoneBookContact contact = new PhoneBookContact();
            contact.ID = parts[0];
            contact.FirstName = parts[1];
            contact.LastName = parts[2];
            contact.PhoneNumber = parts[3];
            contacts[i] = contact;
        }

        return contacts;
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
            if (!contact.ID.Equals(contactToRemove.ID, StringComparison.OrdinalIgnoreCase))
            {
                updatedContacts[index] = contact;
                index++;
            }
        }

        return updatedContacts;
    }
}