using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public class Playlist : ITrackCollection
    {
        private string name;
        private string ownerName;
        private List<MusicTrack> tracks;

        public string Name
        {
            get => throw new NotImplementedException();
        }

        public string OwnerName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public IReadOnlyList<MusicTrack> Tracks
        {
            get => throw new NotImplementedException();
        }

        public Playlist(string name, string ownerName)
        {
            throw new NotImplementedException();
        }

        public void AddTrack(MusicTrack track)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTrack(MusicTrack track)
        {
            throw new NotImplementedException();
        }

        public int GetTotalDuration()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MusicTrack> FindByGenre(Genre genre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MusicTrack> FindByArtist(string artistName)
        {
            throw new NotImplementedException();
        }

        public void Shuffle()
        {
            throw new NotImplementedException();
        }

        public string GetDisplayText()
        {
            throw new NotImplementedException();
        }
    }
}
