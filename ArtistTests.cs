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
    public class ArtistTests
    {
        [TestMethod]
        public void Constructor_SetsAllProperties()
        {
            // arrange + act
            var artist = new Artist(1, "Imagine Dragons", "USA");

            // assert
            Assert.AreEqual(1, artist.Id);
            Assert.AreEqual("Imagine Dragons", artist.Name);
            Assert.AreEqual("USA", artist.Country);
        }
    }
}
