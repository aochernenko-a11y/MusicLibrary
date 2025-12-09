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
    public class PlaylistTests
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
        public void Constructor_SetsProperties_AndStartsEmpty()
        {
            var playlist = new Playlist("My playlist", "User1");

            Assert.AreEqual("My playlist", playlist.Name);
            Assert.AreEqual("User1", playlist.OwnerName);
            Assert.IsNotNull(playlist.Tracks);
            Assert.AreEqual(0, playlist.Tracks.Count);
        }

        [TestMethod]
        public void AddTrack_AddsTrack()
        {
            var playlist = new Playlist("My playlist", "User1");
            var track = CreateTrack(1, "Song", 180, Genre.Pop);

            playlist.AddTrack(track);

            Assert.AreEqual(1, playlist.Tracks.Count);
            Assert.AreEqual(track, playlist.Tracks[0]);
        }

        [TestMethod]
        public void GetTotalDuration_ReturnsSumOfDurations()
        {
            var playlist = new Playlist("My playlist", "User1");

            playlist.AddTrack(CreateTrack(1, "t1", 100, Genre.Rock));
            playlist.AddTrack(CreateTrack(2, "t2", 200, Genre.Pop));

            var total = playlist.GetTotalDuration();

            Assert.AreEqual(300, total);
        }

        [TestMethod]
        public void FindByArtist_ReturnsOnlyTracksOfThatArtist()
        {
            var artist1 = new Artist(1, "Artist1", "USA");
            var artist2 = new Artist(2, "Artist2", "UK");

            var playlist = new Playlist("Mix", "User1");

            var t1 = new MusicTrack(1, "song1", artist1, Genre.Rock, 100);
            var t2 = new MusicTrack(2, "song2", artist2, Genre.Rock, 120);
            var t3 = new MusicTrack(3, "song3", artist1, Genre.Pop, 140);

            playlist.AddTrack(t1);
            playlist.AddTrack(t2);
            playlist.AddTrack(t3);

            var byArtist1 = playlist.FindByArtist("Artist1").ToList();

            CollectionAssert.AreEqual(
                new[] { t1, t3 },
                byArtist1);
        }
    }
}
