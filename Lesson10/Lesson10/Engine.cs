public class Engine : Part
{
    public int Horsepower { get; set; }
    public int CylinderCount { get; set; }

    public override void Install()
    {
        Console.WriteLine($"Installing {Name} (Horsepower: {Horsepower}, Cylinders: {CylinderCount})");
    }
}
