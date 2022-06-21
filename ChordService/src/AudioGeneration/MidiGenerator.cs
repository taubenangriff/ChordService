using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

using Melanchall.DryWetMidi.Interaction;
using System.Diagnostics;

namespace ChordService.AudioGeneration
{
    public class MidiGenerator
    {
        public long ChordDelta { get;} = 200;
        public byte Velocity = 45;

        public MidiFile BuildDummyMidiFile()
        {
            var midiFile = new MidiFile(
            new TrackChunk(
                new SetTempoEvent(500000)),
            new TrackChunk(
                new NoteOnEvent((SevenBitNumber)60, (SevenBitNumber)45),
                new NoteOffEvent((SevenBitNumber)60, (SevenBitNumber)0)
                {
                    DeltaTime = ChordDelta
                }));

            return midiFile;
        }

        public MidiFile BuildFromChordSequence(IEnumerable<ChordGeneration.Chord> _chords)
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

        public MidiFile BuildFromChord(ChordGeneration.Chord _chord)
        { 
            return BuildFromChordSequence( new ChordGeneration.Chord[] {_chord} );
        }

        private TrackChunk ChunkFromChord(ChordGeneration.Chord _chord, long timestamp)
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
