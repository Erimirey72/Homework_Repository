public class BuyerManager
{
    public List<IBuyer> Buyers { get; set; }

    public BuyerManager()
    {
        Buyers = new List<IBuyer>();
    }

    public void RegisterNewBuyer()
    {
        Console.WriteLine("\nRegistering a new buyer...");

        Console.Write("Enter buyer name: ");
        string name = Console.ReadLine();

        IBuyer buyer = new Buyer
        {
            Name = name
        };

        Buyers.Add(buyer);

        Console.WriteLine("Buyer registered successfully.");
    }

    public IBuyer GetBuyerByName(string name)
    {
        return Buyers.Find(b => b.Name.Equals(name));
    }

    public void ShowListOfBuyers()
    {
        Console.WriteLine("\nList of Buyers:");

        foreach (var buyer in Buyers)
        {
            Console.WriteLine($"Name: {buyer.Name}");
        }
    }
}
