public class Part
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public decimal Price { get; set; }

    public virtual void Install()
    {
        Console.WriteLine($"Installing {Name}");
    }
}
