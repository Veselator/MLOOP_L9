using System.Text.RegularExpressions;
using System.Text;

namespace MLOOP_L9
{
    public class FileStatistics
    {
        public static string GetFileStatistics(string? filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return " Помилка: Файл не знайдено.";
                }

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string content = reader.ReadToEnd();

                    int sentenceCount = Regex.Matches(content, @"[.!?]+").Count;
                    int upperCaseCount = 0;
                    int lowerCaseCount = 0;
                    int vowelsCount = 0;
                    int consonantsCount = 0;
                    int digitsCount = 0;

                    foreach (char c in content)
                    {
                        if (char.IsUpper(c))
                            upperCaseCount++;
                        if (char.IsLower(c))
                            lowerCaseCount++;
                        if (char.IsDigit(c))
                            digitsCount++;
                        if (char.IsLetter(c))
                        {
                            char lowerChar = char.ToLower(c);
                            if ("аеєиіїоуюяaeiouy".Contains(lowerChar))
                                vowelsCount++;
                            else
                                consonantsCount++;
                        }
                    }

                    StringBuilder result = new StringBuilder();
                    result.AppendLine("Статистика файлу:");
                    result.AppendLine($"Кількість речень: {sentenceCount}");
                    result.AppendLine($"Кількість великих літер: {upperCaseCount}");
                    result.AppendLine($"Кількість маленьких літер: {lowerCaseCount}");
                    result.AppendLine($"Кількість голосних літер: {vowelsCount}");
                    result.AppendLine($"Кількість приголосних літер: {consonantsCount}");
                    result.AppendLine($"Кількість цифр: {digitsCount}");

                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"Помилка при аналізі файлу: {ex.Message}";
            }
        }
    }
}
