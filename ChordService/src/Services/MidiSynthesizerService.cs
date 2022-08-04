using ChordService.src.Services.Interfaces;

using SimpleSynth.Parameters;
using SimpleSynth.Parsing;
using SimpleSynth.Providers;
using SimpleSynth.Synths;

namespace ChordService.src.Services
{
    public class MidiSynthesizerService : IMidiSynthesizerService
    {
        public async Task<Stream> GetSynthesizedWaveAudioAsync(Stream midiFile, CancellationToken ct = default(CancellationToken))
        {
            return await Task.Run(() => GetWave(midiFile), ct);
        }

        private Stream GetWave(Stream midiFile)
        {
            var interpretation = new MidiInterpretation(midiFile, new DefaultNoteSegmentProvider());
            var synth = BuildSynthesizer(interpretation);
            return synth.GenerateWAV();
        }

        private MidiSynth BuildSynthesizer(MidiInterpretation interpretation)
        {
            return new BasicSynth(
                interpretation,
                new DefaultAdsrEnvelopeProvider(AdsrParameters.Short),
                new DefaultBalanceProvider());
        }
    }
}
