using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MusicLibrary
{
    public class Album : TrackCollectionBase
    {
        public int Year { get; }
        public Artist Artist { get; }

        public Album(string name, int year, Artist artist)
            : base(name)
        {
            if (year <= 0)
                throw new ArgumentOutOfRangeException(nameof(year));

            Artist = artist ?? throw new ArgumentNullException(nameof(artist));
            Year = year;
        }

        public override string GetDisplayText()
        {
            return $"Album: \"{Name}\" ({Year}), Artist: {Artist.Name}, Tracks: {Tracks.Count}, Duration: {GetTotalDuration()} s";
        }
    }
}
