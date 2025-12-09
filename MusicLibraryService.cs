using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public class MusicLibraryService : IMusicLibraryService
    {
        private List<Artist> artists;
        private List<MusicTrack> tracks;
        private List<Album> albums;
        private List<Playlist> playlists;

        public IReadOnlyList<Artist> Artists
        {
            get => throw new NotImplementedException();
        }

        public IReadOnlyList<MusicTrack> Tracks
        {
            get => throw new NotImplementedException();
        }

        public IReadOnlyList<Album> Albums
        {
            get => throw new NotImplementedException();
        }

        public IReadOnlyList<Playlist> Playlists
        {
            get => throw new NotImplementedException();
        }

        public MusicLibraryService()
        {
            throw new NotImplementedException();
        }

        public Artist CreateArtist(int id, string name, string country)
        {
            throw new NotImplementedException();
        }

        public MusicTrack CreateTrack(
            int id,
            string title,
            Artist artist,
            Genre genre,
            int durationSeconds)
        {
            throw new NotImplementedException();
        }

        public Album CreateAlbum(string name, int year, Artist artist)
        {
            throw new NotImplementedException();
        }

        public Playlist CreatePlaylist(string name, string ownerName)
        {
            throw new NotImplementedException();
        }

        public void AddTrackToAlbum(Album album, MusicTrack track)
        {
            throw new NotImplementedException();
        }

        public void AddTrackToPlaylist(Playlist playlist, MusicTrack track)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MusicTrack> FindTracksByArtist(string artistName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MusicTrack> FindTracksByGenre(Genre genre)
        {
            throw new NotImplementedException();
        }
    }
}
