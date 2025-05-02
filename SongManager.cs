using System.Text;
using System.Text.Json;

namespace MLOOP_L9
{
    public class SongManager
    {
        private List<Song> songs = new List<Song>();
        private string defaultFileName = "songs.json";
        public const string lineText = "~~~~~";

        public void AddSong()
        {
            Console.Clear();
            Console.WriteLine($"\n {lineText} ДОДАВАННЯ НОВОЇ ПІСНІ {lineText}\n");

            Song song = new Song();

            Console.Write(" Назва пісні: \n > ");
            song.Title = Console.ReadLine() ?? string.Empty;

            Console.Write(" П.І.Б. автора: \n > ");
            song.Author = Console.ReadLine() ?? string.Empty;

            Console.Write(" Композитор: \n > ");
            song.Composer = Console.ReadLine() ?? string.Empty;

            Console.Write(" Рік написання: \n > ");
            if (int.TryParse(Console.ReadLine(), out int year)) song.Year = year;

            Console.WriteLine(" Текст пісні (введіть порожній рядок для завершення):");
            StringBuilder lyricsBuilder = new StringBuilder();
            string? line;

            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                lyricsBuilder.AppendLine(line);
            }

            song.Lyrics = lyricsBuilder.ToString().Trim();

            Console.Write(" Кількість виконавців: \n > ");
            if (int.TryParse(Console.ReadLine(), out int performersCount) && performersCount > 0)
            {
                song.Performers = new string[performersCount];
                for (int i = 0; i < performersCount; i++)
                {
                    Console.Write($" Виконавець №{i + 1}: ");
                    song.Performers[i] = Console.ReadLine() ?? string.Empty;
                }
            }

            Console.Write(" Посилання на пісню: \n > ");
            song.Link = Console.ReadLine() ?? string.Empty;
            songs.Add(song);

            Console.WriteLine("\n Пісню успішно додано! Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        public void RemoveSong()
        {
            Console.Clear();
            Console.WriteLine($"\n {lineText} ВИДАЛЕННЯ ПІСНІ {lineText} \n");

            if (songs.Count == 0)
            {
                Console.WriteLine(" Колекція пісень порожня. Натисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < songs.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {songs[i].Title} - {songs[i].Author}");
            }

            Console.Write("\n Введіть номер пісні для видалення: \n > ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= songs.Count)
            {
                string title = songs[index - 1].Title;
                songs.RemoveAt(index - 1);
                Console.WriteLine($"\n Пісню '{title}' видалено. Натисніть будь-яку клавішу...");
            }
            else
            {
                Console.WriteLine(" Неправильний номер. Натисніть будь-яку клавішу...");
            }

            Console.ReadKey();
        }

        public void EditSong()
        {
            Console.Clear();
            Console.WriteLine($"\n {lineText} РЕДАГУВАННЯ ПІСНІ {lineText}\n");

            if (songs.Count == 0)
            {
                Console.WriteLine(" Колекція пісень порожня. Натисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < songs.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {songs[i].Title} - {songs[i].Author}");
            }

            Console.Write("\n Введіть номер пісні для редагування: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= songs.Count)
            {
                Song song = songs[index - 1];

                Console.WriteLine("\n Що ви хочете змінити?");
                Console.WriteLine(" 1. Назву");
                Console.WriteLine(" 2. Автора");
                Console.WriteLine(" 3. Композитора");
                Console.WriteLine(" 4. Рік");
                Console.WriteLine(" 5. Текст пісні");
                Console.WriteLine(" 6. Виконавців");
                Console.Write("\n > ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write(" Нова назва: \n > ");
                            song.Title = Console.ReadLine() ?? song.Title;
                            break;
                        case 2:
                            Console.Write(" Новий автор: \n > ");
                            song.Author = Console.ReadLine() ?? song.Author;
                            break;
                        case 3:
                            Console.Write(" Новий композитор: \n > ");
                            song.Composer = Console.ReadLine() ?? song.Composer;
                            break;
                        case 4:
                            Console.Write(" Новий рік: \n > ");
                            if (int.TryParse(Console.ReadLine(), out int newYear))
                                song.Year = newYear;
                            break;
                        case 5:
                            Console.WriteLine(" Новий текст пісні (введіть порожній рядок для завершення):\n > ");
                            StringBuilder lyricsBuilder = new StringBuilder();
                            string? line;
                            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                            {
                                lyricsBuilder.AppendLine(line);
                            }
                            song.Lyrics = lyricsBuilder.ToString().Trim();
                            break;
                        case 6:
                            Console.Write(" Кількість виконавців: \n > ");
                            if (int.TryParse(Console.ReadLine(), out int performersCount) && performersCount > 0)
                            {
                                song.Performers = new string[performersCount];
                                for (int i = 0; i < performersCount; i++)
                                {
                                    Console.Write($" Виконавець #{i + 1}: \n > ");
                                    song.Performers[i] = Console.ReadLine() ?? string.Empty;
                                }
                            }
                            break;
                        default:
                            Console.WriteLine(" Неправильний вибір.");
                            break;
                    }

                    Console.WriteLine("\n Інформацію оновлено. Натисніть будь-яку клавішу...");
                }
                else
                {
                    Console.WriteLine(" Неправильний вибір. Натисніть будь-яку клавішу...");
                }
            }
            else
            {
                Console.WriteLine(" Неправильний номер. Натисніть будь-яку клавішу...");
            }

            Console.ReadKey();
        }

        public void SearchSong()
        {
            Console.Clear();
            Console.WriteLine($"\n {lineText} ПОШУК ПІСНІ {lineText}\n");

            if (songs.Count == 0)
            {
                Console.WriteLine(" Колекція пісень порожня. Натисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine(" За яким критерієм шукати?");
            Console.WriteLine(" 1. За назвою");
            Console.WriteLine(" 2. За автором");
            Console.WriteLine(" 3. За композитором");
            Console.WriteLine(" 4. За роком");
            Console.WriteLine(" 5. За виконавцем");
            Console.Write("\n Ваш вибір: \n > ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                List<Song> results = new List<Song>();
                string searchTerm;
                int searchYear;

                switch (choice)
                {
                    case 1:
                        Console.Write(" Введіть назву для пошуку: \n > ");
                        searchTerm = Console.ReadLine() ?? string.Empty;
                        results = songs.Where(s => s.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case 2:
                        Console.Write(" Введіть автора для пошуку: \n > ");
                        searchTerm = Console.ReadLine() ?? string.Empty;
                        results = songs.Where(s => s.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case 3:
                        Console.Write(" Введіть композитора для пошуку: \n > ");
                        searchTerm = Console.ReadLine() ?? string.Empty;
                        results = songs.Where(s => s.Composer.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case 4:
                        Console.Write(" Введіть рік для пошуку: \n > ");
                        if (int.TryParse(Console.ReadLine(), out searchYear))
                            results = songs.Where(s => s.Year == searchYear).ToList();
                        break;
                    case 5:
                        Console.Write(" Введіть виконавця для пошуку: \n > ");
                        searchTerm = Console.ReadLine() ?? string.Empty;
                        results = songs.Where(s => s.Performers.Any(p => p.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))).ToList();
                        break;
                    default:
                        Console.WriteLine(" Неправильний вибір. Натисніть будь-яку клавішу...\n > ");
                        Console.ReadKey();
                        return;
                }

                Console.WriteLine($"\n Знайдено результатів: {results.Count}");
                for (int i = 0; i < results.Count; i++)
                {
                    Console.WriteLine($"\n --- Результат #{i + 1} ---");
                    Console.WriteLine(results[i].ToString());
                }
            }
            else
            {
                Console.WriteLine(" Неправильний вибір. Натисніть будь-яку клавішу...");
            }

            Console.WriteLine("\n Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }

        public void SaveToFile()
        {
            Console.Clear();
            Console.WriteLine($"\n {lineText} ЗБЕРЕЖЕННЯ КОЛЕКЦІЇ У ФАЙЛ {lineText}\n");

            Console.Write($" Введіть ім'я файлу (за замовчуванням {defaultFileName}): \n > ");
            string fileName = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = defaultFileName;

            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                string jsonString = JsonSerializer.Serialize(songs, options);
                File.WriteAllText(fileName, jsonString, Encoding.Unicode);

                Console.WriteLine($"\n Колекцію успішно збережено у файл '{fileName}'. Натисніть будь-яку клавішу...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Помилка при збереженні файлу: {ex.Message}. Натисніть будь-яку клавішу...");
            }

            Console.ReadKey();
        }

        public void LoadFromFile()
        {
            Console.Clear();
            Console.WriteLine($"\n {lineText} ЗАВАНТАЖЕННЯ КОЛЕКЦІЇ З ФАЙЛУ {lineText}\n");

            Console.Write($" Введіть ім'я файлу (за замовчуванням {defaultFileName}): ");
            string fileName = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = defaultFileName;

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"\n Файл '{fileName}' не знайдено. Натисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"\n Файл '{fileName}' не знайдено. Натисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            try
            {
                string jsonString = File.ReadAllText(fileName, Encoding.Unicode);
                List<Song>? loadedSongs = JsonSerializer.Deserialize<List<Song>>(jsonString);
                if (loadedSongs != null)
                {
                    songs = loadedSongs;
                    Console.WriteLine($"\n Завантажено {songs.Count} пісень з файлу '{fileName}'. Натисніть будь-яку клавішу...");
                }
                else
                {
                    Console.WriteLine("\n Помилка: Файл містить невірний формат даних. Натисніть будь-яку клавішу...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Помилка при завантаженні файлу: {ex.Message}. Натисніть будь-яку клавішу...");
            }

            Console.ReadKey();
        }

        public void ShowSongsByPerformer()
        {
            Console.Clear();
            Console.WriteLine($"\n {lineText} ПІСНІ ЗА ВИКОНАВЦЕМ {lineText}\n");

            if (songs.Count == 0)
            {
                Console.WriteLine(" Колекція пісень порожня. Натисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            Console.Write(" Введіть ім'я виконавця: \n > ");
            string performer = Console.ReadLine() ?? string.Empty;

            List<Song> performerSongs = songs
                .Where(s => s.Performers.Any(p => p.Contains(performer, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            Console.WriteLine($"\n Знайдено пісень виконавця '{performer}': {performerSongs.Count}");

            for (int i = 0; i < performerSongs.Count; i++)
            {
                Console.WriteLine($"\n --- Пісня #{i + 1} ---");
                Console.WriteLine(performerSongs[i].ToString());
            }

            Console.WriteLine("\n Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }

        public void ShowAllSongs()
        {
            Console.Clear();
            Console.WriteLine($"\n {lineText} УСІ ПІСНІ В КОЛЕКЦІЇ {lineText}\n");

            if (songs.Count == 0)
            {
                Console.WriteLine(" Колекція пісень порожня. Натисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < songs.Count; i++)
            {
                Console.WriteLine($"\n --- Пісня #{i + 1} ---");
                Console.WriteLine(songs[i].ToString());
            }

            Console.WriteLine("\n Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }
}
