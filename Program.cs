using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicLibrary
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            IMusicLibraryService library = new MusicLibraryService();
            var svc = library as MusicLibraryService;

            if (svc != null)
            {
                svc.TrackCreated += track =>
                    Console.WriteLine($"[EVENT] Новий трек створено: {track.Title}");

                svc.TrackRated += (track, score) =>
                    Console.WriteLine($"[EVENT] Трек '{track.Title}' отримав рейтинг: {score}");
            }

            SeedDemoData(library);

            while (true)
            {
                Console.WriteLine("=== Music Library ===");
                Console.WriteLine("1  – Створити новий трек");
                Console.WriteLine("2  – Показати всі треки");
                Console.WriteLine("3  – Перейменувати трек");
                Console.WriteLine("4  – Пошук треків за виконавцем");
                Console.WriteLine("5  – Пошук треків за жанром");
                Console.WriteLine("6  – Оцінити трек (rating)");
                Console.WriteLine("7  – Видалити трек з бібліотеки");
                Console.WriteLine("8  – Додати трек до плейлиста");
                Console.WriteLine("9  – Видалити трек з плейлиста");
                Console.WriteLine("10 – Створити новий альбом");
                Console.WriteLine("11 – Показати альбоми");
                Console.WriteLine("12 – Видалити альбом");
                Console.WriteLine("13 – Створити новий плейлист");
                Console.WriteLine("14 – Показати плейлисти");
                Console.WriteLine("15 – Перемішати плейлист");
                Console.WriteLine("16 – Видалити плейлист");
                Console.WriteLine("0  – Вихід");
                Console.Write("Ваш вибір: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        CreateTrackMenu(library);
                        break;

                    case "2":
                        PrintTracks(library.Tracks);
                        break;

                    case "3":
                        RenameTrackMenu(library);
                        break;

                    case "4":
                        SearchByArtist(library);
                        break;

                    case "5":
                        SearchByGenre(library);
                        break;

                    case "6":
                        RateTrackMenu(library);
                        break;

                    case "7":
                        DeleteTrackMenu(library);
                        break;

                    case "8":
                        AddTrackToPlaylistMenu(library);
                        break;

                    case "9":
                        RemoveTrackFromPlaylistMenu(library);
                        break;

                    case "10":
                        CreateAlbumMenu(library);
                        break;

                    case "11":
                        PrintCollections(library.Albums);
                        break;

                    case "12":
                        DeleteAlbumMenu(library);
                        break;

                    case "13":
                        CreatePlaylistMenu(library);
                        break;

                    case "14":
                        PrintCollections(library.Playlists);
                        break;

                    case "15":
                        ShufflePlaylistMenu(library);
                        break;

                    case "16":
                        DeletePlaylistMenu(library);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Невірний пункт меню.");
                        break;
                }


                Console.WriteLine();
                Console.WriteLine("Натисніть Enter, щоб продовжити...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        // ===== ПОШУК =====

        private static void SearchByArtist(IMusicLibraryService library)
        {
            Console.Write("Введіть ім'я виконавця: ");
            string name = Console.ReadLine();
            var byArtist = library.FindTracksByArtist(name);
            PrintTracks(byArtist);
        }

        private static void SearchByGenre(IMusicLibraryService library)
        {
            Console.Write("Введіть жанр (Rock, Pop, HipHop, Jazz, Classical, Metal, Electronic, Folk, Soundtrack): ");
            string genreText = Console.ReadLine();
            if (Enum.TryParse<Genre>(genreText, true, out var genre))
            {
                var byGenre = library.FindTracksByGenre(genre);
                PrintTracks(byGenre);
            }
            else
            {
                Console.WriteLine("Невідомий жанр.");
            }
        }

        // ===== СТВОРЕННЯ ОБ'ЄКТІВ =====

        private static void CreatePlaylistMenu(IMusicLibraryService library)
        {
            Console.Write("Назва плейлиста: ");
            string name = Console.ReadLine();

            Console.Write("Ім'я власника: ");
            string owner = Console.ReadLine();

            var playlist = library.CreatePlaylist(name, owner);
            Console.WriteLine($"Створено плейлист: {playlist.GetDisplayText()}");
        }

        private static void CreateTrackMenu(IMusicLibraryService library)
        {
            Console.Write("Id треку (ціле число): ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Невірний Id.");
                return;
            }

            if (library.Tracks.Any(t => t.Id == id))
            {
                Console.WriteLine("Трек з таким Id вже існує.");
                return;
            }

            Console.Write("Назва треку: ");
            string title = Console.ReadLine();

            Console.Write("Тривалість у секундах: ");
            if (!int.TryParse(Console.ReadLine(), out int duration))
            {
                Console.WriteLine("Невірна тривалість.");
                return;
            }

            Console.Write("Жанр (Rock, Pop, HipHop, Jazz, Classical, Metal, Electronic, Folk, Soundtrack): ");
            string genreText = Console.ReadLine();
            if (!Enum.TryParse<Genre>(genreText, true, out var genre))
            {
                Console.WriteLine("Невідомий жанр.");
                return;
            }

            var artist = SelectOrCreateArtist(library);
            if (artist == null) return;

            var track = library.CreateTrack(id, title, artist, genre, duration);
            Console.WriteLine($"Створено трек: {track.GetDisplayText()}");
        }

        private static void CreateAlbumMenu(IMusicLibraryService library)
        {
            Console.Write("Назва альбому: ");
            string name = Console.ReadLine();

            Console.Write("Рік випуску: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Невірний рік.");
                return;
            }

            var artist = SelectOrCreateArtist(library);
            if (artist == null) return;

            var album = library.CreateAlbum(name, year, artist);
            Console.WriteLine($"Створено альбом: {album.GetDisplayText()}");
        }

        // Вибрати існуючого артиста або створити нового
        private static Artist SelectOrCreateArtist(IMusicLibraryService library)
        {
            if (library.Artists.Any())
            {
                Console.WriteLine("Існуючі виконавці:");
                foreach (var a in library.Artists)
                {
                    Console.WriteLine($"{a.Id}: {a.Name} ({a.Country})");
                }
            }
            else
            {
                Console.WriteLine("У бібліотеці ще немає жодного виконавця.");
            }

            Console.Write("Id виконавця (якщо не існує – буде створений): ");
            if (!int.TryParse(Console.ReadLine(), out int artistId))
            {
                Console.WriteLine("Невірний Id.");
                return null;
            }

            var existing = library.Artists.FirstOrDefault(a => a.Id == artistId);
            if (existing != null)
                return existing;

            Console.Write("Ім'я нового виконавця: ");
            string name = Console.ReadLine();

            Console.Write("Країна: ");
            string country = Console.ReadLine();

            return library.CreateArtist(artistId, name, country);
        }

        // ===== ОПЕРАЦІЇ З ПЛЕЙЛИСТАМИ =====

        private static void AddTrackToPlaylistMenu(IMusicLibraryService library)
        {
            var playlist = SelectPlaylist(library);
            if (playlist == null) return;

            var track = SelectTrack(library);
            if (track == null) return;

            library.AddTrackToPlaylist(playlist, track);
            Console.WriteLine("Трек додано до плейлиста.");
        }

        private static void RemoveTrackFromPlaylistMenu(IMusicLibraryService library)
        {
            var playlist = SelectPlaylist(library);
            if (playlist == null) return;

            if (playlist.Tracks.Count == 0)
            {
                Console.WriteLine("У плейлисті немає треків.");
                return;
            }

            Console.WriteLine("Треки у цьому плейлисті:");
            foreach (var t in playlist.Tracks)
            {
                Console.WriteLine($"{t.Id}: {t.Title} – {t.Artist.Name}");
            }

            Console.Write("Введіть Id треку для видалення: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Невірний Id.");
                return;
            }

            var track = playlist.Tracks.FirstOrDefault(t => t.Id == id);
            if (track == null)
            {
                Console.WriteLine("Трек не знайдено у плейлисті.");
                return;
            }

            bool removed = playlist.RemoveTrack(track);
            Console.WriteLine(removed ? "Трек видалено з плейлиста." : "Не вдалося видалити трек.");
        }

        private static void ShufflePlaylistMenu(IMusicLibraryService library)
        {
            var playlist = SelectPlaylist(library);
            if (playlist == null) return;

            playlist.Shuffle();
            Console.WriteLine("Плейлист перемішано.");
        }

        private static void DeletePlaylistMenu(IMusicLibraryService library)
        {
            var playlist = SelectPlaylist(library);
            if (playlist == null) return;

            bool removed = library.DeletePlaylist(playlist);
            Console.WriteLine(removed ? "Плейлист видалено." : "Не вдалося видалити плейлист.");
        }

        private static void DeleteAlbumMenu(IMusicLibraryService library)
        {
            var album = SelectAlbum(library);
            if (album == null) return;

            bool removed = library.DeleteAlbum(album);
            Console.WriteLine(removed ? "Альбом видалено." : "Не вдалося видалити альбом.");
        }

        private static void DeleteTrackMenu(IMusicLibraryService library)
        {
            var track = SelectTrack(library);
            if (track == null) return;

            bool removed = library.DeleteTrack(track);
            Console.WriteLine(removed ? "Трек видалено з бібліотеки." : "Не вдалося видалити трек.");
        }

        // ===== РЕЙТИНГ / ПЕРЕЙМЕНУВАННЯ =====

        private static void RateTrackMenu(IMusicLibraryService library)
        {
            var track = SelectTrack(library);
            if (track == null) return;

            Console.Write("Оцінка (1..10): ");
            string inputScore = Console.ReadLine().Replace(",", ".");

            if (!double.TryParse(inputScore, System.Globalization.NumberStyles.Any,
                                 System.Globalization.CultureInfo.InvariantCulture, out double score))
            {
                Console.WriteLine("Невірне значення.");
                return;
            }

            try
            {
                track.AddRating(score);
                (library as MusicLibraryService)?.NotifyTrackRated(track, score);
                Console.WriteLine($"Новий рейтинг треку: {track.Rating:F1}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

        }

        private static void RenameTrackMenu(IMusicLibraryService library)
        {
            var track = SelectTrack(library);
            if (track == null) return;

            Console.Write("Нова назва треку: ");
            string newTitle = Console.ReadLine();

            try
            {
                track.Rename(newTitle);
                Console.WriteLine("Назву змінено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        // ===== ВИБІР ОБ'ЄКТІВ =====

        private static MusicTrack SelectTrack(IMusicLibraryService library)
        {
            if (!library.Tracks.Any())
            {
                Console.WriteLine("Немає жодного треку.");
                return null;
            }

            Console.WriteLine("Доступні треки:");
            foreach (var t in library.Tracks.OrderBy(t => t.Id))
            {
                Console.WriteLine($"{t.Id}: {t.Title} – {t.Artist.Name} ({t.Genre})");
            }

            Console.Write("Введіть Id треку: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Невірний Id.");
                return null;
            }

            var track = library.Tracks.FirstOrDefault(t => t.Id == id);
            if (track == null)
            {
                Console.WriteLine("Трек не знайдено.");
            }
            return track;
        }

        private static Playlist SelectPlaylist(IMusicLibraryService library)
        {
            if (!library.Playlists.Any())
            {
                Console.WriteLine("Немає жодного плейлиста.");
                return null;
            }

            Console.WriteLine("Доступні плейлисти:");
            for (int i = 0; i < library.Playlists.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {library.Playlists[i].GetDisplayText()}");
            }

            Console.Write("Оберіть номер плейлиста: ");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Невірний номер.");
                return null;
            }

            if (index < 1 || index > library.Playlists.Count)
            {
                Console.WriteLine("Плейлист з таким номером не існує.");
                return null;
            }

            return library.Playlists[index - 1];
        }

        private static Album SelectAlbum(IMusicLibraryService library)
        {
            if (!library.Albums.Any())
            {
                Console.WriteLine("Немає жодного альбому.");
                return null;
            }

            Console.WriteLine("Доступні альбоми:");
            for (int i = 0; i < library.Albums.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {library.Albums[i].GetDisplayText()}");
            }

            Console.Write("Оберіть номер альбому: ");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Невірний номер.");
                return null;
            }

            if (index < 1 || index > library.Albums.Count)
            {
                Console.WriteLine("Альбом з таким номером не існує.");
                return null;
            }

            return library.Albums[index - 1];
        }

        // ===== ВИВІД (ПОЛІМОРФІЗМ ЧЕРЕЗ IPrintable / ITrackCollection) =====

        private static void PrintTracks(IEnumerable<MusicTrack> tracks)
        {
            Console.WriteLine("ТРЕКИ:");
            foreach (IPrintable printable in tracks)
            {
                Console.WriteLine(printable.GetDisplayText());
            }
        }

        private static void PrintCollections(IEnumerable<ITrackCollection> collections)
        {
            Console.WriteLine("КОЛЕКЦІЇ:");
            foreach (ITrackCollection collection in collections)
            {
                IPrintable printable = (IPrintable)collection;
                Console.WriteLine(printable.GetDisplayText());
            }
        }

        // ===== ДЕМО-ДАНІ =====

        private static void SeedDemoData(IMusicLibraryService library)
        {
            var artist1 = library.CreateArtist(1, "Imagine Dragons", "USA");
            var artist2 = library.CreateArtist(2, "Арія", "Ukraine");
            var artist3 = library.CreateArtist(3, "Hans Zimmer", "Germany");

            var t1 = library.CreateTrack(1, "Believer", artist1, Genre.Rock, 204);
            var t2 = library.CreateTrack(2, "Radioactive", artist1, Genre.Rock, 186);
            var t3 = library.CreateTrack(3, "Щастя поруч", artist2, Genre.Pop, 210);
            var t4 = library.CreateTrack(4, "Main Theme", artist3, Genre.Soundtrack, 300);

            t1.AddRating(9);
            t1.AddRating(10);
            t2.AddRating(8);
            t3.AddRating(7);
            t4.AddRating(10);

            var album = library.CreateAlbum("Best of Dragons", 2020, artist1);
            library.AddTrackToAlbum(album, t1);
            library.AddTrackToAlbum(album, t2);

            var playlist = library.CreatePlaylist("My Favorites", "Student");
            library.AddTrackToPlaylist(playlist, t1);
            library.AddTrackToPlaylist(playlist, t3);
            library.AddTrackToPlaylist(playlist, t4);
        }
    }
}
