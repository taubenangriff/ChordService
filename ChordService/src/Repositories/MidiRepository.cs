using ChordService.src.Models.Audio;
using ChordService.src.Models.Chords;
using ChordService.src.Repositories.Interfaces;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace ChordService.src.Repositories
{
    public class MidiRepository : IMidiRepository
    {
        public long ChordDelta { get; } = 200;
        public byte Velocity = 45;

        public async Task<Stream> GetAsync(Chord c, CancellationToken ct = default(CancellationToken))
        {
            return await Task.Run(() => BuildFromChord(c).ToStream(), ct);
        }

        private MidiFile BuildFromChord(Chord _chord)
        {
            return BuildFromChordSequence(new Chord[] { _chord });
        }

        public MidiFile BuildFromChordSequence(IEnumerable<Chord> _chords)
        {
            var midiFile = new MidiFile(
                new TrackChunk(
                    new SetTempoEvent(500000))
            );
            for (int i = 0; i < _chords.Count(); i++)
            {
                var chunk = ChunkFromChord(_chords.ElementAt(i), ChordDelta * i);
                midiFile.Chunks.Add(chunk);
            }
            return midiFile;
        }


        private TrackChunk ChunkFromChord(src.Models.Chords.Chord _chord, long timestamp)
        {
            var trackChunk = new TrackChunk();

            bool set = true;
            foreach (float f in _chord.Notes)
            {
                trackChunk.Events.Add(
                    new NoteOnEvent((SevenBitNumber)((int)f), (SevenBitNumber)Velocity)
                    {
                        DeltaTime = set ? timestamp : 0
                    }
                );
                set = false;
            }

            set = true;
            foreach (float f in _chord.Notes)
            {
                trackChunk.Events.Add(
                    new NoteOffEvent((SevenBitNumber)((int)f), (SevenBitNumber)0)
                    {
                        DeltaTime = set ? ChordDelta : 0
                    }
                );
                set = false;
            }
            return trackChunk;
        }
    }
}
