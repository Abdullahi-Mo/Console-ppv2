public class Car : Vehicle
{
    public override int SpaceRequired => 4;  // En bil tar 4 utrymmen.

    public Car(string registrationNumber) : base(registrationNumber) { }

    public override decimal CalculateParkingFee()
    {
        // Här kan du lägga till logik för att beräkna parkeringsavgift för bil.
        return 20;  // Exempelpris för parkering.
    }
}