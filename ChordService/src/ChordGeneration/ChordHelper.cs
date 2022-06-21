namespace ChordService.ChordGeneration
{
    public static class ChordHelper
    {
        public static Chord GetAugmentedTriad()
        {
            float[] notes = { 0, 4, 8 };
            return new Chord(notes);
        }

        public static Chord GetMajorTriad()
        {
            float[] notes = { 0, 4, 7 };
            return new Chord(notes);
        }

        public static Chord GetMinorTriad()
        {
            float[] notes = { 0, 3, 7 };
            return new Chord(notes);
        }

        public static Chord GetDiminished()
        {
            float[] notes = { 0, 3, 6, 9 };
            return new Chord(notes);
        }

        public static Chord GetHalfDiminished()
        {
            float[] notes = { 0, 3, 6, 10 };
            return new Chord(notes);
        }
    }
}
