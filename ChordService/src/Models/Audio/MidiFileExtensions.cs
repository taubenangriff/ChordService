using Melanchall.DryWetMidi.Core;

namespace ChordService.src.Models.Audio
{
    public static class MidiSequenceExtensions
    {
        public static Stream ToStream(this MidiFile seq)
        {
            var MemoryStream = new MemoryStream();
            seq.Write(MemoryStream);
            MemoryStream.Seek(0, SeekOrigin.Begin);
            return MemoryStream;
        }
    }
}
