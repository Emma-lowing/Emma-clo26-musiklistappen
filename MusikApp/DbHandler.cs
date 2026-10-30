using Microsoft.Data.Sqlite;

namespace MusikApp;

public class DbHandler
{
    private readonly string _connectionString;

    public DbHandler(string connectionString)
    {
        _connectionString = connectionString;
    }

    // ---------------------------------------------------------------
    // G-nivå – grundfunktioner och hjälpmetoder
    // ---------------------------------------------------------------

    /// <summary>Hämtar alla låtar, sorterade på titel.</summary>
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
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id, title, duration_seconds, album_id FROM Songs WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            int durationSeconds = 0;

            if (!reader.IsDBNull(2))
            {
                durationSeconds = reader.GetInt32(2);
            }

            return new Song
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                DurationSeconds = durationSeconds,
                AlbumId = reader.GetInt32(3)
            };
        }

        return null;
    }
  

    /// <summary>Lägger till en ny låt.</summary>
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

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.Parameters.AddWithValue("@id", id);
        command.CommandText = "DELETE FROM SongGenres WHERE song_id = @id";
        command.ExecuteNonQuery();

        command.CommandText = "DELETE FROM Songs WHERE id = @id";
        int deletedRows = command.ExecuteNonQuery();
        transaction.Commit();
        return deletedRows > 0;
    }

    // ---------------------------------------------------------------
    // VG-nivå – låtar med album genom JOIN
    // ---------------------------------------------------------------

    /// <summary>(VG, alternativ A) Låtar tillsammans med albumets namn. Kräver en JOIN.</summary>
    public List<(string SongTitle, string AlbumName)> GetSongsWithAlbum()
    {
        var songWithAlbums = new List<(string SongTitle, string AlbumName)>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
SELECT Songs.title, Albums.title
FROM Songs
JOIN Albums ON Songs.album_id = Albums.id
ORDER BY Albums.title, Songs.title;
""";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            string songTitle = reader.GetString(0);
            string albumName = reader.GetString(1);

            songWithAlbums.Add((songTitle, albumName));
        }
        return songWithAlbums;
    }
}
