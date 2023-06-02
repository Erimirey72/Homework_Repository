public class Tire : Part
{
    public string TireType { get; set; }

    public override void Install()
    {
        Console.WriteLine($"Installing {Name} (Tire Type: {TireType})");
    }
}
