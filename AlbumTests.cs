using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicLibrary;

namespace MusicLibrary_test
{
    [TestClass]
    public class AlbumTests
    {
        private Artist CreateDefaultArtist()
        {
            return new Artist(1, "Test Artist", "UK");
        }

        private MusicTrack CreateTrack(int id, string title, int duration, Genre genre)
        {
            return new MusicTrack(id, title, CreateDefaultArtist(), genre, duration);
        }

        [TestMethod]
        public void Constructor_SetsProperties_AndStartsWithEmptyTrackList()
        {
            var artist = CreateDefaultArtist();

            var album = new Album("Best Hits", 2020, artist);

            Assert.AreEqual("Best Hits", album.Name);
            Assert.AreEqual(2020, album.Year);
            Assert.AreEqual(artist, album.Artist);
            Assert.IsNotNull(album.Tracks);
            Assert.AreEqual(0, album.Tracks.Count);
        }

        [TestMethod]
        public void AddTrack_AddsTrackToCollection()
        {
            var artist = CreateDefaultArtist();
            var album = new Album("Best Hits", 2020, artist);
            var track = CreateTrack(1, "Track 1", 180, Genre.Rock);

            album.AddTrack(track);

            Assert.AreEqual(1, album.Tracks.Count);
            Assert.AreEqual(track, album.Tracks[0]);
        }

        [TestMethod]
        public void GetTotalDuration_ReturnsSumOfTrackDurations()
        {
            var artist = CreateDefaultArtist();
            var album = new Album("Best Hits", 2020, artist);

            album.AddTrack(CreateTrack(1, "t1", 100, Genre.Rock));
            album.AddTrack(CreateTrack(2, "t2", 200, Genre.Rock));

            var total = album.GetTotalDuration();

            Assert.AreEqual(300, total);
        }

        [TestMethod]
        public void FindByGenre_ReturnsOnlyTracksWithThatGenre()
        {
            var artist = CreateDefaultArtist();
            var album = new Album("Mix", 2020, artist);

            var rock1 = CreateTrack(1, "rock1", 100, Genre.Rock);
            var pop1 = CreateTrack(2, "pop1", 120, Genre.Pop);
            var rock2 = CreateTrack(3, "rock2", 150, Genre.Rock);

            album.AddTrack(rock1);
            album.AddTrack(pop1);
            album.AddTrack(rock2);

            var rockTracks = album.FindByGenre(Genre.Rock).ToList();

            CollectionAssert.AreEqual(
                new[] { rock1, rock2 },
                rockTracks);
        }
    }
}
