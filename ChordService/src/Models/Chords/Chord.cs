namespace ChordService.src.Models.Chords
{
    public class Chord
    {
        public IEnumerable<float> Notes { get; private set; }
        public int NotesCount => Notes.Count();
        public String Name { get; set; }

        public Chord(IEnumerable<float> _notes) {
            Notes = _notes;
            Name = "?";
        }

        public int GetOctave() => (int)Notes.Min() / 12;

        public Chord(IEnumerable<float> _notes, String _name)
        {
            Notes = _notes;
            Name = _name;
        }

        public Chord Transpose(int _halfsteps)
        {
            var notes = Notes.Select(x => x + _halfsteps);
            return new Chord(notes, Name);
        }

        public Chord Invert(int times)
        {
            if (times <= 0) return this;
            var lasts = Notes.Skip(1).ToList();
            var firsts = Notes.Take(1);
            foreach (var first in firsts)
                lasts.Add(first + 12);

            return new Chord(lasts, Name).Invert(times - 1);
        }
    }
}
