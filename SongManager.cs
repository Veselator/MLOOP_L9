using System.Text;
using System.Text.Json;

namespace MLOOP_L9
{
    public class SongManager
    {
        private Song[] songs = new Song[capacity];
        private int count;
        private const int capacity = 1024;
        private const string defaultFileName = "songs.json";
        public const string lineText = "~~~~~";
        private bool isSaved; // Прапор, який показує, чи зберегли ми зміни

        public SongManager()
        {
            songs = new Song[capacity];
            count = 0;
        }

        public SongManager(Song[] songs)
        {
            if (songs != null)
            {
                this.songs = new Song[capacity];
                this.count = songs.Length;
                Array.Copy(songs, this.songs, songs.Length);
            }
            else
            {
                this.songs = new Song[capacity];
                count = 0;
            }
        }

        public SongManager(string fileName)
        {
            songs = new Song[capacity];
            count = 0;
            LoadFromFile(fileName);
        }

        public void CheckIsSaved()
        {
            if (!isSaved) SaveToFile();
        }

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

            songs[count] = song;
            count++;
            isSaved = false;

            Console.WriteLine("\n Пісню успішно додано! Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        public void RemoveSong()
        {
            Console.Clear();
            Console.WriteLine($"\n {lineText} ВИДАЛЕННЯ ПІСНІ {lineText} \n");

            if (count == 0)
            {
                Console.WriteLine(" Колекція пісень порожня. Натисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($" {i + 1}. {songs[i].Title} - {songs[i].Author}");
            }

            Console.Write("\n Введіть номер пісні для видалення: \n > ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= count)
            {
                string title = songs[index - 1].Title;

                for (int i = index - 1; i < count - 1; i++)
                {
                    songs[i] = songs[i + 1];
                }
                count--;
                isSaved = false;

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

            if (count == 0)
            {
                Console.WriteLine(" Колекція пісень порожня. Натисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($" {i + 1}. {songs[i].Title} - {songs[i].Author}");
            }

            Console.Write("\n Введіть номер пісні для редагування: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= count)
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
                            isSaved = false;
                            break;
                        case 2:
                            Console.Write(" Новий автор: \n > ");
                            song.Author = Console.ReadLine() ?? song.Author;
                            isSaved = false;
                            break;
                        case 3:
                            Console.Write(" Новий композитор: \n > ");
                            song.Composer = Console.ReadLine() ?? song.Composer;
                            isSaved = false;
                            break;
                        case 4:
                            Console.Write(" Новий рік: \n > ");
                            if (int.TryParse(Console.ReadLine(), out int newYear))
                                song.Year = newYear;
                            isSaved = false;
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
                            isSaved = false;
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
                            isSaved = false;
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

            if (count == 0)
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
                Song[] results = new Song[count];
                int resultCount = 0;
                string searchTerm;
                int searchYear;

                switch (choice)
                {
                    case 1:
                        Console.Write(" Введіть назву для пошуку: \n > ");
                        searchTerm = Console.ReadLine() ?? string.Empty;
                        for (int i = 0; i < count; i++)
                        {
                            if (songs[i].Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                            {
                                results[resultCount++] = songs[i];
                            }
                        }
                        break;
                    case 2:
                        Console.Write(" Введіть автора для пошуку: \n > ");
                        searchTerm = Console.ReadLine() ?? string.Empty;
                        for (int i = 0; i < count; i++)
                        {
                            if (songs[i].Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                            {
                                results[resultCount++] = songs[i];
                            }
                        }
                        break;
                    case 3:
                        Console.Write(" Введіть композитора для пошуку: \n > ");
                        searchTerm = Console.ReadLine() ?? string.Empty;
                        for (int i = 0; i < count; i++)
                        {
                            if (songs[i].Composer.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                            {
                                results[resultCount++] = songs[i];
                            }
                        }
                        break;
                    case 4:
                        Console.Write(" Введіть рік для пошуку: \n > ");
                        if (int.TryParse(Console.ReadLine(), out searchYear))
                        {
                            for (int i = 0; i < count; i++)
                            {
                                if (songs[i].Year == searchYear)
                                {
                                    results[resultCount++] = songs[i];
                                }
                            }
                        }
                        break;
                    case 5:
                        Console.Write(" Введіть виконавця для пошуку: \n > ");
                        searchTerm = Console.ReadLine() ?? string.Empty;
                        for (int i = 0; i < count; i++)
                        {
                            bool found = false;
                            for (int j = 0; j < songs[i].Performers.Length; j++)
                            {
                                if (songs[i].Performers[j].Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (found)
                            {
                                results[resultCount++] = songs[i];
                            }
                        }
                        break;
                    default:
                        Console.WriteLine(" Неправильний вибір. Натисніть будь-яку клавішу...\n > ");
                        Console.ReadKey();
                        return;
                }

                Console.WriteLine($"\n Знайдено результатів: {resultCount}");
                for (int i = 0; i < resultCount; i++)
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

        private bool SaveToFile(string fileName = defaultFileName)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                Song[] songsToSave = new Song[count];
                Array.Copy(songs, songsToSave, count);

                string jsonString = JsonSerializer.Serialize(songsToSave, options);
                File.WriteAllText(fileName, jsonString, Encoding.Unicode);
                isSaved = true;
                return true;
            }
            catch 
            { 
                return false;
            }
        }

        public void SaveToFileUI()
        {
            Console.Clear();
            Console.WriteLine($"\n {lineText} ЗБЕРЕЖЕННЯ КОЛЕКЦІЇ У ФАЙЛ {lineText}\n");

            Console.Write($" Введіть ім'я файлу (за замовчуванням {defaultFileName}): \n > ");
            string fileName = Console.ReadLine() ?? defaultFileName;

            if (SaveToFile(fileName))
            {
                Console.WriteLine($"\n Колекцію успішно збережено у файл '{fileName}'. Натисніть будь-яку клавішу...");
            }
            else
            {
                Console.WriteLine("\n Помилка при збереженні файлу! Натисніть будь-яку клавішу...");
            }

            Console.ReadKey();
        }

        private bool LoadFromFile(string fileName = defaultFileName)
        {
            try
            {
                string jsonString = File.ReadAllText(fileName, Encoding.Unicode);
                Song[]? loadedSongs = JsonSerializer.Deserialize<Song[]>(jsonString);

                if (loadedSongs != null)
                {
                    songs = new Song[capacity];
                    count = loadedSongs.Length;
                    Array.Copy(loadedSongs, songs, loadedSongs.Length);

                    isSaved = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public void LoadFromFileUI()
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

            if (LoadFromFile(fileName))
            {
                Console.WriteLine($"\n Завантажено {count} пісень з файлу '{fileName}'. Натисніть будь-яку клавішу...");
            }
            else
            {
                Console.WriteLine("\n Помилка при завантаженні файлу! Натисніть будь-яку клавішу...");
            }

            Console.ReadKey();
        }

        public void ShowSongsByPerformer()
        {
            Console.Clear();
            Console.WriteLine($"\n {lineText} ПІСНІ ЗА ВИКОНАВЦЕМ {lineText}\n");

            if (count == 0)
            {
                Console.WriteLine(" Колекція пісень порожня. Натисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            Console.Write(" Введіть ім'я виконавця: \n > ");
            string performer = Console.ReadLine() ?? string.Empty;

            Song[] performerSongs = new Song[count];
            int resultCount = 0;

            for (int i = 0; i < count; i++)
            {
                bool hasPerformer = false;
                for (int j = 0; j < songs[i].Performers.Length; j++)
                {
                    if (songs[i].Performers[j].Contains(performer, StringComparison.OrdinalIgnoreCase))
                    {
                        hasPerformer = true;
                        break;
                    }
                }

                if (hasPerformer)
                {
                    performerSongs[resultCount++] = songs[i];
                }
            }

            Console.WriteLine($"\n Знайдено пісень виконавця '{performer}': {resultCount}");

            for (int i = 0; i < resultCount; i++)
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

            if (count == 0)
            {
                Console.WriteLine(" Колекція пісень порожня. Натисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n --- Пісня #{i + 1} ---");
                Console.WriteLine(songs[i].ToString());
            }

            Console.WriteLine("\n Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }
}
