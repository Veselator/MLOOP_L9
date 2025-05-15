using System.Text;

namespace MLOOP_L9
{
    internal class Task3 : ITask
    {
        public const string Title = "Завдання 3";
        public const int NOfTask = 3;

        public static string loadFromFile(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static void Start()
        {
            SongManager manager = new SongManager("songs.json");

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine($"\n {SongManager.lineText} МЕНЕНДЖЕР ПІСЕНЬ \"ΚΑΛΜΙΟΥΣ\" (КАЛЬМІУС) {SongManager.lineText}\n");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(loadFromFile("title.txt"));
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(" 1. Додати пісню");
                Console.WriteLine(" 2. Видалити пісню");
                Console.WriteLine(" 3. Редагувати пісню");
                Console.WriteLine(" 4. Пошук пісні");
                Console.WriteLine(" 5. Зберегти колекцію вручну");
                Console.WriteLine(" 6. Завантажити колекцію з файлу");
                Console.WriteLine(" 7. Показати пісні конкретного виконавця");
                Console.WriteLine(" 8. Показати всі пісні");
                Console.WriteLine(" 0. Вихід");
                Console.Write("\n Ваш вибір: \n > ");
                if (!int.TryParse(Console.ReadLine(), out int choice)) continue;

                switch (choice)
                {
                    case 1:
                        manager.AddSong();
                        break;
                    case 2:
                        manager.RemoveSong();
                        break;
                    case 3:
                        manager.EditSong();
                        break;
                    case 4:
                        manager.SearchSong();
                        break;
                    case 5:
                        manager.SaveToFileUI();
                        break;
                    case 6:
                        manager.LoadFromFileUI();
                        break;
                    case 7:
                        manager.ShowSongsByPerformer();
                        break;
                    case 8:
                        manager.ShowAllSongs();
                        break;
                    case 0:
                        exit = true;
                        manager.CheckIsSaved();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
