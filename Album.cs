using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public class Album : ITrackCollection
    {
        private string name;
        private int year;
        private Artist artist;
        private List<MusicTrack> tracks;

        public string Name
        {
            get => throw new NotImplementedException();
        }

        public int Year
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Artist Artist
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public IReadOnlyList<MusicTrack> Tracks
        {
            get => throw new NotImplementedException();
        }

        public Album(string name, int year, Artist artist)
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

        public string GetDisplayText()
        {
            throw new NotImplementedException();
        }
    }
}
