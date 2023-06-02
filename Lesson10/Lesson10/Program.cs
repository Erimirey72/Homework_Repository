
class Program
{
    static void Main(string[] args)
    {
        Car car1 = new Car()
        {
            Make = "Shkoda",
            Model = "Oktavia"
        };

        Car car2 = new Car()
        {
            Make = "Honda",
            Model = "Accord",
            NumberOfDoors = 4
        };

        Wheel frontLeftWheel = new Wheel()
        {
            Name = "Front Left Wheel",
            Size = 18,
            TireModel = "All-Season"
        };

        Wheel frontRightWheel = new Wheel()
        {
            Name = "Front Right Wheel",
            Size = 18,
            TireModel = "All-Season"
        };

        Wheel backRightWheel = new Wheel()
        {
            Name = "Back Right Wheel",
            Size = 18,
            TireModel = "All-Season"
        };

        Wheel backLeftWheel = new Wheel()
        {
            Name = "Back Left Wheel",
            Size = 18,
            TireModel = "All-Season"
        };

        Engine engine = new Engine()
        {
            Name = "V6 Engine",
            Horsepower = 300,
            CylinderCount = 6
        };

        Tire frontLeftTire = new Tire()
        {
            Name = "Front Left Tire",
            TireType = "All-Season"
        };

        Tire frontRightTire = new Tire()
        {
            Name = "Front Right Tire",
            TireType = "All-Season"
        };

        Seat driverSeat = new Seat()
        {
            Name = "Driver Seat",
            Material = "Leather"
        };

        car1.AddPart(frontLeftTire);
        car1.AddPart(frontRightTire);
        car1.AddPart(driverSeat);

        car1.InstallParts();

        car2.AddPart(frontLeftWheel);
        car2.AddPart(frontRightWheel);
        car2.AddPart(backRightWheel);
        car2.AddPart(backLeftWheel);
        car2.AddPart(engine);

        car2.InstallParts();
    }
}
