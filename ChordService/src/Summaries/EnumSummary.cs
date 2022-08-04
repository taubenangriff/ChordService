using ChordService.src.Models.Chords;

namespace ChordService.src.Summaries
{
    public class EnumSummary
    {
        public static String GetEnumDocumentation<T>()
        {
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException();

            return "- " + String.Join("\n - ", Enum.GetValues(typeof(T)).Cast<T>().Select(x => x.ToString()));
        }


    }
}
