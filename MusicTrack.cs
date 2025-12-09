using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public class MusicTrack : IPrintable
    {
        private int id;
        private string title;
        private Artist artist;
        private Genre genre;
        private int durationSeconds;
        private double rating;

        public int Id
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string Title
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Artist Artist
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Genre Genre
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public int DurationSeconds
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public double Rating
        {
            get => throw new NotImplementedException();
            private set => throw new NotImplementedException();
        }

        public MusicTrack(int id,
                          string title,
                          Artist artist,
                          Genre genre,
                          int durationSeconds)
        {
            throw new NotImplementedException();
        }

        public void Rename(string newTitle)
        {
            throw new NotImplementedException();
        }

        public void ChangeGenre(Genre newGenre)
        {
            throw new NotImplementedException();
        }

        public void AddRating(int score)
        {
            throw new NotImplementedException();
        }

        public string GetDisplayText()
        {
            throw new NotImplementedException();
        }
    }
}
