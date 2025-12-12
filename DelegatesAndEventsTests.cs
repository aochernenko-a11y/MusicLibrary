using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLibrary;

namespace MusicLibrary_test
{
    [TestClass]
    public class DelegatesAndEventsTests
    {
        [TestMethod]
        public void TrackCreated_Event_Fires_When_Track_Is_Created()
        {
            var service = new MusicLibraryService();
            bool fired = false;

            service.TrackCreated += track =>
            {
                if (track.Title == "TestTrack")
                    fired = true;
            };

            var artist = service.CreateArtist(1, "A", "UA");

            service.CreateTrack(10, "TestTrack", artist, Genre.Rock, 120);

            Assert.IsTrue(fired, "Подія TrackCreated повинна викликатися при створенні треку");
        }

        [TestMethod]
        public void TrackRated_Event_Fires_When_Rating_Added()
        {
            var service = new MusicLibraryService();
            bool fired = false;
            double receivedScore = 0;

            service.TrackRated += (track, score) =>
            {
                fired = true;
                receivedScore = score;
            };

            var artist = service.CreateArtist(1, "Artist", "UA");
            var track = service.CreateTrack(1, "Song", artist, Genre.Rock, 200);

            track.AddRating(8.5);
            service.NotifyTrackRated(track, 8.5);

            Assert.IsTrue(fired, "Подія TrackRated повинна викликатися при оцінюванні треку");
            Assert.AreEqual(8.5, receivedScore);
        }

        [TestMethod]
        public void TrackFormatter_FormatsTrackCorrectly()
        {
            var service = new MusicLibraryService();
            var artist = service.CreateArtist(1, "Artist", "UA");
            var track = service.CreateTrack(1, "MySong", artist, Genre.Pop, 150);

            TrackFormatter formatter = t => $"{t.Id}:{t.Title}:{t.Genre}";

            string result = formatter(track);

            Assert.AreEqual("1:MySong:Pop", result);
        }

        [TestMethod]
        public void TrackFilter_FiltersHighRatedTracks()
        {
            var service = new MusicLibraryService();
            var artist = service.CreateArtist(1, "A", "UA");

            var t1 = service.CreateTrack(1, "Song1", artist, Genre.Rock, 100);
            var t2 = service.CreateTrack(2, "Song2", artist, Genre.Rock, 100);

            t1.AddRating(9);
            t2.AddRating(5);

            TrackFilter filter = t => t.Rating >= 8.0;

            var result = service.Tracks.Where(t => filter(t)).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreSame(t1, result[0]);
        }

        [TestMethod]
        public void VoidNotification_InvokesSuccessfully()
        {
            string captured = "";
            VoidNotification notify = msg => captured = msg;

            notify("Hello!");

            Assert.AreEqual("Hello!", captured);
        }
    }
}
