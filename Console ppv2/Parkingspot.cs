public class ParkingSpot
{
    public int SpotNumber { get; private set; }
    public int TotalSpace { get; private set; } // Total space in the parking spot
    public int AvailableSpace { get; private set; } // Available space in the parking spot
    private Vehicle parkedVehicle; // Store the parked vehicle (only one vehicle per spot for simplicity)

    public ParkingSpot(int spotNumber, int spacePerSpot)
    {
        SpotNumber = spotNumber;
        TotalSpace = spacePerSpot;
        AvailableSpace = spacePerSpot;
        parkedVehicle = null;  // Initially, no vehicle is parked
    }

    // Check if the spot is available for parking based on available space
    public bool IsAvailable()
    {
        return parkedVehicle == null; // Spot is available if no vehicle is parked
    }

    // Park a vehicle in the spot
    public bool ParkVehicle(Vehicle vehicle)
    {
        if (AvailableSpace >= vehicle.SpaceRequired && IsAvailable())
        {
            parkedVehicle = vehicle; // Park the vehicle in the spot
            AvailableSpace -= vehicle.SpaceRequired; // Decrease the available space
            return true;  // Parking successful
        }
        return false;  // Not enough space or the spot is already occupied
    }

    // Remove a vehicle from the spot
    public bool RemoveVehicle(Vehicle vehicle)
    {
        if (parkedVehicle != null && parkedVehicle == vehicle)
        {
            parkedVehicle = null; // Remove the vehicle from the spot
            AvailableSpace += vehicle.SpaceRequired; // Free up space
            return true;  // Vehicle removed successfully
        }
        return false;  // Vehicle not found
    }

    // Retrieve the parked vehicle in the spot (if any)
    public Vehicle GetParkedVehicle()
    {
        return parkedVehicle;  // Return the parked vehicle, or null if no vehicle is parked
    }

    // Get status of the parking spot
    public string GetStatus()
    {
        if (parkedVehicle != null)
        {
            return $"Spot {SpotNumber}: Occupied by {parkedVehicle.RegistrationNumber}, Available space {AvailableSpace}";
        }
        else
        {
            return $"Spot {SpotNumber}: Available, Available space {AvailableSpace}";
        }
    }
}
