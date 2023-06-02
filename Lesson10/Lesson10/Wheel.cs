public class Wheel : Part
{
    public int Size { get; set; }
    public string TireModel { get; set; }

    public override void Install()
    {
        Console.WriteLine($"Installing {Name} (Size: {Size}, Tire Model: {TireModel})");
    }
}
