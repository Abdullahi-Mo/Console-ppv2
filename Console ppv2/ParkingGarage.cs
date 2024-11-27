public class ParkingGarage
{
    public List<ParkingSpot> Spots { get; private set; }

    // Den andra konstruktorn initialiserar rutor med storlekar.
    public ParkingGarage(int numberOfSpaces, int spacePerSpot)
    {
        Spots = new List<ParkingSpot>();
        for (int i = 0; i < numberOfSpaces; i++)
        {
            Spots.Add(new ParkingSpot(i + 1, spacePerSpot));  // Skapa parkeringsruta med storlek
        }
    }

    // Första konstruktorn anropar den andra konstruktorn.
    public ParkingGarage(int numberOfSpaces) : this(numberOfSpaces, 4) { }

    // Försök att parkera ett fordon i garaget.
    public bool ParkVehicle(Vehicle vehicle)
    {
        foreach (var spot in Spots)
        {
            if (spot.IsAvailable() && spot.AvailableSpace >= vehicle.SpaceRequired)
            {
                if (spot.ParkVehicle(vehicle))
                {
                    Console.WriteLine($"Fordon med registreringsnummer {vehicle.RegistrationNumber} parkerades på plats {spot.SpotNumber}.");
                    return true;  // Parkeringen lyckades.
                }
            }
        }

        Console.WriteLine("Det finns inte tillräckligt med ledigt utrymme för fordonet.");
        return false;  // Parkeringen misslyckades.
    }

    // Ta bort ett fordon från garaget.
    public bool RemoveVehicle(Vehicle vehicle)
    {
        foreach (var spot in Spots)
        {
            if (spot.RemoveVehicle(vehicle))
            {
                Console.WriteLine($"Fordon med registreringsnummer {vehicle.RegistrationNumber} togs bort från plats {spot.SpotNumber}.");
                return true;
            }
        }
        Console.WriteLine("Fordonet hittades inte i garaget.");
        return false;
    }

    // Sök efter ett fordon i garaget baserat på registreringsnummer
    public Vehicle SearchVehicle(string regNr)
    {
        foreach (var spot in Spots)
        {
            var vehicle = spot.GetParkedVehicle();
            if (vehicle != null && vehicle.RegistrationNumber == regNr)
            {
                Console.WriteLine($"Fordon med registreringsnummer {regNr} hittades på plats {spot.SpotNumber}.");
                return vehicle;
            }
        }
        Console.WriteLine($"Fordon med registreringsnummer {regNr} kunde inte hittas.");
        return null;
    }

    // Flytta ett fordon från en plats till en annan
    public bool MoveVehicle(string regNr)
    {
        var vehicle = SearchVehicle(regNr);
        if (vehicle == null)
        {
            Console.WriteLine($"Fordon med registreringsnummer {regNr} hittades inte.");
            return false;
        }

        // Hitta den nuvarande platsen för fordonet
        ParkingSpot currentSpot = null;
        foreach (var spot in Spots)
        {
            if (spot.GetParkedVehicle() == vehicle)
            {
                currentSpot = spot;
                break; // Vi har hittat platsen där fordonet står
            }
        }

        // Om fordonet finns och vi kan ta bort det
        if (currentSpot != null && RemoveVehicle(vehicle))
        {
            // Försök att parkera fordonet på en annan ledig plats
            foreach (var spot in Spots)
            {
                // Kontrollera att det är en ledig plats och inte samma plats
                if (spot.IsAvailable() && spot != currentSpot)
                {
                    if (spot.ParkVehicle(vehicle))
                    {
                        Console.WriteLine($"Fordon med registreringsnummer {regNr} flyttades från plats {currentSpot.SpotNumber} till plats {spot.SpotNumber}.");
                        return true;
                    }
                }
            }

            // Om ingen annan plats är tillgänglig
            Console.WriteLine("Det gick inte att flytta fordonet till en ny plats.");
            return false;
        }

        // Om fordonet inte kan tas bort eller det inte fanns någon plats
        Console.WriteLine("Det gick inte att ta bort fordonet från dess nuvarande plats.");
        return false;
    }

    // Hämta ett fordon från garaget baserat på registreringsnummer
    public Vehicle RetrieveVehicle(string regNr)
    {
        var vehicle = SearchVehicle(regNr);
        if (vehicle != null)
        {
            RemoveVehicle(vehicle);
            Console.WriteLine($"Fordon med registreringsnummer {regNr} har hämtats och togs bort från garaget.");
            return vehicle;
        }
        return null;
    }

    // Visa status för alla parkeringsplatser i garaget
    internal void ShowStatus()
    {
        Console.WriteLine("Status för parkeringsgaraget:");

        // Loopa igenom alla parkeringsrutor och visa deras status
        foreach (var spot in Spots)
        {
            var vehicle = spot.GetParkedVehicle();
            if (vehicle != null)
            {
                Console.WriteLine($"Plats {spot.SpotNumber}: Upptagen av fordon {vehicle.RegistrationNumber}.");
            }
            else
            {
                Console.WriteLine($"Plats {spot.SpotNumber}: Ledig.");
            }
        }
    }
}