using Melanchall.DryWetMidi.Core;

namespace ChordService.AudioGeneration
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="seq"></param>
    /// <returns>An unclosed stream containing the midi sequence</returns>
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
