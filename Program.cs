using System.Text;

namespace MLOOP_L9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Console.ForegroundColor = ConsoleColor.White;

            int userChoice;

            Console.WriteLine(
                "  _____________________\n" +
                " /                     \\ \n" +
                " |Лабораторна робота №9| \n" +
                " \\_____________________/"
            );

            do
            {
                Console.Write($"\n Виберіть завдання (0 - вихід): \n\n" +
                    $" {Task1.NOfTask}) {Task1.Title};\n" +
                    $" {Task2.NOfTask}) {Task2.Title};\n" +
                    $" {Task3.NOfTask}) {Task3.Title}.\n > ");
                string userInput = Console.ReadLine();
                Console.Clear();
                if (!int.TryParse(userInput, out userChoice)) userChoice = 0;

                switch(userChoice)
                {
                    case Task1.NOfTask:
                        Task1.Start();
                        break;
                    case Task2.NOfTask:
                        Task2.Start();
                        break;
                    case Task3.NOfTask:
                        Task3.Start();
                        break;
                }

                Console.Clear();
            } 
            while (userChoice != 0);
        }
    }
}
