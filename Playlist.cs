using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MusicLibrary
{
    public class Playlist : TrackCollectionBase
    {
        public string OwnerName { get; }

        public Playlist(string name, string ownerName)
            : base(name)
        {
            OwnerName = ownerName ?? "Unknown";
        }

        /// <summary>
        /// Перемішування треків у плейлисті.
        /// </summary>
        public void Shuffle()
        {
            // Доступ до базового списку через foreach підтримується,
            // але тут достатньо створити копію і заново додати в новому порядку.
            var tracksCopy = new System.Collections.Generic.List<MusicTrack>(Tracks);
            var random = new Random();

            for (int i = tracksCopy.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (tracksCopy[i], tracksCopy[j]) = (tracksCopy[j], tracksCopy[i]);
            }

            // Очищаємо і додаємо по-новому
            foreach (var track in Tracks)
            {
                RemoveTrack(track);
            }

            foreach (var track in tracksCopy)
            {
                AddTrack(track);
            }
        }

        public override string GetDisplayText()
        {
            return $"Playlist: \"{Name}\" (Owner: {OwnerName}), Tracks: {Tracks.Count}, Duration: {GetTotalDuration()} s";
        }
    }
}
