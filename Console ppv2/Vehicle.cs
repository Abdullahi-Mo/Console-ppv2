using System;

public abstract class Vehicle
{
    public string RegistrationNumber { get; set; }
    public DateTime CheckInTime { get; private set; }

    // Lägg till abstrakt SpaceRequired-egenskap
    public abstract int SpaceRequired { get; }

    protected Vehicle(string registrationNumber)
    {
        RegistrationNumber = registrationNumber;
        CheckInTime = DateTime.Now;
    }

    public TimeSpan GetParkingDuration()
    {
        return DateTime.Now - CheckInTime;
    }

    public abstract decimal CalculateParkingFee();
}