namespace ChordService.ChordGeneration
{
    public enum Mode { 
        Major,
        Minor,
        Augmented, 
        Diminished,
        HalfDiminished
    }

    public enum RomanNumeral 
    { 
        One,
        TwoMinor, 
        TwoMajor, 
        ThreeMinor, 
        ThreeMajor, 
        Four, 
        Tritone, 
        Five, 
        SixMinor,
        SixMajor,
        SevenMinor, 
        SevenMajor
    }

    public struct RomanNumeralChord
    {
        public readonly Mode Mode;
        public readonly RomanNumeral Numeral;

        public RomanNumeralChord(Mode _m, RomanNumeral _r)
        { 
            Mode = _m;
            Numeral = _r;
        }
    }

    public class RomanNumeralChordGenerator
    {
        int AbsoluteRoot { get; set; }

        private Dictionary<Mode, Func<Chord>> ChordsByMode = new Dictionary<Mode, Func<Chord>>
        {
            { Mode.Major, () => ChordHelper.GetMajorTriad() },
            { Mode.Minor, () => ChordHelper.GetMinorTriad() },
            { Mode.Augmented, () => ChordHelper.GetAugmentedTriad() },
            { Mode.Diminished, () => ChordHelper.GetDiminished() },
            { Mode.HalfDiminished, () => ChordHelper.GetHalfDiminished() }
        };

        public RomanNumeralChordGenerator() { }

        public Chord GetChord(RomanNumeralChord chord)
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
                case Mode.Minor: Base = Base.ToLower(); break;
                case Mode.Augmented: Base = Base + @"⁺"; break;
                case Mode.Diminished: Base = Base + @"°"; break;
                case Mode.HalfDiminished: Base = Base + @"^"; break;
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
