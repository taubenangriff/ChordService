using ChordService.src.Models.Chords;

namespace ChordService.src.Contracts.Requests
{
    public class RomanNumeralChordRequest
    {
        public String Numeral { get; init; } = RomanNumeral.One.ToString();
        public String Mode { get; init; } = ChordMode.Major.ToString();
        public byte? Transposition { get; init; } = 60;
        public byte? Inversion { get; init; } = 0;
    }
}
