using ChordService.ChordGeneration;

namespace ChordService.Services
{
    public class RomanNumeralService
    {
        private RomanNumeralChord chord = new(Mode.HalfDiminished, RomanNumeral.ThreeMinor);

        private RomanNumeralChordGenerator generator = new RomanNumeralChordGenerator();

        public Chord GetRandomChord()
        {
            return generator.GetChord(chord);
        }

        public Chord GetChord(RomanNumeralChord _chord)
        {
            return generator.GetChord(_chord);
        }

        public Chord GetChord(RomanNumeralChord chord, int? inversion, int? transposition)
        {
            Chord c = generator.GetChord(chord);
            if (inversion.HasValue)
                c = c.Invert(inversion.Value);
            if (transposition.HasValue)
                c = c.Transpose(transposition.Value);
            return c;
        }
    }
}
