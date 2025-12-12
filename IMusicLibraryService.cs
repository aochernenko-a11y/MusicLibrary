using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public interface IMusicLibraryService
    {
        IReadOnlyList<Artist> Artists { get; }
        IReadOnlyList<MusicTrack> Tracks { get; }
        IReadOnlyList<Album> Albums { get; }
        IReadOnlyList<Playlist> Playlists { get; }

        Artist CreateArtist(int id, string name, string country);
        MusicTrack CreateTrack(int id, string title, Artist artist, Genre genre, int durationSeconds);
        Album CreateAlbum(string name, int year, Artist artist);
        Playlist CreatePlaylist(string name, string ownerName);

        void AddTrackToAlbum(Album album, MusicTrack track);
        void AddTrackToPlaylist(Playlist playlist, MusicTrack track);

        IEnumerable<MusicTrack> FindTracksByArtist(string artistName);
        IEnumerable<MusicTrack> FindTracksByGenre(Genre genre);

        bool DeleteTrack(MusicTrack track);
        bool DeleteAlbum(Album album);
        bool DeletePlaylist(Playlist playlist);
    }
}
