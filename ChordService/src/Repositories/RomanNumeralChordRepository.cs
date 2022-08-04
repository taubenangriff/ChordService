using ChordService.src.Models.Chords;
using ChordService.src.Repositories.Interfaces;

namespace ChordService.src.Repositories
{
    public class RomanNumeralChordRepository : IRomanNumeralChordRepository
    {
        int AbsoluteRoot { get; set; }

        private Dictionary<ChordMode, Func<Chord>> ChordsByMode = new Dictionary<ChordMode, Func<Chord>>
        {
            { ChordMode.Major, () => ChordHelper.GetMajorTriad() },
            { ChordMode.Minor, () => ChordHelper.GetMinorTriad() },
            { ChordMode.Augmented, () => ChordHelper.GetAugmentedTriad() },
            { ChordMode.Diminished, () => ChordHelper.GetDiminished() },
            { ChordMode.HalfDiminished, () => ChordHelper.GetHalfDiminished() },
            { ChordMode.DominantSeventh, () => ChordHelper.GetDominantSeventh() }
        };

        public async Task<Chord> GetAsync(RomanNumeralChord chord, CancellationToken ct = default(CancellationToken))
        {
            return await Task.Run(() => GetChord(chord));
        }

        private Chord GetChord(RomanNumeralChord chord)
        {
            int HalfSteps = (int)chord.Numeral;
            int ChordRoot = HalfSteps + AbsoluteRoot;

            Chord? chordData = ChordsByMode.GetValueOrDefault(chord.Mode)?.Invoke().Transpose(ChordRoot);
            if (chordData is null) throw new InvalidOperationException("Mode is unsupported!");
            chordData.Name = GetRepresentation(chord);
            return chordData;
        }

        public String GetRepresentation(RomanNumeralChord chord)
        {
            String Base = GetNumeralRepresentation(chord.Numeral);
            switch (chord.Mode)
            {
                case ChordMode.Minor: Base = Base.ToLower(); break;
                case ChordMode.Augmented: Base = Base + @"⁺"; break;
                case ChordMode.Diminished: Base = Base + @"°"; break;
                case ChordMode.HalfDiminished: Base = Base + @"^"; break;
            }
            return Base;
        }

        private String GetNumeralRepresentation(RomanNumeral numeral)
        {
            switch (numeral)
            {
                case RomanNumeral.One: return "I";
                case RomanNumeral.TwoMinor: return "bII";
                case RomanNumeral.TwoMajor: return "II";
                case RomanNumeral.ThreeMinor: return "bIII";
                case RomanNumeral.ThreeMajor: return "III";
                case RomanNumeral.Four: return "IV";
                case RomanNumeral.Tritone: return "bV";
                case RomanNumeral.Five: return "V";
                case RomanNumeral.SixMinor: return "bVI";
                case RomanNumeral.SixMajor: return "VI";
                case RomanNumeral.SevenMinor: return "bVII";
                case RomanNumeral.SevenMajor: return "VII";
                default: throw new InvalidProgramException("Not all Roman Numerals have a representation. add asap.");
            }
        }
    }
}
