using System.Text.RegularExpressions;
using System.Text;

public class Censor
{
    public static string CensorText(string? textFilePath, string? bannedWordsFilePath)
    {
        try
        {
            if (!File.Exists(textFilePath))
            {
                return " Помилка: Файл з текстом не знайдено.";
            }

            if (!File.Exists(bannedWordsFilePath))
            {
                return " Помилка: Файл зі словами для цензури не знайдено.";
            }

            string text;
            using (FileStream textFileStream = new FileStream(textFilePath, FileMode.Open, FileAccess.Read))
            using (StreamReader textReader = new StreamReader(textFileStream, Encoding.UTF8))
            {
                text = textReader.ReadToEnd();
            }

            List<string> bannedWords = new List<string>();
            using (FileStream bannedWordsFileStream = new FileStream(bannedWordsFilePath, FileMode.Open, FileAccess.Read))
            using (StreamReader bannedWordsReader = new StreamReader(bannedWordsFileStream, Encoding.UTF8))
            {
                string? line;
                while ((line = bannedWordsReader.ReadLine()) != null)
                {
                    bannedWords.Add(line);
                }
            }

            string censoredText = text;
            foreach (string word in bannedWords)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    string trimmedWord = word.Trim();
                    string replacement = new string('*', trimmedWord.Length);

                    censoredText = Regex.Replace(
                        censoredText,
                        $@"\b{Regex.Escape(trimmedWord)}\b",
                        replacement,
                        RegexOptions.IgnoreCase
                    );
                }
            }

            string resultFilePath = Path.Combine(Path.GetDirectoryName(textFilePath) ?? "", $"censored_{Path.GetFileName(textFilePath)}");

            using (FileStream resultFileStream = new FileStream(resultFilePath, FileMode.Create, FileAccess.Write))
            using (StreamWriter resultWriter = new StreamWriter(resultFileStream, Encoding.UTF8))
            {
                resultWriter.Write(censoredText);
            }

            return $" Текст успішно відцензуровано.\n Результат збережено у файлі: {resultFilePath}\n\n" +
                   $" Відцензурований текст:\n{censoredText}";
        }
        catch (Exception ex)
        {
            return $" Помилка при цензуруванні тексту: {ex.Message}";
        }
    }
}