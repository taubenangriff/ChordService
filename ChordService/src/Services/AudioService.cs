using ChordService.AudioGeneration;
using ChordService.ChordGeneration;

namespace ChordService.Services
{
    public enum AudioType { wave, midi }

    public class AudioService
    {
        MidiGenerator generator = new MidiGenerator();
        AudioFromMidiBuilder builder = new AudioFromMidiBuilder();

        public async Task<Stream?> GetCadenceAsync(AudioType audioType, int transposition, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                var stream = generator.BuildFromChordSequence(
                new Chord[]
                {
                        ChordHelper.GetMajorTriad().Invert(1).Transpose(transposition),
                        ChordHelper.GetMajorTriad().Transpose(transposition + 5),
                        ChordHelper.GetMajorTriad().Transpose(transposition - 5).Invert(2),
                        ChordHelper.GetMajorTriad().Invert(1).Transpose(transposition)
                }).ToStream();
                return UpdateByAudioType(stream, audioType);
            },
            cancellationToken);
        }

        public String GetFilename(AudioType type) => type == AudioType.wave ? "audio.wav" : "audio.mid";

        private Stream UpdateByAudioType(Stream s, AudioType type) => type == AudioType.wave ? builder.GetWave(s) : s;

        public async Task<Stream?> GetSequenceAsync(Chord[] chords, AudioType audioType, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => UpdateByAudioType(generator.BuildFromChordSequence(chords).ToStream(), audioType), cancellationToken);
        }
    }
}
