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
    public class MusicLibraryServiceTests
    {
        [TestMethod]
        public void CreateArtist_AddsArtistToCollection()
        {
            IMusicLibraryService service = new MusicLibraryService();

            var artist = service.CreateArtist(1, "Artist", "USA");

            Assert.AreEqual(1, service.Artists.Count);
            Assert.AreEqual(artist, service.Artists[0]);
        }

        [TestMethod]
        public void CreateTrack_AddsTrackToCollection()
        {
            IMusicLibraryService service = new MusicLibraryService();
            var artist = service.CreateArtist(1, "Artist", "USA");

            var track = service.CreateTrack(10, "Song", artist, Genre.Rock, 180);

            Assert.AreEqual(1, service.Tracks.Count);
            Assert.AreEqual(track, service.Tracks[0]);
        }

        [TestMethod]
        public void AddTrackToAlbum_ReallyAdds()
        {
            IMusicLibraryService service = new MusicLibraryService();
            var artist = service.CreateArtist(1, "Artist", "USA");
            var album = service.CreateAlbum("Album", 2020, artist);
            var track = service.CreateTrack(10, "Song", artist, Genre.Rock, 180);

            service.AddTrackToAlbum(album, track);

            Assert.AreEqual(1, album.Tracks.Count);
            Assert.AreEqual(track, album.Tracks[0]);
        }

        [TestMethod]
        public void FindTracksByGenre_ReturnsOnlyTracksOfThisGenre()
        {
            IMusicLibraryService service = new MusicLibraryService();
            var artist = service.CreateArtist(1, "Artist", "USA");

            var t1 = service.CreateTrack(1, "rock1", artist, Genre.Rock, 100);
            var t2 = service.CreateTrack(2, "pop1", artist, Genre.Pop, 120);
            var t3 = service.CreateTrack(3, "rock2", artist, Genre.Rock, 140);

            var rockTracks = service.FindTracksByGenre(Genre.Rock).ToList();

            CollectionAssert.AreEqual(new[] { t1, t3 }, rockTracks);
        }
    }
}
