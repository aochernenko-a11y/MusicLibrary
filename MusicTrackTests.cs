using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicLibrary;

namespace MusicLibrary_test
{
    [TestClass]
    public class MusicTrackTests
    {
        private Artist CreateDefaultArtist()
        {
            return new Artist(1, "Test Artist", "UK");
        }

        [TestMethod]
        public void Constructor_SetsAllProperties()
        {
            var artist = CreateDefaultArtist();

            var track = new MusicTrack(
                id: 10,
                title: "Believer",
                artist: artist,
                genre: Genre.Rock,
                durationSeconds: 204);

            Assert.AreEqual(10, track.Id);
            Assert.AreEqual("Believer", track.Title);
            Assert.AreEqual(artist, track.Artist);
            Assert.AreEqual(Genre.Rock, track.Genre);
            Assert.AreEqual(204, track.DurationSeconds);
            Assert.AreEqual(0.0, track.Rating, 0.0001);
        }

        [TestMethod]
        public void Rename_ChangesTitle()
        {
            var artist = CreateDefaultArtist();
            var track = new MusicTrack(1, "Old name", artist, Genre.Pop, 180);

            track.Rename("New name");

            Assert.AreEqual("New name", track.Title);
        }

        [TestMethod]
        public void ChangeGenre_UpdatesGenre()
        {
            var artist = CreateDefaultArtist();
            var track = new MusicTrack(1, "Song", artist, Genre.Pop, 180);

            track.ChangeGenre(Genre.Rock);

            Assert.AreEqual(Genre.Rock, track.Genre);
        }

        [TestMethod]
        public void AddRating_SetsRating()
        {
            var artist = CreateDefaultArtist();
            var track = new MusicTrack(1, "Song", artist, Genre.Rock, 200);

            track.AddRating(4);

            Assert.AreEqual(4.0, track.Rating, 0.0001);
        }
    }
}
