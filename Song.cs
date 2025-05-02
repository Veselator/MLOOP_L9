using System.Text;

namespace MLOOP_L9
{
    [Serializable]
    public class Song
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Composer { get; set; }
        public int Year { get; set; }
        public string Lyrics { get; set; }
        public string Link { get; set; }
        public string[] Performers { get; set; } = Array.Empty<string>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" Назва: {Title}");
            sb.AppendLine($" Автор: {Author}");
            sb.AppendLine($" Композитор: {Composer}");
            sb.AppendLine($" Рік: {Year}");
            sb.AppendLine($" Виконавці: {string.Join(", ", Performers)}");
            sb.AppendLine($" Текст пісні:\n {Lyrics}");
            sb.AppendLine($" Посилання на пісню:\n {Link}");
            return sb.ToString();
        }
    }
}
