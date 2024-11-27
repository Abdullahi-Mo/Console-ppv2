public class MC : Vehicle
{
    public override int SpaceRequired => 2;  // En motorcykel tar 2 utrymmen (kan vara upp till 2 i en ruta).

    public MC(string registrationNumber) : base(registrationNumber) { }

    public override decimal CalculateParkingFee()
    {
        // Här kan du lägga till logik för att beräkna parkeringsavgift för motorcykel.
        return 10;  // Exempelpris för parkering.
    }
}