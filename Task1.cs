namespace MLOOP_L9
{
    internal class Task1 : ITask
    {
        public const string Title = "Завдання 1";
        public const int NOfTask = 1;

        public static void Start()
        {
            Console.Write($" {Title}:\n Введіть путь до файлу\n > ");
            Console.WriteLine(FileStatistics.GetFileStatistics(Console.ReadLine()));
        }
    }
}
