using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLibrary;

namespace MusicLibrary_test
{
    [TestClass]
    public class AlbumPlaylistTests
    {
        [TestMethod]
        public void Album_AddTrackAndGetTotalDuration_WorksCorrectly()
        {
            // Arrange
            var artist = new Artist(1, "Artist", "UA");
            var album = new Album("Album 1", 2024, artist);

            var t1 = new MusicTrack(1, "Song 1", artist, Genre.Rock, 100);
            var t2 = new MusicTrack(2, "Song 2", artist, Genre.Rock, 150);

            // Act
            album.AddTrack(t1);
            album.AddTrack(t2);
            var duration = album.GetTotalDuration();

            // Assert
            Assert.AreEqual(2, album.Tracks.Count);
            Assert.AreEqual(250, duration);
        }

        [TestMethod]
        public void Album_AddTrack_DoesNotDuplicate()
        {
            var artist = new Artist(1, "Artist", "UA");
            var album = new Album("Album 1", 2024, artist);
            var track = new MusicTrack(1, "Song", artist, Genre.Rock, 100);

            album.AddTrack(track);
            album.AddTrack(track); // друга спроба

            Assert.AreEqual(1, album.Tracks.Count, "Трек не повинен дублюватися в альбомі");
        }

        [TestMethod]
        public void Playlist_AddAndRemoveTrack_Works()
        {
            var artist = new Artist(1, "Artist", "UA");
            var playlist = new Playlist("My list", "User");

            var t1 = new MusicTrack(1, "Song 1", artist, Genre.Rock, 100);
            var t2 = new MusicTrack(2, "Song 2", artist, Genre.Rock, 150);

            playlist.AddTrack(t1);
            playlist.AddTrack(t2);

            var removed = playlist.RemoveTrack(t1);

            Assert.IsTrue(removed, "Трек має бути видалений");
            Assert.AreEqual(1, playlist.Tracks.Count);
            Assert.AreSame(t2, playlist.Tracks.Single());
        }

        [TestMethod]
        public void TrackCollection_FindByGenreAndArtist_ReturnsCorrectTracks()
        {
            var artist1 = new Artist(1, "Artist 1", "UA");
            var artist2 = new Artist(2, "Artist 2", "US");
            var album = new Album("Album 1", 2024, artist1);

            var t1 = new MusicTrack(1, "Rock 1", artist1, Genre.Rock, 100);
            var t2 = new MusicTrack(2, "Pop 1", artist2, Genre.Pop, 120);
            var t3 = new MusicTrack(3, "Rock 2", artist2, Genre.Rock, 130);

            album.AddTrack(t1);
            album.AddTrack(t2);
            album.AddTrack(t3);

            var rockTracks = album.FindByGenre(Genre.Rock).ToList();
            var byArtist2 = album.FindByArtist("Artist 2").ToList();

            Assert.AreEqual(2, rockTracks.Count, "Має бути 2 рок-треки");
            Assert.AreEqual(2, byArtist2.Count, "Має бути 2 треки виконавця Artist 2");
        }
    }
}
