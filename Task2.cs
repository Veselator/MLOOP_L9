using MLOOP_L9;

internal class Task2 : ITask
{
    public const string Title = "Завдання 2";
    public const int NOfTask = 2;

    public static void Start()
    {
        Console.Write($" {Title}:\n Введіть шлях до файлу з текстом\n > ");
        string? textFilePath = Console.ReadLine();

        Console.Write(" Введіть шлях до файлу зі словами для цензури\n > ");
        string? bannedWordsFilePath = Console.ReadLine();

        string result = Censor.CensorText(textFilePath, bannedWordsFilePath);
        Console.WriteLine(result);
    }
}