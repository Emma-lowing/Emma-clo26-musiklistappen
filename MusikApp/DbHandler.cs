using Microsoft.Data.Sqlite;

namespace MusikApp;

/// <summary>
/// ALL databaskod bor här. Program.cs ska aldrig innehålla SQL eller SqliteConnection.
///
/// Det här är ett skelett: metoderna finns, kommentarerna visar vad varje metod ska göra,
/// men koden är din. Alla metoder kastar NotImplementedException tills du har skrivit dem.
///
/// ADO.NET i fem steg (samma i varje metod som pratar med databasen):
///   1. Öppna en anslutning        -> new SqliteConnection(...) och connection.Open()
///   2. Skapa ett kommando         -> connection.CreateCommand()
///   3. Skriv SQL:en               -> command.CommandText = "..."
///   4. Skicka in värden säkert    -> command.Parameters.AddWithValue("@namn", värde)
///   5. Kör kommandot              -> ExecuteReader() för SELECT, ExecuteNonQuery() för INSERT/DELETE
///
/// Använd "using var" på anslutning och kommando så stängs de av sig själva.
/// Bygg SQL:en med parametrar (@titel), aldrig genom att klistra ihop strängar.
/// </summary>
public class DbHandler
{
    private readonly string _connectionString;
    // Tips: spara bara connection string här. Den kommer från appsettings.json
    // via Program.cs (t.ex. "Data Source=musik.db").
    // Öppna sedan en NY anslutning inuti varje metod nedan.

    public DbHandler(string connectionString)
    {
        _connectionString = connectionString;
    }

    // ---------------------------------------------------------------
    // G-nivå: du behöver minst tre av de fem här (välj själv vilka)
    // ---------------------------------------------------------------

    /// <summary>Hämtar alla låtar, sorterade på titel.</summary>
    
    //Metod GetAllSongs som hämtar alla låtar från databasen och returnerar dem som en lista av Song-objekt
    public List<Song> GetAllSongs()
    {
        var songs = new List<Song>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id, title, duration_seconds, album_id FROM Songs ORDER BY title";

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            int durationSeconds = 0;

            if (!reader.IsDBNull(2))
            {
                durationSeconds = reader.GetInt32(2);
            }

            var song = new Song
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                DurationSeconds = durationSeconds,
                AlbumId = reader.GetInt32(3)
            };

            songs.Add(song);
        }

        return songs;
    }


    //Kontroll om album existerar
    public bool AlbumExists(int albumId)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Albums WHERE id = @albumId";
        command.Parameters.AddWithValue("@albumId", albumId);

        long count = (long)command.ExecuteScalar()!;

        return count > 0;
    }

    /// <summary>Hämtar en specifik låt. Returnerar null om den inte finns.</summary>
    public Song? GetSongById(int id)
    {
        // TODO: SELECT ... WHERE Id = @id (parameter!).
        // TODO: if (reader.Read()) { ... } annars returnera null.
        throw new NotImplementedException();
    }

    /// <summary>Söker låtar där titeln innehåller söktexten.</summary>
    public List<Song> SearchSongs(string searchText)
    {
        // TODO: SELECT ... WHERE Titel LIKE @sok.
        // Tips: parametervärdet ska vara "%" + searchText + "%" så att LIKE matchar delar av titeln.
        throw new NotImplementedException();
    }

    /// <summary>Lägger till en ny låt.</summary>
    /// 

    //AddSong-metoden som lägger till en ny låt i databasen
    public void AddSong(Song song)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();

        command.CommandText = """
INSERT INTO Songs (title, duration_seconds, album_id)
VALUES (@title, @durationSeconds, @albumId);
""";
        command.Parameters.AddWithValue("@title", song.Title);
        command.Parameters.AddWithValue("@durationSeconds", song.DurationSeconds);
        command.Parameters.AddWithValue("@albumId", song.AlbumId);

        command.ExecuteNonQuery();
    }

    /// <summary>Tar bort en låt. Returnerar true om en rad faktiskt togs bort.</summary>
    public bool DeleteSong(int id)
    {
        // TODO: DELETE FROM ... WHERE Id = @id.
        // Tips: ExecuteNonQuery() returnerar antalet påverkade rader.
        // Menyn i Program.cs ska fråga "Är du säker? j/n" innan den här metoden anropas.
        throw new NotImplementedException();
    }

    // ---------------------------------------------------------------
    // VG-nivå: du behöver minst EN av dessa två (och reflektionen i README)
    // ---------------------------------------------------------------

    /// <summary>(VG, alternativ A) Låtar tillsammans med albumets namn. Kräver en JOIN.</summary>
    public List<(string SongTitle, string AlbumName)> GetSongsWithAlbum()
    {
        // TODO: SELECT Låtar.Titel, Album.Namn FROM Låtar JOIN Album ON ... ORDER BY ...
        throw new NotImplementedException();
    }

    /// <summary>(VG, alternativ B) Låtar som är längre än medellängden. Kräver en subquery.</summary>
    public List<Song> GetSongsLongerThanAverage()
    {
        // TODO: SELECT ... WHERE Längd > (SELECT AVG(Längd) FROM Låtar).
        throw new NotImplementedException();
    }
}
