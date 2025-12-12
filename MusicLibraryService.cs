using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicLibrary
{
    public class MusicLibraryService : IMusicLibraryService
    {
        private readonly List<Artist> artists = new List<Artist>();
        private readonly List<MusicTrack> tracks = new List<MusicTrack>();
        private readonly List<Album> albums = new List<Album>();
        private readonly List<Playlist> playlists = new List<Playlist>();
        public event Action<MusicTrack> TrackCreated;
        public event Action<MusicTrack, double> TrackRated;

        public IReadOnlyList<Artist> Artists => artists.AsReadOnly();
        public IReadOnlyList<MusicTrack> Tracks => tracks.AsReadOnly();
        public IReadOnlyList<Album> Albums => albums.AsReadOnly();
        public IReadOnlyList<Playlist> Playlists => playlists.AsReadOnly();

        public Artist CreateArtist(int id, string name, string country)
        {
            var existing = artists.FirstOrDefault(a => a.Id == id);
            if (existing != null) return existing;

            var artist = new Artist(id, name, country);
            artists.Add(artist);
            return artist;
        }

        public MusicTrack CreateTrack(int id, string title, Artist artist, Genre genre, int durationSeconds)
        {
            var existing = tracks.FirstOrDefault(t => t.Id == id);
            if (existing != null) return existing;

            var track = new MusicTrack(id, title, artist, genre, durationSeconds);
            tracks.Add(track);
            TrackCreated?.Invoke(track);
            return track;
        }
        public void NotifyTrackRated(MusicTrack track, double score)
        {
            TrackRated?.Invoke(track, score);
        }


        public Album CreateAlbum(string name, int year, Artist artist)
        {
            var album = new Album(name, year, artist);
            albums.Add(album);
            return album;
        }

        public Playlist CreatePlaylist(string name, string ownerName)
        {
            var playlist = new Playlist(name, ownerName);
            playlists.Add(playlist);
            return playlist;
        }

        public void AddTrackToAlbum(Album album, MusicTrack track)
        {
            if (album == null) throw new ArgumentNullException(nameof(album));
            if (track == null) throw new ArgumentNullException(nameof(track));

            album.AddTrack(track);
        }

        public void AddTrackToPlaylist(Playlist playlist, MusicTrack track)
        {
            if (playlist == null) throw new ArgumentNullException(nameof(playlist));
            if (track == null) throw new ArgumentNullException(nameof(track));

            playlist.AddTrack(track);
        }

        public IEnumerable<MusicTrack> FindTracksByArtist(string artistName)
        {
            if (string.IsNullOrWhiteSpace(artistName))
                return Array.Empty<MusicTrack>();

            return tracks.Where(t =>
                t.Artist != null &&
                t.Artist.Name.IndexOf(artistName, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public IEnumerable<MusicTrack> FindTracksByGenre(Genre genre)
        {
            return tracks.Where(t => t.Genre == genre);
        }

        public bool DeleteTrack(MusicTrack track)
        {
            if (track == null) return false;

            foreach (var album in albums)
            {
                album.RemoveTrack(track);
            }

            foreach (var playlist in playlists)
            {
                playlist.RemoveTrack(track);
            }

            return tracks.Remove(track);
        }

        public bool DeleteAlbum(Album album)
        {
            if (album == null) return false;
            return albums.Remove(album);
        }

        public bool DeletePlaylist(Playlist playlist)
        {
            if (playlist == null) return false;
            return playlists.Remove(playlist);
        }
    }
}
