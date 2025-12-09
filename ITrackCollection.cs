using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public interface ITrackCollection : IPrintable
    {
        string Name { get; }

        IReadOnlyList<MusicTrack> Tracks { get; }

        void AddTrack(MusicTrack track);

        bool RemoveTrack(MusicTrack track);

        int GetTotalDuration();

        IEnumerable<MusicTrack> FindByGenre(Genre genre);

        IEnumerable<MusicTrack> FindByArtist(string artistName);
    }
}
