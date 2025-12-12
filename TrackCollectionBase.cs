using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public abstract class TrackCollectionBase : ITrackCollection, IPrintable
    {
        private readonly List<MusicTrack> tracks = new List<MusicTrack>();

        public string Name { get; protected set; }

        public IReadOnlyList<MusicTrack> Tracks => tracks.AsReadOnly();

        protected TrackCollectionBase(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public virtual void AddTrack(MusicTrack track)
        {
            if (track == null) throw new ArgumentNullException(nameof(track));
            if (!tracks.Contains(track))
            {
                tracks.Add(track);
            }
        }

        public virtual bool RemoveTrack(MusicTrack track)
        {
            if (track == null) return false;
            return tracks.Remove(track);
        }

        public virtual int GetTotalDuration()
        {
            return tracks.Sum(t => t.DurationSeconds);
        }

        public virtual IEnumerable<MusicTrack> FindByGenre(Genre genre)
        {
            return tracks.Where(t => t.Genre == genre);
        }

        public virtual IEnumerable<MusicTrack> FindByArtist(string artistName)
        {
            if (string.IsNullOrWhiteSpace(artistName))
                return Array.Empty<MusicTrack>();

            return tracks.Where(t =>
                t.Artist != null &&
                t.Artist.Name.IndexOf(artistName, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public abstract string GetDisplayText();

        public IEnumerator<MusicTrack> GetEnumerator()
        {
            return tracks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
