using Microsoft.Extensions.Configuration;

// Läser inställningar från appsettings.json (samma sätt som i hero_databas).
// Databasens sökväg ligger där, så du slipper skriva den i koden.
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Hittar ingen connection string 'Default' i appsettings.json.");

// Börja här. Den här raden visar att inställningarna lästes in, ta bort den när du vill.
Console.WriteLine($"Musiklistappen: börja med att få menyn att fungera!  (databas: {connectionString})");

// Tips på ordning (se UPPGIFT.md):
//   1. Rita upp huvudmenyn (alternativ numrerade 1-N) och läs in ett val med Console.ReadLine().
//      Skriv ut "Här kommer en lista senare" tills databasen är kopplad.
//   2. Skapa ett DbHandler-objekt: new DbHandler(connectionString)
//   3. Anropa DbHandlers metoder från menyn. Här ska ALDRIG SQL stå.
//   4. Avsluta snyggt när användaren väljer "Avsluta".


bool kör = true;
while(kör)
{
    Console.WriteLine("\n=== Taylor Swifts musiklista ===");
    Console.WriteLine("1. Visa alla låtar");
    Console.WriteLine("2. Lägg till en låt");
    Console.WriteLine("3. Ta bort en låt");
    Console.WriteLine("4. Avsluta");
    Console.Write("Välj: ");

    string val = Console.ReadLine() ?? "";

    switch (val)
    {
        case "1":
            Console.WriteLine("Här ska låtarna visas.");
            break;

        case "2":
            Console.WriteLine("Här ska en låt läggas till.");
            break;

        case "3":
            Console.WriteLine("Här ska en låt tas bort.");
            break;

        case "4":
            kör = false;
            break;

        default:
            Console.WriteLine("Ogiltigt val. Försök igen.");
            break;
    }
}