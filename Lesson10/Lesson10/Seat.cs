public class Seat : Part
{
    public string Material { get; set; }

    public override void Install()
    {
        Console.WriteLine($"Installing {Name} (Material: {Material})");
    }
}