public class Vehicle
{
    public string Make { get; set; }
    public string Model { get; set; }
    public List<Part> Parts { get; set; }

    public Vehicle()
    {
        Parts = new List<Part>();
    }

    public void AddPart(Part part)
    {
        Parts.Add(part);
    }

    public void InstallParts()
    {
        Console.WriteLine($"Installing parts for {Make} {Model}");
        foreach (var part in Parts)
        {
            part.Install();
        }
        Console.WriteLine($"Installation completed for {Make} {Model}.");
    }
}
