using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLibrary;

namespace MusicLibrary_test
{
    [TestClass]
    public class MusicLibraryServiceTests
    {
        [TestMethod]
        public void CreateTrack_AddsTrackToService()
        {
            // Arrange
            IMusicLibraryService service = new MusicLibraryService();
            var artist = service.CreateArtist(1, "Artist", "UA");

            // Act
            var track = service.CreateTrack(1, "Song", artist, Genre.Rock, 200);

            // Assert
            Assert.AreEqual(1, service.Tracks.Count);
            Assert.AreSame(track, service.Tracks[0]);
        }

        [TestMethod]
        public void DeleteTrack_RemovesFromServiceAndCollections()
        {
            IMusicLibraryService service = new MusicLibraryService();
            var artist = service.CreateArtist(1, "Artist", "UA");

            var track = service.CreateTrack(1, "Song", artist, Genre.Rock, 200);

            var album = service.CreateAlbum("Album 1", 2024, artist);
            var playlist = service.CreatePlaylist("My list", "User");

            service.AddTrackToAlbum(album, track);
            service.AddTrackToPlaylist(playlist, track);

            // Перевірка, що трек присутній скрізь
            Assert.AreEqual(1, service.Tracks.Count);
            Assert.AreEqual(1, album.Tracks.Count);
            Assert.AreEqual(1, playlist.Tracks.Count);

            // Act
            var deleted = service.DeleteTrack(track);

            // Assert
            Assert.IsTrue(deleted, "DeleteTrack має повернути true");
            Assert.AreEqual(0, service.Tracks.Count, "Трек має бути видалений із сервісу");
            Assert.AreEqual(0, album.Tracks.Count, "Трек має бути видалений з альбому");
            Assert.AreEqual(0, playlist.Tracks.Count, "Трек має бути видалений з плейлиста");
        }

        [TestMethod]
        public void FindTracksByArtist_ReturnsCorrectTracks()
        {
            IMusicLibraryService service = new MusicLibraryService();

            var artist1 = service.CreateArtist(1, "Artist 1", "UA");
            var artist2 = service.CreateArtist(2, "Artist 2", "US");

            var t1 = service.CreateTrack(1, "Song 1", artist1, Genre.Rock, 200);
            var t2 = service.CreateTrack(2, "Song 2", artist2, Genre.Rock, 200);
            var t3 = service.CreateTrack(3, "Song 3", artist1, Genre.Pop, 150);

            var result = service.FindTracksByArtist("Artist 1").ToList();

            Assert.AreEqual(2, result.Count);
            CollectionAssert.Contains(result, t1);
            CollectionAssert.Contains(result, t3);
        }

        [TestMethod]
        public void FindTracksByGenre_ReturnsOnlySelectedGenre()
        {
            IMusicLibraryService service = new MusicLibraryService();
            var artist = service.CreateArtist(1, "Artist", "UA");

            var t1 = service.CreateTrack(1, "Rock 1", artist, Genre.Rock, 200);
            var t2 = service.CreateTrack(2, "Pop 1", artist, Genre.Pop, 180);

            var rock = service.FindTracksByGenre(Genre.Rock).ToList();

            Assert.AreEqual(1, rock.Count);
            Assert.AreSame(t1, rock[0]);
        }
    }
}
