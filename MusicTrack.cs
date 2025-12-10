using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    /// <summary>
    /// Трек: реалізує IPrintable + .NET-інтерфейси IComparable, ICloneable.
    /// Це дає приклад множинної реалізації інтерфейсів.
    /// </summary>
    public class MusicTrack : IPrintable, IComparable<MusicTrack>, ICloneable
    {
        private int ratingVotes;

        public int Id { get; }
        public string Title { get; private set; }
        public Artist Artist { get; private set; }
        public Genre Genre { get; private set; }
        public int DurationSeconds { get; private set; }
        public double Rating { get; private set; }

        public MusicTrack(int id, string title, Artist artist, Genre genre, int durationSeconds)
        {
            if (durationSeconds <= 0)
                throw new ArgumentOutOfRangeException(nameof(durationSeconds));

            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Artist = artist ?? throw new ArgumentNullException(nameof(artist));
            Genre = genre;
            DurationSeconds = durationSeconds;
            Rating = 0;
            ratingVotes = 0;
        }

        public void Rename(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("Назва треку не може бути порожньою.", nameof(newTitle));

            Title = newTitle;
        }

        public void ChangeGenre(Genre newGenre)
        {
            Genre = newGenre;
        }

        /// <summary>
        /// Додає голос до рейтингу (1..10), зберігаючи середнє значення.
        /// </summary>
        public void AddRating(double score)
        {
            if (score < 1 || score > 10)
                throw new ArgumentOutOfRangeException(nameof(score), "Рейтинг має бути в діапазоні 1..10.");

            Rating = (Rating * ratingVotes + score) / (ratingVotes + 1);
            ratingVotes++;
        }

        public string GetDisplayText()
        {
            TimeSpan duration = TimeSpan.FromSeconds(DurationSeconds);
            return $"Track: \"{Title}\" | {Artist.Name} | {Genre} | {duration:mm\\:ss} | Rating: {Rating:F1}";
        }

        // IComparable<MusicTrack> – порівняння за назвою, потім за тривалістю
        public int CompareTo(MusicTrack other)
        {
            if (other == null) return 1;

            int titleCompare = string.Compare(Title, other.Title, StringComparison.OrdinalIgnoreCase);
            if (titleCompare != 0)
                return titleCompare;

            return DurationSeconds.CompareTo(other.DurationSeconds);
        }

        // ICloneable – поверхнева копія
        public object Clone()
        {
            return new MusicTrack(Id, Title, Artist, Genre, DurationSeconds)
            {
                Rating = Rating,
                ratingVotes = ratingVotes
            };
        }
    }
}
