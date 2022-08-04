namespace ChordService.src.Models.Chords
{
    public class RomanNumeralChord
    {
        public readonly ChordMode Mode;
        public readonly RomanNumeral Numeral;

        public RomanNumeralChord(ChordMode _m, RomanNumeral _r)
        {
            Mode = _m;
            Numeral = _r;
        }
    }
}
