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
        [TestMethod]
        public void AddRating_TwoMarks_CalculatesAverage()
        {
            // Arrange
            var artist = new Artist(1, "Test Artist", "UA");
            var track = new MusicTrack(1, "Song 1", artist, Genre.Rock, 200);

            // Act
            track.AddRating(8.0);
            track.AddRating(10.0);

            // Assert
            Assert.AreEqual(9.0, track.Rating, 0.0001, "Середній рейтинг має бути 9.0");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddRating_InvalidValue_ThrowsException()
        {
            var artist = new Artist(1, "Test Artist", "UA");
            var track = new MusicTrack(1, "Song 1", artist, Genre.Rock, 200);

            // Рейтинг поза діапазоном 1..10
            track.AddRating(11.0);
        }

        [TestMethod]
        public void Rename_ValidName_ChangesTitle()
        {
            var artist = new Artist(1, "Test Artist", "UA");
            var track = new MusicTrack(1, "Old", artist, Genre.Rock, 200);

            track.Rename("New");

            Assert.AreEqual("New", track.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rename_EmptyName_Throws()
        {
            var artist = new Artist(1, "Test Artist", "UA");
            var track = new MusicTrack(1, "Old", artist, Genre.Rock, 200);

            track.Rename("   ");
        }

        [TestMethod]
        public void CompareTo_SortsByTitleThenDuration()
        {
            var artist = new Artist(1, "Artist", "UA");
            var t1 = new MusicTrack(1, "AAA", artist, Genre.Rock, 200);
            var t2 = new MusicTrack(2, "BBB", artist, Genre.Rock, 150);
            var t3 = new MusicTrack(3, "BBB", artist, Genre.Rock, 300);

            var list = new[] { t3, t2, t1 };
            Array.Sort(list);

            Assert.AreSame(t1, list[0], "Першим має бути AAA");
            Assert.AreSame(t2, list[1], "Другим має бути BBB з меншою тривалістю");
            Assert.AreSame(t3, list[2], "Третім має бути BBB з більшою тривалістю");
        }

        [TestMethod]
        public void Clone_CreatesIndependentCopy()
        {
            var artist = new Artist(1, "Artist", "UA");
            var original = new MusicTrack(1, "Song", artist, Genre.Rock, 200);
            original.AddRating(10.0);

            var copy = (MusicTrack)original.Clone();

            Assert.AreEqual(original.Title, copy.Title);
            Assert.AreEqual(original.Rating, copy.Rating, 0.0001);

            // Перейменовуємо копію – оригінал не змінюється
            copy.Rename("Other");
            Assert.AreEqual("Song", original.Title);
            Assert.AreEqual("Other", copy.Title);
        }
    }
}
